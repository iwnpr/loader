using loader_utils;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Receipt
{
    public partial class NotificationOfReceipt : INotificationOfReceipt
    {
        public NotificationOfReceipt() { }

        [XmlIgnore]
        public string SchemaVersion { get => this.schemaVersion.GetXmlEnumAttribute() ?? throw new ArgumentNullException("schemaVersion"); }

        [XmlIgnore]
        public string? Inn { get => Items.GetString(ItemsElementName, "inn"); }

        [XmlIgnore]
        public string? Ogrn { get => Items.GetString(ItemsElementName, "ogrn"); }

        [XmlIgnore]
        public string? SourceId { get => Items.GetString(ItemsElementName, "sourceId"); }

        [XmlIgnore]
        public string OutgoingDocNumber { get => this.outgoingDocNumber; }

        [XmlIgnore]
        public string AcceptanceStatus
        {
            get
            {
                return string.IsNullOrEmpty(Items1.GetString(Items1ElementName, "acceptanceStatusFail")) ?
                    Items1.GetString(Items1ElementName, "acceptanceStatusFail") ?? string.Empty :
                    Items1.GetString(Items1ElementName, "acceptanceStatusOK") ?? string.Empty;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [XmlIgnore]
        public System.DateTimeOffset? incomingDocDateTime
        {
            get
            {
                if (Items1.GetString(Items1ElementName, "acceptanceStatusFail") == null)
                {
                    return Items1.GetDateTimeOffset(Items1ElementName, "incomingDocDateTime");
                }
                else
                {
                    return null;
                }
            }
        }

        [XmlIgnore]
        public string? IncomingDocNumber
        {
            get
            {
                return Items1.GetString(Items1ElementName, "acceptanceStatusFail") ?? Items1.GetString(Items1ElementName, "incomingDocNumber") ?? string.Empty;
            }
        }

        [XmlIgnore]
        public INotificationOfReceiptErrors[] Errors
        {
            get
            {
                return Items1.GetObjects<NotificationOfReceiptErrors>();
            }
        }

        [XmlIgnore]
        public string FileName { get => fileName; set => fileName = value; }

        [XmlIgnore]
        public string FileHash { get => fileHash; set => fileHash = value; }


        public void FillFields(IDocument document)
        {
            if (document.sourceID != null)
            {
                ItemsElementName = [ItemsChoiceType.sourceId];
                Items = [document.sourceID];
            }
            else
            {
                ItemsElementName = [ItemsChoiceType.inn, ItemsChoiceType.ogrn];
                Items = [document.inn ?? "-", document.ogrn ?? "-"];
            }
            outgoingDocDateTime = document.dateTimeDoc.ToString("yyyy-MM-ddTHH:mm:sszzz");
            outgoingDocNumber = document.regNumberDoc;
        }
        public void FillFields(XElement xDocHeader, string fileName, string fileHash, DateTime uploadCreated)
        {
            FillFields(xDocHeader);

            Items1ElementName = [Items1ChoiceType.acceptanceStatusOK, Items1ChoiceType.incomingDocDateTime, Items1ChoiceType.incomingDocNumber];
            Items1 = [string.Empty, new DateTimeOffset(DateTime.Now.RemoveMilliseconds()), DocumentHelper.GetNextRegNum()]; //.ToString("o")

            this.fileName = fileName;
            this.fileHash = fileHash;

            this.docDateTime = uploadCreated;
        }
        public void FillFields(XElement xDocHeader)
        {
            var innValue = xDocHeader?.Attribute("inn")?.Value;
            var ogrnValue = xDocHeader?.Attribute("ogrn")?.Value;
            var sourceIdValue = xDocHeader?.Attribute("sourceID")?.Value;
            var regNumberDoValue = xDocHeader?.Attribute("regNumberDoc")?.Value;
            var dateTimeDoc = xDocHeader?.Attribute("dateTimeDoc")?.Value;

            if (!string.IsNullOrEmpty(innValue) && !string.IsNullOrEmpty(ogrnValue))
            {
                ItemsElementName = [ItemsChoiceType.inn, ItemsChoiceType.ogrn];
                Items = [innValue, ogrnValue];
            }
            else
            {
                ItemsElementName = [ItemsChoiceType.sourceId];
                Items = [sourceIdValue];
            }

            outgoingDocDateTime = dateTimeDoc ?? "-";
            outgoingDocNumber = regNumberDoValue ?? "-";
        }
        public void Decline(List<NotificationOfReceiptErrors> errors)
        {
            Items1ElementName = new Items1ChoiceType[errors.Count + 1];
            Items1 = new string[errors.Count + 1];

            Items1ElementName[0] = Items1ChoiceType.acceptanceStatusFail;
            Items1[0] = string.Empty;
            int i = 0;
            foreach (var error in errors)
            {
                Items1ElementName[0] = Items1ChoiceType.Errors;
                Items1[0] = error;
                i++;
            }
        }

        public Acceptance.NotificationOfAcceptance Accept()
        {
            var incomingDocNumber = DocumentHelper.GetNextRegNum();
            Items1ElementName = [Items1ChoiceType.acceptanceStatusOK, Items1ChoiceType.incomingDocDateTime, Items1ChoiceType.incomingDocNumber];
            Items1 = [string.Empty, new DateTimeOffset(DateTime.Now.RemoveMilliseconds()), DocumentHelper.GetNextRegNum()];

            Acceptance.NotificationOfAcceptance notificationOfAcceptance = new()
            {
                DocInfo = new()
            };
            if (ItemsElementName[0] == ItemsChoiceType.sourceId)
            {
                notificationOfAcceptance.DocInfo.ItemsElementName = [Acceptance.ItemsChoiceType.sourceId];
                notificationOfAcceptance.DocInfo.Items = [Items[0]];
            }
            else
            {
                notificationOfAcceptance.DocInfo.ItemsElementName = [Acceptance.ItemsChoiceType.inn, Acceptance.ItemsChoiceType.ogrn];
                notificationOfAcceptance.DocInfo.Items = [Items[0], Items[1]];
            }
            notificationOfAcceptance.DocInfo.outgoingDocDateTime = System.DateTimeOffset.Parse(outgoingDocDateTime);
            notificationOfAcceptance.DocInfo.outgoingDocNumber = outgoingDocNumber;
            notificationOfAcceptance.DocInfo.incomingDocDateTime = DateTime.Now.RemoveMilliseconds();
            notificationOfAcceptance.DocInfo.incomingDocNumber = incomingDocNumber;
            notificationOfAcceptance.DocInfo.subjectsReceived = 0;
            notificationOfAcceptance.DocInfo.eventsAccepted = 0;
            notificationOfAcceptance.DocInfo.eventsReceived = 0;
            notificationOfAcceptance.DocInfo.eventsRejected = 0;
            notificationOfAcceptance.DocInfo.docDateTime = docDateTime;
            notificationOfAcceptance.DocInfo.fileName = fileName;
            notificationOfAcceptance.DocInfo.fileHash = fileHash;
            notificationOfAcceptance.Errors = [];
            notificationOfAcceptance.Warnings = [];
            return notificationOfAcceptance;
        }
        public void AddErrors(int errorCode, List<string> errorMessages)
        {
            foreach (var errorMessage in errorMessages)
            {
                AddError(errorCode, errorMessage);
            }
        }

        public void AddError(int errorCode, string errorMessage)
        {
            AddError(new NotificationOfReceiptErrors(EnumHelper.GetCode<refReceiptErrorType>(errorCode.ToString()), errorMessage));
        }

        void AddError(INotificationOfReceiptErrors error)
        {
            if (Items1ElementName.Contains(Items1ChoiceType.acceptanceStatusFail))
            {
                Items1ElementName = [.. Items1ElementName, Items1ChoiceType.Errors];
                Items1 = [.. Items1, error];
            }
            else
            {
                Items1ElementName = [Items1ChoiceType.acceptanceStatusFail];
                Items1 = [string.Empty];

                Items1ElementName = [.. Items1ElementName, Items1ChoiceType.Errors];
                Items1 = [.. Items1, error];
            }
        }
    }
}