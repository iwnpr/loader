namespace Xsd
{
    public interface IEventData
    {
        public ulong orderNum
        {
            get;
        }

        public string? Uid
        {
            get;
        }

        public refEventNameType EventName
        {
            get;
        }

        public DateTimeOffset eventDateTime
        {
            get;
        }

        public DateTime eventDateTimeLocal
        {
            get;
        }

        public List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title);

        public INotificationOfAcceptanceError CreateError(refBlockNameType? blockName, string? fieldName, object? fieldValue, string errorMessage);

    }
}
