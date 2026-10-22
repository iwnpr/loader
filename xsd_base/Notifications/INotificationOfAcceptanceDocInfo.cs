using System.Xml.Linq;
using Xsd;

namespace xsd_base.Notifications
{
    public interface INotificationOfAcceptanceDocInfo
    {
        refDataAcceptStatusType dataReceivedStatus { get; set; }
        DateTimeOffset outgoingDocDateTime { get; set; }
        string outgoingDocNumber { get; set; }
        DateTimeOffset incomingDocDateTime { get; set; }
        string incomingDocNumber { get; set; }
        DateTimeOffset docDateTime { get; set; }
        string fileName { get; set; }
        string fileHash { get; set; }
        ulong subjectsReceived { get; set; }
        ulong eventsReceived { get; set; }
        ulong eventsAccepted { get; set; }
        ulong eventsRejected { get; set; }
        string? inn { get; }
        string? ogrn { get; }
        string? sourceId { get; }

        void FillFields(XElement xDocHeader);
        void FillFields(XElement xDocHeader, string fileName, string fileHash, DateTime uploadCreated);
        void ReceiveSubjects(ulong subjectsCount);
        void ReceiveEvents(ulong eventsToReceive);
        void AcceptEvents(ulong eventsToAccept);
        void RejectEvents(ulong eventsCount);
    }
}
