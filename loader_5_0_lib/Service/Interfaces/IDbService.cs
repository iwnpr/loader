using loader_5_0_lib.Classes;
using loader_5_0_lib.Service.Implementation;
using System.Xml.Linq;
using Xsd;

namespace loader_5_0_lib.Service.Interfaces
{
    public interface IDbService
    {
        void UpdateDocStatus(ActiveUpload activeUpload, int state, XElement? xReceipt, XElement? xAcceptance);

        void SaveDbContext();

        void AddToDbContextReceipt(long uploadKeyId, INotificationOfReceipt receipt);

        void AddToDbContextAcceptance(long uploadKeyId, INotificationOfAcceptance acceptance);

        void UpdateNotificationOfReceipt(long uploadKeyId, XElement xReceipt);

        UploadPackageInfoExt? GetPreviousPackages(long legalPersonId, DateTime dateDoc, DateTime created, string outgoingDocDateTime, string outgoingDocNumber);
    }
}
