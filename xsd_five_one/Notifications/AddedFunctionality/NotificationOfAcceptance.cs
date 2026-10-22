using loader_utils;
using System.Xml.Serialization;
using xsd_base.Notifications;

namespace Xsd.VersionFiveOne.Acceptance
{
    public partial class NotificationOfAcceptance : INotificationOfAcceptance
    {
        public NotificationOfAcceptance()
        {
            docInfoField = new();
        }

        [XmlIgnore]
        public string? SchemaVersion { get => schemaVersion.GetXmlEnumAttribute(); }

        [XmlIgnore]
        public INotificationOfAcceptanceDocInfo DocInfoExt { get => docInfoField; }

        [XmlIgnore]
        public INotificationOfAcceptanceError[] ErrorsExt { get => errorsField; }

        [XmlIgnore]
        public string[] WarningsExt => this.warningsField;

        public void AddError(INotificationOfAcceptanceError error)
        {
            errorsField = [.. errorsField ?? [], (NotificationOfAcceptanceError)error];
        }

        public void AddErrors(IEnumerable<INotificationOfAcceptanceError> errors)
        {
            errorsField = [.. errorsField ?? [], .. errors.Select(x => (NotificationOfAcceptanceError)x).ToArray()];
        }

        public void AddWarning(string warningMessage)
        {
            warningsField = [.. warningsField ?? [], .. new string[] { warningMessage }];
        }
    }
}
