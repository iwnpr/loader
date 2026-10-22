using loader_5_0_lib.Adapters;
using loader_5_0_lib.Classes;
using loader_5_0_lib.DataBase;
using loader_5_0_lib.Enums;
using loader_5_0_lib.Helpers;
using loader_5_0_lib.Service.Implementation;
using loader_5_0_lib.Service.Interfaces;
using loader_utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.Data;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Xsd;
using xsd_base.BaseXmlValidation;

namespace loader_5_0_lib.Service
{
    public interface IUniDocService
    {
        ulong SubjectsReceived { get; }
        ulong EventsReceived { get; }
        ulong EventsAccepted { get; }
        ulong EventsRejected { get; }
        void ProcessDocument(FileInfo FileInfo, ActiveUpload activeUpload);
        void ProcessDocument(UploadDocInfo uploadDocInfo, ActiveUpload activeUpload);
    }

    public class UniDocService(
        ILogger<UniDocService> logger, IConfiguration config, IAppParam appParam, IDocService docService, IKafkaAdapter kafkaAdapter, IBureauData bureauData,
        IDbService dbService) : IUniDocService
    {
        private readonly ILogger<UniDocService> _logger = logger;
        private readonly IConfiguration _config = config;
        private readonly IAppParam _appParam = appParam;
        private readonly IDocService _docService = docService;
        private readonly IKafkaAdapter _kafkaAdapter = kafkaAdapter;
        private readonly IBureauData _bureauData = bureauData;
        private readonly IDbService _dbService = dbService;

        private readonly Dictionary<int, string> PriorityOrderFileNamePrefix = config.GetSection("Settings:PriorityOrder:FileNamePrefixs").Get<Dictionary<int, string>>() ?? [];

        public readonly ConcurrentQueue<string> PriorityTopics = new(config.GetSection("Kafka:PriorityTopics").Get<string[]>() ?? []);

        public ConcurrentQueue<string> SplitTopics = [];

        public readonly Dictionary<long, string> LegalTopics = config.GetSection("Kafka:Topics").Get<Dictionary<long, string>>() ?? [];

        public readonly long LegalTopicDefault = config.GetValue<long>("Kafka:TopicDefault");

        public string _topicNameDefault = (config.GetSection("Kafka:Topics").Get<Dictionary<long, string>>() ?? []).TryGetValue(config.GetValue<long>("Kafka:TopicDefault"), out string? topicName) ?
            topicName : (config.GetSection("Kafka:Topics").Get<Dictionary<long, string>>() ?? []).First().Value;

        private readonly Dictionary<int, List<string>> SplitLegals = config.GetSection("Kafka:SplitLegals").Get<Dictionary<int, List<string>>>() ?? [];
        private readonly Dictionary<string, string> SplitLegalsUsed = [];
        public ulong SubjectsReceived { get; private set; }
        public ulong EventsReceived { get; private set; }
        public ulong EventsAccepted { get; private set; }
        public ulong EventsRejected { get; private set; }

        public void ProcessDocument(FileInfo FileInfo, ActiveUpload activeUpload)
        {
            SubjectsReceived = 0;
            EventsReceived = 0;
            EventsAccepted = 0;
            EventsRejected = 0;

            var uploadDocInfo = new UploadDocInfo(FileInfo.FullName, activeUpload);
            ProcessDocument(uploadDocInfo, activeUpload);
        }

        public void ProcessDocument(UploadDocInfo uploadDocInfo, ActiveUpload activeUpload)
        {
            ObjectDeserializer? objectDeserializer = default;

            _docService.DocHeader = _docService.GetUploadDocumentHeader(uploadDocInfo.DocMemStream) ?? throw new NullReferenceException("DocumentHeader");

            INotificationOfReceipt notificationOfReceipt;
            INotificationOfReceiptErrors notificationOfReceiptErrors;
            INotificationOfAcceptance notificationOfAcceptance;
            INotificationOfAcceptanceError notificationOfAcceptanceError;

            if (_docService.DocHeader.SchemaVersion.Equals("5.0"))
            {
                objectDeserializer = new Xsd.VersionFiveZero.Main.ObjectDeserializer();

                notificationOfReceipt = new Xsd.VersionFiveZero.Receipt.NotificationOfReceipt();
                notificationOfReceiptErrors = new Xsd.VersionFiveZero.Receipt.NotificationOfReceiptErrors();
                notificationOfAcceptance = new Xsd.VersionFiveZero.Acceptance.NotificationOfAcceptance();
                notificationOfAcceptanceError = new Xsd.VersionFiveZero.Acceptance.NotificationOfAcceptanceError();
            }
            else if (_docService.DocHeader.SchemaVersion.Equals("5.1"))
            {
                objectDeserializer = new Xsd.VersionFiveOne.Main.ObjectDeserializer();

                notificationOfReceipt = new Xsd.VersionFiveOne.Receipt.NotificationOfReceipt();
                notificationOfReceiptErrors = new Xsd.VersionFiveOne.Receipt.NotificationOfReceiptErrors();
                notificationOfAcceptance = new Xsd.VersionFiveOne.Acceptance.NotificationOfAcceptance();
                notificationOfAcceptanceError = new Xsd.VersionFiveOne.Acceptance.NotificationOfAcceptanceError();
            }
            else
            {
                throw new NotImplementedException($"Ошибка обработки документа, отсутствует schemaVersion {_docService.DocHeader.SchemaVersion}");
            }

            notificationOfAcceptance.DocInfoExt.subjectsReceived = 0;
            notificationOfAcceptance.DocInfoExt.eventsReceived = 0;
            notificationOfAcceptance.DocInfoExt.eventsAccepted = 0;
            notificationOfAcceptance.DocInfoExt.eventsRejected = 0;

            _docService.Receipt = notificationOfReceipt;
            _docService.Acceptance = notificationOfAcceptance;

            #region Проверки последовательности orderNum

            BaseXmlCheckResult xmlCheckResult;

            using (XmlReader reader = XmlReader.Create(uploadDocInfo.DocMemStream, ReaderHelpers.GetXmlReaderSettings()))
            {
                BaseXmlValidator baseXmlValidator;

                if (_docService.DocHeader.SchemaVersion.Equals("5.0"))
                {
                    baseXmlValidator = new Xsd.VersionFiveZero.Main.BaseXmlValidator(reader);
                }
                else //if (_docService.DocHeader.SchemaVersion.Equals("5.1"))
                {
                    baseXmlValidator = new Xsd.VersionFiveOne.Main.BaseXmlValidator(reader);
                }

                xmlCheckResult = baseXmlValidator.CheckXmlStructure();
            }

            if (xmlCheckResult.Error != null)
            {
                RejectedAllEvents(xmlCheckResult);

                if (xmlCheckResult.Error.ErrorCode == 1)
                {
                    notificationOfReceipt.FillFields(_docService.DocHeader.Xml, uploadDocInfo.FileName, uploadDocInfo.FileHash, uploadDocInfo.UploadCreated);
                    notificationOfReceipt.AddError(7, xmlCheckResult.Error.ErrorMessage);

                    notificationOfReceipt.sendingDateTime = DateTime.Now.RemoveMilliseconds();

                    return;
                }
                else
                {
                    _docService.SetUploadDocumentSource(uploadDocInfo.DocMemStream);

                    notificationOfReceipt.FillFields(_docService.DocHeader.Xml, uploadDocInfo.FileName, uploadDocInfo.FileHash, uploadDocInfo.UploadCreated);
                    notificationOfAcceptance.DocInfoExt.FillFields(_docService.DocHeader.Xml, uploadDocInfo.FileName, uploadDocInfo.FileHash, uploadDocInfo.UploadCreated);

                    notificationOfAcceptance.AddErrors([xmlCheckResult.Error]);

                    notificationOfAcceptance.sendingDateTime = DateTime.Now.RemoveMilliseconds();
                    notificationOfReceipt.sendingDateTime = DateTime.Now.RemoveMilliseconds();

                    return;
                }
            }
            #endregion Проверки последовательности orderNum

            _docService.SetUploadDocumentSource(uploadDocInfo.DocMemStream);

            notificationOfReceipt.FillFields(_docService.DocHeader.Xml, uploadDocInfo.FileName, uploadDocInfo.FileHash, uploadDocInfo.UploadCreated);

            #region Проверка синтаксиса XML

            var xmlSyntaxCheck = ReaderHelper.CheckXmlSyntax(uploadDocInfo.DocMemStream);
            if (xmlSyntaxCheck.Count > 0)
            {
                notificationOfReceipt.AddErrors(7, [.. xmlSyntaxCheck.Select(x => $"Ошибка структуры xml: {x}")]);
                return;
            }

            #endregion Проверка синтаксиса XML

            #region c 01.07.2026 - ЕТФ 5.1 для кредиторов, ЕТФ 5.1 и ЕТФ 5.0 для ОСР

            //есть SourceCode - источник ЮР лицо и тип ОСР — оператор сервиса рассрочки)
            if (_docService.DocHeader.SourceCode.HasValue && _docService.DocHeader.SourceCode.Equals(18) && _docService.DocHeader.SchemaVersion.Equals("5.0"))
            {
                //формат 5.0 для оператор сервиса рассрочки не проверяем 
            }
            else
            {
                //Получаем информацияю о текущем формате из конфигурации
                if (_appParam.UploadFormats.TryGetValue(_docService.DocHeader.SchemaVersion, out UploadFormatInfo? uploadFormatInfo))
                {
                    //если текущая дата больше или равна дате прекращения приема формата
                    if (DateOnly.FromDateTime(DateTime.Now) >= uploadFormatInfo.ExpirationDate)
                    {
                        notificationOfReceipt.AddError((int)RefReceiptErrorType.Item99, $"с {uploadFormatInfo.ExpirationDate:dd.MM.yyyy} версия {_docService.DocHeader.SchemaVersion} недоступна для загрузки");
                        notificationOfReceipt.sendingDateTime = DateTime.Now.RemoveMilliseconds();
                        return;
                    }
                }
            }

            #endregion c 01.07.2026 - ЕТФ 5.1 для кредиторов, ЕТФ 5.1 и ЕТФ 5.0 для ОСР

            #region Проверка заголовка по xsd

            var documentForXsdCheck = new XDocument(new XElement(_docService.DocHeader.Xml));
            documentForXsdCheck.Root!.Name = "DocumentRoot";
            documentForXsdCheck.Root.Element("Source")?.Remove();

            var documentXsdError = XsdHelper.ValidateByXsd(documentForXsdCheck, _docService.DocHeader.SchemaVersion, SchemaSetType.DateTimeCheck);
            if (documentXsdError != null)
            {
                notificationOfReceipt.AddError((int)RefReceiptErrorType.Item7, $"Документ не соответсвует схеме xsd. {documentXsdError}");
                notificationOfReceipt.sendingDateTime = DateTime.Now.RemoveMilliseconds();

                return;
            }

            #endregion Проверка заголовка по xsd

            notificationOfAcceptance.DocInfoExt.FillFields(_docService.DocHeader.Xml, uploadDocInfo.FileName, uploadDocInfo.FileHash, uploadDocInfo.UploadCreated);

            var dateDoc = DateOnly.FromDateTime(DateTime.Parse(_docService.DocHeader.DateTimeDoc));

            if (dateDoc > DateOnly.FromDateTime(DateTime.Now))
            {
                notificationOfReceipt.AddError((int)RefReceiptErrorType.Item2, $"Дата регистрации документа указана в будущее");
                notificationOfReceipt.sendingDateTime = DateTime.Now.RemoveMilliseconds();

                return;
            }

            #region Проверка уникальности regNumberDoc и dateTimeDoc

            var prevRegNumberDocs = _docService.GetPreviousRegNumberDocs(activeUpload.UploadInfo.UserInfo.ClientId, new DateTime(dateDoc, TimeOnly.MinValue), activeUpload.Created, _docService.DocHeader.DateTimeDoc, _docService.DocHeader.RegNumberDoc);

            if (prevRegNumberDocs != null)
            {
                notificationOfReceipt.AddError((int)RefReceiptErrorType.Item2, $"Документ {prevRegNumberDocs.FileName} с иcходящим номером {prevRegNumberDocs.OutgoingDocNumber} и датой {prevRegNumberDocs.OutgoingDocDate} передавался ранее");
                return;
            }

            #endregion Проверка уникальности regNumberDoc и dateTimeDoc

            #region проверка части с данными источника

            var sourceXsdErrors = XsdHelper.ValidateByXsd(new XDocument(_docService.DocHeader.SourceXml), _docService.DocHeader.SchemaVersion);
            if (sourceXsdErrors != null)
            {
                notificationOfReceipt.AddError((int)RefReceiptErrorType.Item7, $"Часть с данными источника не соответствует xsd. {sourceXsdErrors}");
                notificationOfReceipt.sendingDateTime = DateTime.Now.RemoveMilliseconds();

                return;
            }

            #endregion проверка части с данными источника

            List<string> errorMessages = [];

            #region Проверка регистрационных данных ИНН и ОГРН источника
            if (!CheckLegal(_docService.DocHeader, activeUpload, out errorMessages))
            {
                notificationOfReceipt.AddErrors(3, errorMessages);
                notificationOfReceipt.sendingDateTime = DateTime.Now.RemoveMilliseconds();

                return;
            }
            #endregion Проверка регистрационных данных ИНН и ОГРН источника

            #region регистрация первого квитка

            if (!_appParam.FileSystemUse)
            {
                notificationOfReceipt.sendingDateTime = DateTime.Now.RemoveMilliseconds();

                XElement xReceipt = XElement.Parse(SerializeHelper.Serialize(notificationOfReceipt));
                _dbService.UpdateNotificationOfReceipt(activeUpload.KeyId, xReceipt);

                _dbService.AddToDbContextReceipt(activeUpload.KeyId, notificationOfReceipt);

                _dbService.SaveDbContext();
            }

            #endregion регистрация первого квитка

            List<ulong> uploadOrders =
                _appParam.FileSystemUse ? [] : _bureauData.GetSendOrders(activeUpload.KeyId);

            var document = objectDeserializer.GetDocumentObject(_docService.DocHeader.Xml);

            #region Ограничение дат снизу в головном теге

            var dateCheckError = XsdHelper.ValidateByXsd(documentForXsdCheck, _docService.DocHeader.SchemaVersion, SchemaSetType.DateTimeCheck);
            if (dateCheckError != null)
            {
                var dateError = DateErrorParser.Parse(dateCheckError);
                if (dateError != null)
                {
                    //ошибка в заголовке, все событий отклоняються
                    RejectedAllEvents(_docService.DocHeader);
                    notificationOfAcceptance.AddError(document.CreateError(null, null, dateError.Field, dateError.Value, "Дата в головном теге меньше минимально допустимого значения"));
                    return;
                }
            }

            #endregion Ограничение дат снизу в головном теге

            ISource sourceItem = objectDeserializer.GetSourceObject(_docService.DocHeader.SourceXml);

            #region Ограничение дат снизу в блоке - Source

            dateCheckError = XsdHelper.ValidateByXsd(XDocument.Load(_docService.DocHeader.SourceXml.CreateReader()), _docService.DocHeader.SchemaVersion, SchemaSetType.DateTimeCheck);

            List<INotificationOfAcceptanceError> sourceErrors = [];

            if (dateCheckError != null)
            {
                var dateError = DateErrorParser.Parse(dateCheckError);
                if (dateError != null)
                {
                    sourceErrors.Add(document.CreateError(null, null, null, dateError.Field, dateError.Value, null, null, "Дата меньше минимально допустимого значения"));
                }
            }

            dateCheckError = null;
            #endregion Ограничение дат снизу в блоке - Source

            sourceErrors.AddRange(sourceItem.Check(document));

            if (sourceErrors.Count > 0)
            {
                RejectedAllEvents(xmlCheckResult);

                notificationOfAcceptance.AddErrors(sourceErrors);
                return;
            }

            UploadSubjects docSubjects = new(uploadDocInfo.DocMemStream);

            //очередь парралельной обработки несколько топиков
            SplitTopics = [];

            if (SplitLegals.TryGetValue(activeUpload.UploadInfo.UserInfo.ClientId, out List<string>? value))
            {
                SplitTopics = new(value);
            }

            foreach (var docSubject in docSubjects)
            {
                SubjectsReceived++;

                //dataTemplate = _docService.GetDataTemplateXml(docSubject.SubjectType, _docService.DocHeader.SchemaVersion);
                //tempForCheck = XElement.Load(_docService.DocHeader.Xml.CreateReader());
                //tempForCheck.Add(new XElement("Data", new XElement(docSubject.SubjectNode)));
                //XElement? tempDataSubject = tempForCheck?.Element("Data")?.Element(docSubject.SubjectTypeValue);
                //tempDataSubject?.Add(XElement.Load(docSubject.TitleXml.CreateReader()));
                //tempDataSubject?.Add(XElement.Load(dataTemplate.Element(docSubject.SubjectTypeValue)?.Element("Events")?.CreateReader() ?? new XElement("Events").CreateReader()));
                //ResultCheck checkXsdTitle = _docValidator.ValidateByXsd(XDocument.Load(tempForCheck?.CreateReader() ?? new XElement("Document").CreateReader()));

                var titleXsdError = XsdHelper.ValidateSubjectByXsd(docSubject.TitleXml, _docService.DocHeader.SchemaVersion, docSubject.SubjectType);
                if (titleXsdError != null)
                {
                    titleXsdError = $"Титульная часть не соответствует xsd: {titleXsdError}";
                    var naError = document.CreateError(docSubject.FirstOrderNum, docSubject.FirstEventName, null, null, null, docSubject.SubjectCode, docSubject.SubjectID, titleXsdError);

                    notificationOfAcceptance.AddError(naError);

                    EventsReceived += docSubject.EventsCount;
                    EventsRejected += docSubject.EventsCount;
                    notificationOfAcceptance.DocInfoExt.eventsReceived = EventsRejected;
                    notificationOfAcceptance.DocInfoExt.eventsRejected = EventsReceived;
                    continue;
                }

                //Мария Бычкова, [29.01.2026 12:47] 3.5/4.3 не проверяем ТЧ
                //Мария Бычкова, [29.01.2026 12:47] 3.1/4.1 не проверяем current, в идеале не проверяем и ТЧ, если в пакете нет иных событий, кроме 3.1/4.1

                HashSet<string> pp01List = ["FL_Event_3_5", "UL_Event_3_5", "FL_Event_4_3", "UL_Event_4_3"];
                HashSet<string> pp04List = ["FL_Event_3_1", "UL_Event_3_1", "FL_Event_3_1_1", "UL_Event_3_1_1", "FL_Event_4_1", "UL_Event_4_1", "FL_Event_4_1_1", "UL_Event_4_1_1"];

                bool skipTitleCheck = false;

                if (docSubject.EventsCount.Equals(0))
                {
                    var naError = document.CreateError(null, null, null, null, null, docSubject.SubjectCode, docSubject.SubjectID,
                        $"Для субъекта с номером {SubjectsReceived} блок событий Events не может быть пустым");

                    notificationOfAcceptance.AddError(naError);

                    notificationOfAcceptance.DocInfoExt.eventsReceived = EventsRejected;
                    notificationOfAcceptance.DocInfoExt.eventsRejected = EventsReceived;
                    continue;
                }

                foreach (var eventType in docSubject.Events)
                {
                    //если пакет содержить события 3.5/4.3 не проверяем ТЧ
                    skipTitleCheck = pp01List.Contains(eventType);
                    if (skipTitleCheck) { break; }

                    //если в пакете нет иных событий, кроме 3.1/4.1 
                    if (pp04List.Contains(eventType))
                    {
                        skipTitleCheck = true;
                    }
                    else
                    {
                        skipTitleCheck = false;
                        break;
                    }
                }

                ISubjectTitleData titleObject = objectDeserializer.GetTitleObject(docSubject.TitleXml, docSubject.SubjectType);

                List<INotificationOfAcceptanceError> titleErrors = [];

                #region Ограничение дат снизу Титульная часть

                dateCheckError = XsdHelper.ValidateSubjectByXsd(docSubject.TitleXml, _docService.DocHeader.SchemaVersion, docSubject.SubjectType, SchemaSetType.DateTimeCheck);

                if (dateCheckError != null)
                {
                    var dateError = DateErrorParser.Parse(dateCheckError);
                    if (dateError != null)
                    {
                        titleErrors.Add(document.CreateError(docSubject.FirstOrderNum, docSubject.FirstEventName, null, dateError.Field, dateError.Value, docSubject.SubjectCode, docSubject.SubjectID, "Дата меньше минимально допустимого значения"));
                    }
                }

                #endregion Ограничение дат снизу Титульная часть

                titleObject.SkipTitleChecksInChangeEvents = skipTitleCheck ? skipTitleCheck : titleObject.SkipTitleChecksInChangeEvents;

                titleErrors.AddRange(titleObject.Check(sourceItem));

                if (!titleErrors.Any(x => x.IsCriticalTitleError) && skipTitleCheck)
                {
                    //титульная чаcть не содержит критических ошибок и не подлежит проверке по условиям
                    titleErrors = [];
                }

                if (titleErrors.Count > 0)
                {
                    titleErrors.ForEach(x => x.SubjectCode = titleObject.subjectCode);
                    titleErrors.ForEach(x => x.SubjectID = titleObject.subjectID);

                    if (docSubject.FirstOrderNum.HasValue)
                    {
                        titleErrors.ForEach(x => { x.orderNumSpecified = true; x.orderNum = docSubject.FirstOrderNum ?? 0; });
                    }

                    if (docSubject.FirstEventName != null)
                    {
                        titleErrors.ForEach(x => { x.eventName = Enum.Parse<refEventNameType>(docSubject.FirstEventName); });
                    }

                    notificationOfAcceptance.AddErrors(titleErrors);

                    EventsReceived += docSubject.EventsCount;
                    EventsRejected += docSubject.EventsCount;
                    notificationOfAcceptance.DocInfoExt.eventsReceived = EventsReceived;
                    notificationOfAcceptance.DocInfoExt.eventsRejected = EventsRejected;
                }
                else
                {
                    XElement xDocToSend = XElement.Load(_docService.DocHeader.Xml.CreateReader());

                    xDocToSend.Add(new XElement(docSubject.DataNodeName, new XElement(docSubject.LocalName)));
                    XElement xDataToSend = xDocToSend.Element(docSubject.DataNodeName).Element(docSubject.SubjectTypeValue);
                    xDataToSend.Add(docSubject.TitleXml);
                    xDataToSend.Add(new XElement("Events"));

                    if (!string.IsNullOrEmpty(docSubject.SubjectCode))
                    {
                        var subjectCodeWarnings = DocumentHelper.SubjectCodeCheck(docSubject.SubjectCode);
                        if (!subjectCodeWarnings.IsNullOrEmpty())
                        {
                            foreach (var warningMessage in subjectCodeWarnings)
                            {
                                notificationOfAcceptance.AddWarning(warningMessage);
                            }
                            xDataToSend?.Element("Title")?.SetAttributeValue("subjectCode", null);
                        }
                    }

                    XElement tstDoc = XElement.Load(xDocToSend.CreateReader());

                    foreach (var docEvent in docSubject)
                    {
                        EventsReceived++;

                        if (docEvent.Xml == null)
                        {
                            notificationOfAcceptance.AddError(document.CreateError($"Ошибка чтения тега Events"));
                            return;
                        }

                        var xsdEventError = XsdHelper.ValidateEventByXsd(docEvent.Xml, _docService.DocHeader.SchemaVersion, docSubject.SubjectType);

                        if (xsdEventError != null)
                        {
                            EventsRejected++;

                            var eventXsdErrorMessage = $"Событие не соответствует xsd: {xsdEventError}";

                            var acceptanceError = document.CreateError(docEvent.Xml, docSubject.SubjectCode, docSubject.SubjectID, eventXsdErrorMessage);

                            notificationOfAcceptance.AddError(acceptanceError);
                            continue;
                        }

                        string pattern = @"^([01][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])[+-]([01][0-9]|2[0-3]):([0-5][0-9])$";
                        Regex regex = new(pattern);

                        List<INotificationOfAcceptanceError> eventErrors = [];

                        IEventData eventObject = objectDeserializer.GetEventObject(docEvent.Xml, docSubject.SubjectType);

                        #region Ограничение дат снизу Событие
                        dateCheckError = XsdHelper.ValidateEventByXsd(docEvent.Xml, _docService.DocHeader.SchemaVersion, docSubject.SubjectType, SchemaSetType.DateTimeCheck);

                        if (dateCheckError != null)
                        {
                            var dateError = DateErrorParser.Parse(dateCheckError);
                            if (dateError != null)
                            {
                                eventErrors.Add(document.CreateError(eventObject, null, dateError.Field, dateError.Value, "Дата меньше минимально допустимого значения"));
                            }
                        }
                        #endregion Ограничение дат снизу Событие

                        if (!_docService.DocHeader.SchemaVersion.Equals("4.1") && eventObject.eventDateTime > document.dateTimeDoc)
                        {
                            eventErrors.Add(document.CreateError(eventObject, null, "eventDateTime", eventObject.eventDateTime, "Дата и время события eventDateTime позже, чем исходящая дата и время регистрации документа, содержащего кредитную информацию"));
                        }

                        eventErrors.AddRange(eventObject.Check(sourceItem, titleObject));

                        if (eventErrors.Count > 0)
                        {
                            eventErrors.ForEach(x => x.SubjectCode = titleObject.subjectCode);
                            eventErrors.ForEach(x => x.SubjectID = titleObject.subjectID);

                            notificationOfAcceptance.AddErrors(eventErrors);

                            EventsRejected++;
                            notificationOfAcceptance.DocInfoExt.eventsRejected = EventsRejected;
                        }
                        else
                        {
                            EventsAccepted++;
                            notificationOfAcceptance.DocInfoExt.eventsAccepted = EventsAccepted;

                            //проверка наличия события в бд, для повтороной обработки пакета
                            if (!uploadOrders.Contains(docEvent.OrderNum))
                            {
                                xDataToSend.Element("Events").Add(docEvent.Xml);
                            }
                        }

                    }

                    if (!xDataToSend.Element("Events").IsEmpty)
                    {
                        xDocToSend?.SetAttributeValue("upload_key_id", activeUpload.KeyId);
                        xDocToSend?.SetAttributeValue("document_file_name", uploadDocInfo.FileName);
                        xDocToSend?.SetAttributeValue("upload_created", activeUpload.Created.ToString("s"));

                        string? titleHash = HashHelper.GetTitleHash(xDataToSend);

                        //если источник приоритетной загрузки и событие не антифрод

                        if (!docSubject.SubjectType.Equals(SubjectType.AF) && PriorityLoading(uploadDocInfo.SourceId, uploadDocInfo.FileName, titleHash, out string? topicName))
                        {
                            topicName = string.IsNullOrEmpty(topicName) ? GetTopicName(activeUpload.UploadInfo.UserInfo.ClientId) : topicName;

                            _logger.LogDebug("Приоритетная загрузка по имени файла Топик: {topicName}", topicName);

                            //информируем бд, что событие приоритетной загрузки
                            xDocToSend?.SetAttributeValue("antifraud", true);
                        }
                        //приоритетная загрузка событий антифрод
                        else if (docSubject.SubjectType.Equals(SubjectType.AF))
                        {
                            topicName = GetNextPriorityTopicByTitleHash(titleHash);

                            _logger.LogDebug("Антифрод приоритетная загрузка событий Топик: {topicName}", topicName);
                        }
                        else
                        {
                            // проверяем что источник загружаеться в сплит
                            if (SplitLegals.ContainsKey(activeUpload.UploadInfo.UserInfo.ClientId))
                            {
                                topicName = GetNextSplitTopicByTitleHash(titleHash);

                                _logger.LogDebug("Загрузка источника в Топик парралельной загрузки: {topicName}", topicName);
                            }
                            else
                            {
                                topicName = GetTopicName(activeUpload.UploadInfo.UserInfo.ClientId);

                                _logger.LogDebug("Текущая загрузка событий в Топик: {topicName}", topicName);
                            }
                        }

                        XDocument xDocument = new(xDocToSend);

                        var subjectEventCount = (int)(double)xDocument.XPathEvaluate("count(/Document/Data//Events/*)");

                        var eventsMaxCount = _config.GetValue<int>("SplitSubjectEvents:EventsMaxCount");

                        if (subjectEventCount > eventsMaxCount)
                        {
                            foreach (var xSplitDocSend in GetSplitSubjectEvents(xDocument, subjectEventCount))
                            {
                                XElement xSplitTosend = XElement.Load(_docService.DocHeader.Xml.CreateReader());
                                xSplitTosend.Add(new XElement(docSubject.DataNodeName, new XElement(docSubject.LocalName)));
                                XElement xSplitData = xSplitTosend.Element(docSubject.DataNodeName).Element(docSubject.SubjectTypeValue);
                                xSplitData.Add(docSubject.TitleXml);
                                xSplitData.Add(new XElement(xSplitDocSend));

                                xSplitTosend?.SetAttributeValue("upload_key_id", activeUpload.KeyId);
                                xSplitTosend?.SetAttributeValue("document_file_name", uploadDocInfo.FileName);
                                xSplitTosend?.SetAttributeValue("upload_created", activeUpload.Created.ToString("s"));

                                KafkaServiceProduce(uploadDocInfo, docSubject.Num, topicName ?? _topicNameDefault, xSplitTosend);
                            }
                        }
                        else
                        {
                            KafkaServiceProduce(uploadDocInfo, docSubject.Num, topicName ?? _topicNameDefault, xDocToSend);
                        }
                    }
                }
            }
        }

        private IEnumerable<XElement> GetSplitSubjectEvents(XDocument xDocument, int eventsMaxCount)
        {
            var eventsSplitCount = _config.GetValue<int>("SplitSubjectEvents:EventsToSplitCount");

            var eventsToSplit = (int)(eventsMaxCount / (int)(eventsMaxCount / eventsSplitCount));
            var xEvents = new XElement("Events");
            int eventNum = 0;

            foreach (var xEvent in xDocument.XPathSelectElements("//Events/*"))
            {
                eventNum++;
                xEvents.Add(xEvent);

                if (eventsToSplit.Equals(eventNum))
                {
                    yield return xEvents;
                    eventNum = 0;
                    xEvents = new XElement("Events");
                }
            }

            yield return xEvents;
        }

        private string GetNextSplitTopic()
        {
            if (SplitTopics.TryDequeue(out string? topicName))
            {
                SplitTopics.Enqueue(topicName);

                return topicName;
            }

            throw new Exception("Критическая ошибка, очередь параллельной обработки пустая");
        }
        private string? GetNextSplitTopicByTitleHash(string? titleHash)
        {
            if (string.IsNullOrEmpty(titleHash))
            {
                return GetNextSplitTopic();
            }
            else if (SplitLegalsUsed.TryGetValue(titleHash, out string? topicName))
            {
                return topicName;
            }
            else
            {
                topicName = GetNextSplitTopic();

                if (topicName != null)
                {
                    SplitLegalsUsed.Add(titleHash, topicName);
                }

                return topicName;
            }
        }

        private string GetNextPriorityTopic()
        {
            if (PriorityTopics.TryDequeue(out var topicName))
            {
                PriorityTopics.Enqueue(topicName);

                return topicName;
            }

            return _topicNameDefault;
        }

        private string GetNextPriorityTopicByTitleHash(string? titleHash)
        {
            //титтульная часть пустая (а вдруг)
            if (string.IsNullOrEmpty(titleHash))
            {
                //следующий приоритентный топик
                return GetNextPriorityTopic();
            }
            else
            {
                //титульная часть загружалась ранее
                if (_appParam.UsedPrioritytopics.TryGetValue(titleHash, out string? topic))
                {
                    return topic;
                }
                //титульная часть ранее не загружалась
                else
                {
                    string topicName = GetNextPriorityTopic();

                    _appParam.UsedPrioritytopics.Add(titleHash, topicName);

                    return topicName;
                }
            }
        }

        private void RejectedAllEvents(IUploadDocumentHeader docHeader)
        {
            SubjectsReceived = docHeader.SubjectsCount;
            EventsReceived = docHeader.GroupBlocksCount;
            EventsAccepted = 0;
            EventsRejected = docHeader.GroupBlocksCount;
        }

        private void RejectedAllEvents(BaseXmlCheckResult xmlCheckResult)
        {
            SubjectsReceived = (ulong)xmlCheckResult.SubjectCount;
            EventsReceived = (ulong)xmlCheckResult.EventCount;
            EventsAccepted = 0;
            EventsRejected = (ulong)xmlCheckResult.EventCount;
        }

        public bool CheckLegal(IUploadDocumentHeader docHeader, ActiveUpload activeUpload, out List<string> errorMessage)
        {
            bool ret = true;
            errorMessage = [];

            if (_appParam.FileSystemUse)
            {
                return true;
            }

            var skipCheckSourceInn = _config.GetSection("Validation").GetSection("SkipCheckSourceInn").Get<int[]>() ?? [];

            if (skipCheckSourceInn.Contains(activeUpload.UploadInfo.UserInfo.ClientId))
            {
                return true;
            }

            if (string.IsNullOrEmpty(_docService.DocHeader.Inn) && string.IsNullOrEmpty(_docService.DocHeader.Ogrn))
            {
                errorMessage.Add("Отсутствуют сведения, предусмотренные пунктом 1.8 приложения 3 к Положению 758-П, 1.8.1. Идентификационный номер налогоплательщика [/Document/@inn] (ИНН) и основной государственный регистрационный номер [/Document/@ogrn] (ОГРН) источника.");
                _logger.LogError(errorMessage.Last());

                if (!string.IsNullOrEmpty(_docService.DocHeader.SourceID))
                {
                    errorMessage.Add("Договором об оказании информационных услуг между источником и бюро не установлен иной способ определения источника.");
                    _logger.LogError(errorMessage.Last());
                }

                return false;
            }

            if (!string.IsNullOrEmpty(_docService.DocHeader.Inn) && activeUpload.SourceItn != null && !activeUpload.SourceItn.Equals(_docService.DocHeader.Inn))
            {
                errorMessage.Add("Указанный ИНН не найден в базе бюро");
                ret = false;

                _logger.LogError("Указанный ИНН {docHeaderInn} не найден, в базе бюро {SourceItn}", _docService.DocHeader.Inn, activeUpload.SourceItn);
            }

            if (!string.IsNullOrEmpty(_docService.DocHeader.Ogrn) && activeUpload?.SourcePsrn != null && !activeUpload.SourcePsrn.Equals(_docService.DocHeader.Ogrn))
            {
                errorMessage.Add("Указанный ОГРН не найден в базе бюро");

                _logger.LogError("Указанный ОГРН {docOgrn} не найден, в базе бюро {SourcePsrn}", _docService.DocHeader.Ogrn, activeUpload.SourcePsrn);

                ret = false;
            }
            return ret;
        }

        private void KafkaServiceProduce(UploadDocInfo uploadDocInfo, ulong subjectNum, string topicName, XElement xUniDocSend)
        {
            _logger.LogDebug("Отправляем сообщение Топик: {topicName}", topicName);

            var useCompress = _config.GetValue<bool>("Settings:Compress:CompressMessage", true);
            var compressionLevel = _config.GetValue<CompressionLevel>("Settings:Compress:CompressionLevel", CompressionLevel.Optimal);

            using StringWriter messageWriter = new();

            messageWriter.Write(useCompress ?
                CompressHelper.Compress(xUniDocSend.ToString(SaveOptions.DisableFormatting), compressionLevel) :
                xUniDocSend.ToString(SaveOptions.DisableFormatting));

            messageWriter.Close();

            if (_appParam.FileSystemUse)
            {
                string directorySendToDb = Path.Combine(_appParam.UploadPath, "send_to_db");
                Directory.CreateDirectory(directorySendToDb);

                string uploadFileName = uploadDocInfo.FileName;

                string fileNameFullPath = Path.Combine(directorySendToDb, $"({subjectNum.ToString().PadLeft(6, '0')})" + uploadFileName);

                if (useCompress)
                {
                    using StreamWriter streamWriter = new(Path.ChangeExtension(fileNameFullPath, "pack"));
                    streamWriter.Write(messageWriter);
                    streamWriter.Close();
                }
                else
                {
                    xUniDocSend.Save(fileNameFullPath);
                }
                return;
            }
            else
            {
                _kafkaAdapter.ProduceToTopic(topicName, messageWriter.ToString(), useCompress);
            }
        }

        private string GetTopicName(int srcId)
        {
            if (LegalTopics.ContainsKey(srcId))
            {
                string topicName = LegalTopics[srcId];

                return topicName;
            }
            else
            {
                if (LegalTopics.ContainsKey(LegalTopicDefault))
                {
                    return LegalTopics[LegalTopicDefault];
                }
                else
                {
                    return LegalTopics.First().Value;
                }
            }
        }

        private bool PriorityLoading(int srcId, string fileName, string? titleHash, out string? topicName)
        {
            if (IsPriorityPrefix(srcId, fileName))
            {
                topicName = GetNextPriorityTopicByTitleHash(titleHash);

                return true;
            }

            topicName = null;
            return false;
        }

        private bool IsPriorityPrefix(int srcId, string fileName)
        {
            if (PriorityOrderFileNamePrefix.TryGetValue(srcId, out string? priorityPrefix))
            {
                return FileHelper.HasPriorityOrder(fileName, priorityPrefix);
            }

            return false;
        }
    }
}
