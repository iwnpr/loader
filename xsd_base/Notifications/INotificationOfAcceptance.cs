using xsd_base.Notifications;

namespace Xsd
{
    public interface INotificationOfAcceptance
    {
        string? SchemaVersion { get; }

        DateTimeOffset sendingDateTime { get; set; }

        INotificationOfAcceptanceDocInfo DocInfoExt { get; }

        string[] WarningsExt { get; }

        INotificationOfAcceptanceError[] ErrorsExt { get; }

        public void AddError(INotificationOfAcceptanceError error);

        public void AddErrors(IEnumerable<INotificationOfAcceptanceError> errors);

        void AddWarning(string warningMessage);

    }
}