namespace loader_5_0_lib.NotificationEF
{
    public class NotificationOfReceiptErrorEF
    {
        public long Id { get; set; }
        public long NotificationOfReceiptId { get; set; }
        public NotificationOfReceiptEF NotificationOfReceipt { get; set; }
        public ushort ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
        public NotificationOfReceiptErrorEF() { }
        public NotificationOfReceiptErrorEF(int ErrorCode, string? errorMessage)
        {
            this.ErrorCode = (ushort)ErrorCode;
            ErrorMessage = errorMessage;
        }
    }
}