using loader_5_0_lib.Classes;
using loader_5_0_lib.DataBase;
using loader_5_0_lib.Helpers;
using loader_5_0_lib.Service.Interfaces;
using loader_utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Xml.Linq;

namespace loader_5_0_lib.Service.Implementation
{
    public class Loader(
        ILogger<Loader> logger, IBureauWebUi bureauWebUi, IAppParam appParam,
        IUniDocService uniDocService, IConfiguration config,
        IDocService docService, MSSQLDbContext mSSQLDbContext, IStopWatch stopWatch, IDbService dbService) : ILoader
    {
        private readonly ILogger<Loader> _log = logger;
        private readonly IBureauWebUi _bureauWebUi = bureauWebUi;
        private readonly IAppParam _appParam = appParam;
        private readonly IUniDocService _uniDocService = uniDocService;
        private readonly IConfiguration _config = config;
        private readonly IDocService _docService = docService;
        private readonly MSSQLDbContext _mSSQLDbContext = mSSQLDbContext;
        private readonly IStopWatch _stopWatch = stopWatch;
        private readonly IDbService _dbService = dbService;
        public ActiveUpload? activeUpload { get; set; }
        public void StartUploading()
        {
            try
            {
                string UploadPath = _appParam.UploadPath;


                Dictionary<string, bool> UploadFiles = [];

                if (_appParam.FileSystemUse)
                {
                    UploadFiles = GetFilesFromPath(_appParam.FileSystemUploadFilePath);
                }

                activeUpload = GetActiveUploadRequest(UploadFiles);

                _appParam.ValidateAppsettingsParams();

                while (activeUpload != null)
                {
                    AppContextParams.SetSomething(activeUpload.KeyId);

                    activeUpload.UploadInfo.PackageInfo.ProcessingStart(DateTime.Now);
                    PostUploadInfo(activeUpload);

                    string UploadFullPath = Path.Combine(UploadPath, activeUpload.UploadPath);

                    if (activeUpload.UploadInfo.DocsInfo.Count > 1)
                    {
                        XElement comment = new("user");
                        comment.SetAttributeValue("user-id", 5045021);
                        comment.SetAttributeValue("organization-id", 0);
                        comment.SetAttributeValue("email", "help@rs-cb.ru");
                        comment.SetAttributeValue("name", "Служба поддержки");
                        comment.SetAttributeValue("created", DateTime.Now.ToString(new CultureInfo("de-DE")));
                        comment.Add(new XElement("text", "Документ выгрузки содержит некоррктеное колличесов файлов в архиве"));
                        comment.Add(new XElement("notification-to",
                            new XAttribute("email", "help@rs-cb.ru"),
                            new XAttribute("name", "Тех. поддержка")
                            ));

                        activeUpload.UploadInfo.AddComment(new UploadUserComment(comment));
                        activeUpload.UploadInfo.PackageInfo.ProcessingStatus = RefProcessingStatus.Error;
                    }
                    else
                    {
                        var docInfo = activeUpload.UploadInfo.DocsInfo.First().Value;

                        _log.LogInformation("Начало обработки документа: {FileName}, id: {id}", docInfo.FileName, activeUpload.UploadInfo.PackageInfo.UploadId);

                        string UploadFullFileName = Path.Combine(UploadFullPath, docInfo.FileName);

                        docInfo.ProcessingStage(RefProcessingStage.PreLoad).ProcessingStatus = RefProcessingStatus.Running;
                        PostUploadInfo(activeUpload);

                        FileInfo docFileInfo = new(UploadFullFileName);

                        _stopWatch.Stopwatch.Start();

                        _uniDocService.ProcessDocument(docFileInfo, activeUpload);

                        _docService.Acceptance.DocInfoExt.subjectsReceived = _uniDocService.SubjectsReceived;
                        _docService.Acceptance.DocInfoExt.eventsReceived = _uniDocService.EventsReceived;
                        _docService.Acceptance.DocInfoExt.eventsAccepted = _uniDocService.EventsAccepted;
                        _docService.Acceptance.DocInfoExt.eventsRejected = _uniDocService.EventsRejected;

                        _stopWatch.Stopwatch.Stop();

                        _log.LogInformation("Время обработки документа: {stopWatch}", _stopWatch.Stopwatch.Elapsed);

                        XElement xReceipt = XElement.Parse(SerializeHelper.Serialize(_docService.Receipt) ?? new("NotificationOfReceipt"));
                        XElement? xAcceptance = null;

                        if (!_docService.Receipt.HasErrors)
                        {
                            _docService.Acceptance.sendingDateTime = DateTime.Now.RemoveMilliseconds();

                            _dbService.AddToDbContextAcceptance(activeUpload.KeyId, _docService.Acceptance);

                            xAcceptance = XElement.Parse(SerializeHelper.Serialize(_docService.Acceptance));
                        }

                        //ошибки и сообщения в xml не пишуться
                        if (!_appParam.FileSystemUse && xAcceptance?.Element("Errors") != null)
                        {
                            string message02 = $"Извещение: select [dbo].[loader_getUploadResultXmlByKeyId]({activeUpload.KeyId})";
                            var error01 = new XElement("Message", message02);
                            xAcceptance?.Element("Errors")?.ReplaceNodes(error01);
                        }

                        if (_docService.Receipt.HasErrors)
                        {
                            foreach (var rError in _docService.Receipt.Errors)
                            {
                                docInfo.ProcessingStage(RefProcessingStage.PreLoad).Summary.AddError(1);
                            }

                            docInfo.ProcessedElementsSend = 0;
                            docInfo.ProcessingStage(RefProcessingStage.PreLoad).Summary.TotalElementsCount = 0;
                            docInfo.ProcessingStage(RefProcessingStage.PreLoad).Summary.ProcessedElementsCount = 0;
                        }
                        else
                        {
                            foreach (var rError in _docService.Acceptance.ErrorsExt ?? [])
                            {
                                docInfo.ProcessingStage(RefProcessingStage.PreLoad).Summary.AddError(2);
                            }

                            docInfo.ProcessedElementsSend = (int)_docService.Acceptance.DocInfoExt.eventsAccepted;
                            docInfo.ProcessingStage(RefProcessingStage.PreLoad).Summary.TotalElementsCount = (long)_docService.Acceptance.DocInfoExt.eventsReceived;
                            docInfo.ProcessingStage(RefProcessingStage.PreLoad).Summary.ProcessedElementsCount = (long)_docService.Acceptance.DocInfoExt.eventsAccepted;
                        }

                        _dbService.SaveDbContext();

                        docInfo.ProcessingStage(RefProcessingStage.PreLoad).ProcessingStatus = RefProcessingStatus.Finish;

                        PostUploadInfo(activeUpload);

                        _dbService.UpdateDocStatus(activeUpload, 3, xReceipt, xAcceptance);
                    }

                    activeUpload.UploadInfo.PackageInfo.ProcessingFinish(DateTime.Now);
                    PostUploadInfo(activeUpload);

                    _log.LogInformation("Окончание обработки документа: {FileName}, id: {id}", activeUpload.UploadInfo.DocsInfo.First().Value.FileName, activeUpload.UploadInfo.PackageInfo.UploadId);

                    activeUpload = GetActiveUploadRequest(UploadFiles);
                }
            }
            catch (Exception ex)
            {
                var errMsg = string.Concat(ex.Message, Environment.NewLine, ex.StackTrace);

                _log.LogCritical("Критическая ошибка: {errMsg}", errMsg);

                if (activeUpload != null)
                {
                    _dbService.UpdateDocStatus(activeUpload, -1, null, null);
                    PostUploadInfo(activeUpload);
                }
                else
                {
                    throw new Exception(errMsg);
                }
            }
        }
        public void PostUploadInfo(ActiveUpload activeUpload)
        {
            if (_appParam.FileSystemUse)
            {
                var UploadFileName = activeUpload.UploadInfo.DocsInfo.First().Value.FileName;
                Directory.CreateDirectory(activeUpload.FileSystemProcessingPath);

                string FullFileNameUploadingXml = Path.Combine(
                    activeUpload.FileSystemProcessingPath,
                    "UploadingXml_" + UploadFileName
                    );

                activeUpload.UploadInfo.Xml.Save(FullFileNameUploadingXml);
            }
            else
            {
                _bureauWebUi.UpdateUploadingXml(activeUpload.KeyId, activeUpload.UploadInfo.Xml);
            }
        }
        public Dictionary<string, bool> GetFilesFromPath(string uploadFilesPath)
        {
            Dictionary<string, bool> files = new();

            string? filesDirectoryName = Path.GetDirectoryName(uploadFilesPath);
            if (filesDirectoryName != null)
            {
                files = Directory.GetFiles(uploadFilesPath, "*.XML").ToDictionary(x => x, x =>
                _appParam.ForceReload ? false : Path.Exists(Path.Combine(uploadFilesPath, Path.GetFileNameWithoutExtension(x)))
                );
            }

            return files;
        }
        public ActiveUpload? GetNextFile(Dictionary<string, bool> uploadFiles)
        {
            ActiveUpload? activeUpload = default;

            if (_appParam.FileSystemUploadFilePath == null)
            {
                return null;
            }

            if (!uploadFiles.Any(x => !x.Value))
            {
                return null;
            }

            var uploadFilePath = uploadFiles.Where(x => !x.Value).First();
            FileInfo uploadFileInfo = new FileInfo(uploadFilePath.Key);
            string processingPath = Path.Combine(_appParam.FileSystemUploadFilePath, Path.GetFileNameWithoutExtension(uploadFileInfo.Name).Trim());

            if (_appParam.ForceReload)
            {
                try
                {
                    Directory.Delete(processingPath, true);

                }
                catch (Exception e)
                {
                    _log.LogWarning("{Message}", e.Message);
                }
            }

            if (Path.Exists(processingPath))
            {
                uploadFiles[uploadFilePath.Key] = true;
                return null;
            }

            activeUpload = new(0, DateTime.Now, "1", "2", 1, OtherHelper.GetUploadXml(uploadFileInfo))
            {
                UploadPath = uploadFileInfo.DirectoryName ?? string.Empty,
                FileSystemProcessingPath = processingPath
            };

            uploadFiles[uploadFilePath.Key] = true;

            return activeUpload;
        }
        public ActiveUpload? GetActiveUploadRequest(Dictionary<string, bool>? uploadFiles)
        {
            if (_appParam.FileSystemUse && uploadFiles != null && uploadFiles.Count > 0)
            {
                return GetNextFile(uploadFiles);
            }
            else
            {
                return _bureauWebUi.GetActiveUploadRequest();
            }
        }
    }
}
