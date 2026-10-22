
using loader_utils;

namespace Xsd.VersionFiveOne.Receipt
{
    public partial class NotificationOfReceiptErrors : INotificationOfReceiptErrors
    {
        public NotificationOfReceiptErrors()
        {
        }
        public NotificationOfReceiptErrors(refReceiptErrorType errorCode, string errorMessage)
        {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;
        }

        public int ErrorCode
        {
            get
            {
                return int.Parse(errorCode.GetXmlEnumAttribute()!);
            }
        }

        public string ErrorMessage => errorMessage;
    }
}