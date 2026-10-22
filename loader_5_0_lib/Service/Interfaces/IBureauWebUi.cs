using loader_5_0_lib.Classes;
using System.Xml.Linq;

namespace loader_5_0_lib.Service.Interfaces
{
    public interface IBureauWebUi
    {
        public ActiveUpload? GetActiveUploadRequest();
        void UpdateUploadingXml(long uploadKeyId, XElement uploadingXml);
        public void GetTickets(long uploadKeyId);
        public void UpdateDocStatus(long uploadKeyId, int state, string? sReceipt, string? sAcceptance);
        void UpdateNotificationOfReceipt(long uploadKeyId, XElement xReceipt);
        UploadPackageInfoExt? GetPreviousPackages(long legalPersonId, DateTime dateDoc, DateTime created, string outgoingDocDateTime, string outgoingDocNumber);
    }
}
