using System.Xml.Linq;

namespace Xsd
{
    public interface IDocument
    {
        public string inn
        {
            get;
        }

        public string ogrn
        {
            get;
        }

        public string sourceID
        {
            get;
        }

        public string regNumberDoc
        {
            get;
        }

        public DateTimeOffset dateTimeDoc
        {
            get;
        }

        public DateTime dateTimeDocLocal
        {
            get;
        }

        public ulong subjectsCount
        {
            get;
        }

        public ulong groupBlocksCount
        {
            get;
        }

        public string regNumberDocInaccept
        {
            get;
        }

        public INotificationOfAcceptanceError CreateError(string errorMessage);

        public INotificationOfAcceptanceError CreateError(XElement eventXml, string? subjectCode, string? subjectID, string errorMessage);

        public INotificationOfAcceptanceError CreateError(ulong? orderNum, string? eventName, refBlockNameType? blockName, string? fieldName, object? fieldValue, string? subjectCode, string? subjectID, string errorMessage);

        public INotificationOfAcceptanceError CreateError(IEventData? eventData, refBlockNameType? blockName, string? fieldName, object? fieldValue, string errorMessage);
    }
}


