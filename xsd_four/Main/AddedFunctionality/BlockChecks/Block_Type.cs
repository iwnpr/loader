namespace Xsd.VersionFour.Main
{
    public class Block_Type
    {
        protected EventData? eventData;
        protected refBlockNameType? blockName;
        protected List<INotificationOfAcceptanceError> errorList;

        protected void InitCheck(EventData? eventData)
        {
            this.eventData = eventData;
            errorList = new List<INotificationOfAcceptanceError>();
        }

        protected void AddError(string fieldName, object? fieldValue, string errorMessage, bool ignoreErrorIn32 = false)
        {
            errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, blockName, fieldName, fieldValue, errorMessage, ignoreErrorIn32));
        }
    }
}
