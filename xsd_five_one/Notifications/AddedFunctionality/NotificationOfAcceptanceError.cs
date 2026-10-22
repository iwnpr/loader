using loader_utils;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Xsd.VersionFiveOne.Acceptance
{
    public partial class NotificationOfAcceptanceError : INotificationOfAcceptanceError
    {

        public NotificationOfAcceptanceError()
        {
        }

        bool _isCriticalTitleError;

        [XmlIgnore]
        public bool IsCriticalTitleError
        {
            get
            {
                return _isCriticalTitleError;
            }
        }

        bool _ignoreErrorIn32;

        [XmlIgnore]
        public bool IgnoreErrorIn32
        {
            get
            {
                return _ignoreErrorIn32;
            }
        }

        [XmlIgnore]
        public ushort ErrorCode => ushort.TryParse(this.errorCodeField.GetXmlEnumAttribute(), out ushort errCode) ? errCode : throw new Exception("errorCode {this.errorCode}");

        [XmlIgnore]
        public string? ErrorMessage => this.errorMessageField;

        [XmlIgnore]
        public string? Uid => this.uidField;

        [XmlIgnore]
        public bool OrderNumSpecified { get => this.orderNumFieldSpecified; set => this.orderNumFieldSpecified = value; }

        [XmlIgnore]
        public ulong OrderNum { get => this.orderNumField; set => this.orderNumField = value; }

        [XmlIgnore]
        public bool OrderNum_3_2Specified { get => this.orderNum_3_2FieldSpecified; set => this.orderNum_3_2FieldSpecified = value; }

        [XmlIgnore]
        public ulong OrderNum_3_2 { get => this.orderNum_3_2Field; set => this.orderNum_3_2Field = value; }

        [XmlIgnore]
        public string? SubjectCode { get => this.subjectCodeField; set => this.subjectCodeField = value; }

        [XmlIgnore]
        public string? SubjectID { get => this.subjectIDField; set => this.subjectIDField = value; }

        [XmlIgnore]
        public bool EventNameSpecified => this.eventNameFieldSpecified;

        [XmlIgnore]
        public refEventNameType EventName { get => this.eventNameField; set => this.eventNameField = value; }

        [XmlIgnore]
        public bool BlockNameSpecified => this.blockNameFieldSpecified;

        [XmlIgnore]
        public refBlockNameType BlockName => this.blockNameField;

        [XmlIgnore]
        public string? FieldName => this.fieldNameField;

        [XmlIgnore]
        public string? FieldValue => this.fieldValueField;

        public NotificationOfAcceptanceError(string errorMessage)
        {
            this.errorCode = refAcceptanceErrorType.Item2;
            this.errorMessage = errorMessage;
        }

        public NotificationOfAcceptanceError(string errorCode, string errorMessage)
        {
            this.errorCode = EnumHelper.GetCode<refAcceptanceErrorType>(errorCode);
            this.errorMessage = errorMessage;
        }

        public NotificationOfAcceptanceError(ulong? orderNum, string? eventName, refBlockNameType? blockName, string? fieldName, object? fieldValue, string? subjectCode, string? subjectID, string errorMessage)
        {
            this.errorCode = refAcceptanceErrorType.Item1;
            this.errorMessage = errorMessage;

            if (orderNum != null)
            {
                this.orderNumSpecified = true;
                this.orderNum = (ulong)orderNum;
            }

            if (!string.IsNullOrEmpty(eventName))
            {
                this.eventNameSpecified = true;
                this.eventName = Enum.Parse<refEventNameType>(eventName);
            }

            this.subjectCode = subjectCode;
            this.subjectID = subjectID;
            if (blockName != null)
            {
                this.blockNameSpecified = true;
                this.blockName = blockName.Value;
            }

            if (fieldName != null)
            {
                this.fieldName = fieldName;
            }

            if (fieldValue != null)
            {
                if (fieldValue is DateTime dateTime)
                {
                    if (dateTime == DateTime.MinValue)
                    {
                        this.fieldValue = null;
                    }
                    else
                    {
                        this.fieldValue = (dateTime).ToString("yyyy-MM-dd");
                    }
                }
                else if (fieldValue is DateTimeOffset dateTimeOffset)
                {
                    if (dateTimeOffset == DateTimeOffset.MinValue)
                    {
                        this.fieldValue = null;
                    }
                    else
                    {
                        this.fieldValue = dateTimeOffset.ToString("yyyy-MM-ddTHH:mm:sszzz");
                    }
                }
                else if (fieldValue is Enum enumValue)
                {
                    this.fieldValue = enumValue.GetXmlEnumAttribute();
                }
                else
                {
                    this.fieldValue = fieldValue?.ToString();
                }
            }
        }

        public NotificationOfAcceptanceError(IEventData? eventData, refBlockNameType? blockName, string errorMessage, bool ignoreErrorIn32 = false, bool isCriticalTitleError = false) : this(eventData, null, null, blockName, errorMessage, ignoreErrorIn32, isCriticalTitleError) { }

        public NotificationOfAcceptanceError(IEventData? eventData, refBlockNameType? blockName, string? fieldName, object? fieldValue, string errorMessage, bool ignoreErrorIn32 = false, bool isCriticalTitleError = false)
        {
            this.errorCode = refAcceptanceErrorType.Item2;
            this.errorMessage = errorMessage;
            if (eventData != null)
            {
                this.uid = eventData.Uid;
                this.orderNumSpecified = true;
                this.orderNum = eventData.orderNum;
                this.eventNameSpecified = true;
                this.eventName = eventData.EventName;
                this.eventDateTimeFieldSpecified = true;
                this.eventDateTime = eventData.eventDateTime;
            }
            if (blockName != null)
            {
                this.blockNameSpecified = true;
                this.blockName = blockName.Value;
            }

            if (fieldName != null)
            {
                this.fieldName = fieldName;
            }

            if (fieldValue != null)
            {
                if (fieldValue is DateTime dateTime)
                {
                    if (dateTime == DateTime.MinValue)
                    {
                        this.fieldValue = null;
                    }
                    else
                    {
                        this.fieldValue = dateTime.ToString("yyyy-MM-dd");
                    }
                }
                else if (fieldValue is DateTimeOffset dateTimeOffset)
                {
                    if (dateTimeOffset == DateTimeOffset.MinValue)
                    {
                        this.fieldValue = null;
                    }
                    else
                    {
                        this.fieldValue = dateTimeOffset.ToString("yyyy-MM-ddTHH:mm:sszzz");
                    }
                }
                else if (fieldValue is Enum enumValue)
                {
                    this.fieldValue = enumValue.GetXmlEnumAttribute();
                }
                else
                {
                    this.fieldValue = fieldValue?.ToString();
                }
            }
            _ignoreErrorIn32 = ignoreErrorIn32;
            _isCriticalTitleError = isCriticalTitleError;
        }

        public NotificationOfAcceptanceError(XElement xEvent, string? subjectCode, string? subjectID, string errorMessage) : this("2", errorMessage)
        {
            this.uidField = xEvent.XPathSelectElement("//uid")?.Value;

            var eventDateTimeAttribute = xEvent?.Attribute("eventDateTime")?.Value;
            if (eventDateTimeAttribute != null && DateTimeOffset.TryParse(xEvent?.Attribute("eventDateTime")?.Value, out var eventDateTimeValue))
            {
                this.eventDateTimeField = eventDateTimeValue;
                this.eventDateTimeFieldSpecified = true;
            }

            if (ulong.TryParse(xEvent?.Attribute("orderNum")?.Value, out ulong orderNum))
            {
                this.orderNumField = orderNum;
                this.orderNumFieldSpecified = true;
            }
            else
            {
                this.orderNumFieldSpecified = false;
            }

            if (string.IsNullOrEmpty(xEvent?.Name.LocalName))
            {
                this.eventNameSpecified = false;
            }
            else
            {
                this.eventNameSpecified = true;
                this.eventName = Enum.Parse<refEventNameType>(xEvent.Name.LocalName);
            }

            this.subjectCodeField = subjectCode;
            this.subjectIDField = subjectID;
        }
    }
}
