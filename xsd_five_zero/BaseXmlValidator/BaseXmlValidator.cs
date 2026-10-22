using System.Xml;
using System.Xml.Linq;
using Xsd.VersionFiveZero.Acceptance;

namespace Xsd.VersionFiveZero.Main
{
    public class BaseXmlValidator(XmlReader reader) : xsd_base.BaseXmlValidation.BaseXmlValidator(reader)
    {
        protected override INotificationOfAcceptanceError GetError(string errorType, string errorMessage)
        {
            return new NotificationOfAcceptanceError(errorType, errorMessage);
        }

        protected override INotificationOfAcceptanceError GetError(IEventData eventData, string fieldName, object fieldValue, string errorMessage)
        {
            return new NotificationOfAcceptanceError(eventData, null, fieldName, fieldValue, errorMessage);
        }

        protected override IEventData GetEventObject(XElement eventElement, SubjectType subjectType)
        {
            return new ObjectDeserializer().GetEventObject(eventElement, subjectType);
        }
    }
}
