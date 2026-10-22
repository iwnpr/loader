namespace Xsd.VersionFiveZero.Main
{
    public class ErrorData(refBlockNameType blockName, IEventData? eventData = null)
    {
        protected List<INotificationOfAcceptanceError> errorList = [];

        public List<INotificationOfAcceptanceError> ErrorList
        {
            get { return errorList; }
        }

        public void AddError(string fieldName, object? fieldValue, string errorMessage, bool ignoreErrorIn32 = false)
        {
            errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, blockName, fieldName, fieldValue, errorMessage, ignoreErrorIn32));
        }

        public void AddError(refBlockNameType blockName, string fieldName, object? fieldValue, string errorMessage, bool ignoreErrorIn32 = false)
        {
            errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, blockName, fieldName, fieldValue, errorMessage, ignoreErrorIn32));
        }
    }
}
