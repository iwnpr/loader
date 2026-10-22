using loader_5_0_lib.Classes;
using loader_5_0_lib.DataBase;
using loader_5_0_lib.NotificationEF;
using loader_5_0_lib.Service.Interfaces;
using System.Xml.Linq;
using Xsd;

namespace loader_5_0_lib.Service.Implementation
{
    public class DbService(MSSQLDbContext mSSQLDbContext, IBureauWebUi bureauWebUi,
        IBureauData bureauData, IAppParam appParam) : IDbService
    {
        private readonly MSSQLDbContext _mSSQLDbContext = mSSQLDbContext;
        private readonly IAppParam _appParam = appParam;
        private readonly IBureauWebUi _bureauWebUi = bureauWebUi;
        private readonly IBureauData _bureauData = bureauData;

        public void SaveDbContext()
        {
            if (!_appParam.FileSystemUse)
            {
                _mSSQLDbContext.SaveChanges();
            }
        }

        public void AddToDbContextReceipt(long uploadKeyId, INotificationOfReceipt receipt)
        {
            NotificationOfReceiptEF notificationOfReceiptEF = new();
            notificationOfReceiptEF.Load(uploadKeyId, receipt);
            _mSSQLDbContext.NotificationOfReceipt.Add(notificationOfReceiptEF);
        }

        public void AddToDbContextAcceptance(long uploadKeyId, INotificationOfAcceptance acceptance)
        {
            NotificationOfAcceptanceEF notificationAcceptanceEF = new();
            notificationAcceptanceEF.Load(uploadKeyId, acceptance);
            _mSSQLDbContext.NotificationOfAcceptance.Add(notificationAcceptanceEF);
        }

        public void UpdateDocStatus(ActiveUpload activeUpload, int state, XElement? xReceipt, XElement? xAcceptance)
        {
            if (_appParam.FileSystemUse)
            {
                string uploadFileName = activeUpload.UploadInfo.DocsInfo.First().Value.FileName;

                string fileFullNotificationOfReceipt = Path.Combine(activeUpload.FileSystemProcessingPath, "NotificationOfReceipt_" + uploadFileName);

                if (xReceipt != null)
                {
                    xReceipt.Save(fileFullNotificationOfReceipt);
                }

                if (xAcceptance != null)
                {
                    string fileFullNameNotificationOfAcceptance = Path.Combine(activeUpload.FileSystemProcessingPath, "NotificationOfAcceptance_" + uploadFileName);
                    xAcceptance.Save(fileFullNameNotificationOfAcceptance);
                }
            }
            else
            {
                _bureauWebUi.UpdateDocStatus(activeUpload.KeyId, 3, xReceipt?.ToString(), xAcceptance?.ToString());
            }
        }

        public void UpdateNotificationOfReceipt(long uploadKeyId, XElement xReceipt)
        {
            _bureauWebUi.UpdateNotificationOfReceipt(uploadKeyId, xReceipt);
        }

        public UploadPackageInfoExt? GetPreviousPackages(long legalPersonId, DateTime dateDoc, DateTime created, string outgoingDocDateTime, string outgoingDocNumber)
        {
            return _bureauWebUi.GetPreviousPackages(legalPersonId, dateDoc, created, outgoingDocDateTime, outgoingDocNumber);
        }
    }
}
