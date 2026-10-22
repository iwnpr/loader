namespace Xsd
{
    public interface INotificationOfReceiptErrors
    {
        public int ErrorCode { get; }

        public string ErrorMessage { get; }
    }
}