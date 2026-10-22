using loader_utils;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Xsd.VersionFour.Acceptance
{
    public partial class NotificationOfAcceptanceError : INotificationOfAcceptanceError
    {
        [XmlIgnore]
        bool _isCriticalTitleError;

        [XmlIgnore]
        public bool IsCriticalTitleError
        {
            get
            {
                return _isCriticalTitleError;
            }
        }

        [XmlIgnore]
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
        public bool EventDateTimeSpecified => this.eventDateFieldSpecified;

        [XmlIgnore]
        public DateTime EventDate => this.eventDate; //DateOnly.FromDateTime(eventDate).ToString("o");

        [XmlIgnore]
        public bool EventTimeSpecified => this.eventTimeFieldSpecified;

        [XmlIgnore]
        public ushort ErrorCode => this.errorCodeField;

        [XmlIgnore]
        public string? ErrorMessage => this.errorMessageField;

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

        [XmlIgnore]
        public DateTime EventDateTime { get; }

        [XmlIgnore]
        public string? EventDateTime50 { get; }

        public NotificationOfAcceptanceError()
        {
        }

        public NotificationOfAcceptanceError(string errorMessage)
        {
            this.errorCode = 2;
            this.errorMessage = errorMessage;
        }

        public NotificationOfAcceptanceError(string errorCode, string errorMessage)
        {
            this.errorCode = ushort.Parse(errorCode);
            this.errorMessage = errorMessage;
        }

        public NotificationOfAcceptanceError(IEventData? eventData, refBlockNameType? blockName, string? fieldName, object? fieldValue, string errorMessage, bool ignoreErrorIn32 = false, bool isCriticalTitleError = false)
        {
            this.errorCode = 2;
            this.errorMessage = errorMessage;
            if (eventData != null)
            {
                this.uid = eventData.Uid;
                this.orderNumSpecified = true;
                this.orderNum = eventData.orderNum;
                this.eventNameSpecified = true;
                this.eventName = eventData.EventName;
                if (eventData.EventDate != null)
                {
                    this.eventDateSpecified = true;
                    this.eventDateField = eventData.EventDate.Value;
                }
                this.eventTimeField = eventData.EventTime;
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
                        this.fieldValue = (dateTime).ToString("yyyy-MM-dd");
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

        public NotificationOfAcceptanceError(ulong? orderNum, string? eventName, refBlockNameType? blockName, string? fieldName, object? fieldValue, string? subjectCode, string? subjectID, string errorMessage)
        {
            this.errorCode = 1;
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

        public NotificationOfAcceptanceError(XElement xEvent, string? subjectCode, string? subjectID, string errorMessage) : this("2", errorMessage)
        {
            this.uidField = xEvent.XPathSelectElement("//uid")?.Value;

            if (DateTime.TryParse(xEvent?.Attribute("eventDateTime")?.Value, out DateTime eventDateTime))
            {
                this.eventDateFieldSpecified = true;
                this.eventDateField = eventDateTime;
            }
            else
            {
                this.eventDateFieldSpecified = false;
            }

            if (string.IsNullOrEmpty(xEvent?.Attribute("eventTime")?.Value))
            {
                this.eventTimeFieldSpecified = false;
            }
            else
            {
                this.eventTimeFieldSpecified = true;
                this.eventTimeField = xEvent?.Attribute("orderNum")?.Value;
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
