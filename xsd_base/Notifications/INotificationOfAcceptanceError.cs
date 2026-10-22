namespace Xsd
{
    public interface INotificationOfAcceptanceError
    {
        DateTimeOffset eventDateTime { get; }

        public bool eventDateTimeSpecified { get; }

        ushort ErrorCode { get; }

        string? ErrorMessage { get; }

        string? uid { get; set; }

        bool orderNumSpecified { get; set; }

        ulong orderNum { get; set; }

        bool orderNum_3_2Specified { get; set; }

        ulong orderNum_3_2 { get; set; }

        string? SubjectCode { get; set; }

        string? SubjectID { get; set; }

        bool eventNameSpecified { get; }

        public refEventNameType eventName { get; set; }

        bool BlockNameSpecified { get; }

        refBlockNameType BlockName { get; }

        string? FieldName { get; }

        string? FieldValue { get; }

        bool IsCriticalTitleError { get; }

        bool IgnoreErrorIn32 { get; }
    }
}
