using loader_utils;
using System.Xml.Serialization;
using xsd_base.Notifications;

namespace Xsd.VersionFour.Acceptance
{
    public partial class NotificationOfAcceptance : INotificationOfAcceptance
    {
        public NotificationOfAcceptance()
        {
            DocInfo = new();
        }

        [XmlIgnore]
        public DateTime SendingDateTimeExt { get; set; }

        [XmlIgnore]
        public string? SchemaVersion { get => schemaVersion.GetXmlEnumAttribute(); }

        [XmlIgnore]
        public INotificationOfAcceptanceDocInfo DocInfoExt { get => docInfoField; }

        [XmlIgnore]
        public string[] WarningsExt => this.warningsField ?? [];

        [XmlIgnore]
        public INotificationOfAcceptanceError[] ErrorsExt { get => errorsField ?? []; }

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
