using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class Document : IDocument
    {
        [XmlIgnore]
        public DateTime dateTimeDocLocal
        {
            get
            {
                return dateTimeDoc.DateTime;
            }
        }

        public INotificationOfAcceptanceError CreateError(IEventData? eventData, refBlockNameType? blockName, string? fieldName, object? fieldValue, string errorMessage)
        {
            return new Acceptance.NotificationOfAcceptanceError(eventData, blockName, fieldName, fieldValue, errorMessage);
        }

        public INotificationOfAcceptanceError CreateError(string errorMessage)
        {
            return new Acceptance.NotificationOfAcceptanceError(errorMessage);
        }
        public INotificationOfAcceptanceError CreateError(ulong? orderNum, string? eventName, refBlockNameType? blockName, string? fieldName, object? fieldValue, string? subjectCode, string? subjectID, string errorMessage)
        {
            return new Acceptance.NotificationOfAcceptanceError(orderNum, eventName, blockName, fieldName, fieldValue, subjectCode, subjectID, errorMessage);
        }

        public INotificationOfAcceptanceError CreateError(XElement xEvent, string? subjectCode, string? subjectID, string errorMessage)
        {
            return new Acceptance.NotificationOfAcceptanceError(xEvent, subjectCode, subjectID, errorMessage);
        }
    }
}