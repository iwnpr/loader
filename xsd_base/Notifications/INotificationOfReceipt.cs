using System.Xml.Linq;

namespace Xsd
{
    public interface INotificationOfReceipt
    {
        string SchemaVersion { get; }
        string? Inn { get; }
        string? Ogrn { get; }
        string? SourceId { get; }
        string outgoingDocDateTime { get; }
        string OutgoingDocNumber { get; }
        string AcceptanceStatus { get; }
        INotificationOfReceiptErrors[] Errors { get; }
        DateTimeOffset? incomingDocDateTime { get; }
        string? IncomingDocNumber { get; }
        string FileName { get; }
        string FileHash { get; }
        DateTimeOffset docDateTime { get; }
        DateTimeOffset sendingDateTime { get; set; }
        bool HasErrors { get { return Errors.Count() > 0; } }
        void AddError(int errorCode, string errorMessage);
        void AddErrors(int errorCode, List<string> errorMessages);
        void FillFields(IDocument document);
        void FillFields(XElement xDocHeader);
        void FillFields(XElement xDocHeader, string fileName, string fileHash, DateTime uploadCreated);
    }
}
