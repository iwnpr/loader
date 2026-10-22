
using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Receipt
{
    public partial class NotificationOfReceiptErrors : INotificationOfReceiptErrors
    {
        public NotificationOfReceiptErrors()
        {
        }

        public NotificationOfReceiptErrors(refReceiptErrorType errorCode, string errorMessage)
        {
            this.errorCode = errorCode;
            this.errorMessageField = errorMessage;
        }

        public int ErrorCode
        {
            get
            {
                return int.Parse(errorCode.GetXmlEnumAttribute()!);
            }
        }

        [XmlIgnore]
        public string ErrorMessage => this.errorMessage;
    }
}