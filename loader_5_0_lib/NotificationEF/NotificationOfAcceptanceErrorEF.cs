using Xsd;

namespace loader_5_0_lib.NotificationEF
{
    public class NotificationOfAcceptanceErrorEF
    {
        public long Id { get; set; }
        public long NotificationOfAcceptanceId { get; set; }
        public NotificationOfAcceptanceEF NotificationOfAcceptance { get; set; }
        public string? EventDate { get; set; }
        public string? EventTime { get; set; }
        public string? EventDateTime { get; set; }
        public string? ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Uid { get; set; }
        public ulong? OrderNum { get; set; }
        public ulong? OrderNum_3_2 { get; set; }
        public string? SubjectCode { get; set; }
        public string? FieldValue { get; set; }
        public string? SubjectID { get; set; }
        public string? EventName { get; set; }
        public string? BlockName { get; set; }
        public string? FieldName { get; set; }
        public NotificationOfAcceptanceErrorEF() { }
        public NotificationOfAcceptanceErrorEF(INotificationOfAcceptanceError na, string schemaVersion)
        {

            EventDateTime = na.eventDateTimeSpecified ? na.eventDateTime.ToString("yyyy-MM-ddTHH:mm:sszzz") : null;

            ErrorCode = na.ErrorCode.ToString();

            ErrorMessage = na.ErrorMessage;

            Uid = na.uid;

            OrderNum = na.orderNumSpecified ? na.orderNum : null;

            OrderNum_3_2 = na.orderNum_3_2Specified ? na.orderNum_3_2 : null;

            SubjectCode = na.SubjectCode;

            SubjectID = na.SubjectID;

            EventName = na.eventNameSpecified ? na.eventName.ToString() : null;

            BlockName = na.BlockNameSpecified ? na.BlockName.ToString() : null;

            FieldName = na.FieldName;

            FieldValue = na.FieldValue;
        }
    }
}
