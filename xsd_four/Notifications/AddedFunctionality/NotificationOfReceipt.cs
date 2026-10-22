using loader_utils;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Receipt
{
    public partial class NotificationOfReceipt : INotificationOfReceipt
    {
        [XmlIgnore]
        public string SchemaVersion { get => schemaVersionField.GetXmlEnumAttribute() ?? throw new ArgumentNullException("SchemaVersion"); }

        [XmlIgnore]
        public string? Inn { get => Items.GetString(ItemsElementName, "inn"); }

        [XmlIgnore]
        public string? Ogrn { get => Items.GetString(ItemsElementName, "ogrn"); }

        [XmlIgnore]
        public string? SourceId { get => Items.GetString(ItemsElementName, "sourceId"); }

        [XmlIgnore]
        public string OutgoingDocDateTime => this.outgoingDocDate;

        [XmlIgnore]
        public string OutgoingDocNumber => this.outgoingDocNumber;

        [XmlIgnore]
        public string AcceptanceStatus
        {
            get
            {
                return Items1.GetString(Items1ElementName, "acceptanceStatusFail") == null ?
                    Items1.GetString(Items1ElementName, "acceptanceStatusOK") == null ? throw new Exception("acceptanceStatus") : "acceptanceStatusOK" :
                    "acceptanceStatusFail";
            }
        }

        [XmlIgnore]
        public DateTime? IncomingDocDateTime { get => Items1.GetString(Items1ElementName, "acceptanceStatusFail") == null ? Items1.GetDateTime(Items1ElementName, "incomingDocDate") : null; }

        [XmlIgnore]
        public string? IncomingDocNumber { get => Items1.GetString(Items1ElementName, "acceptanceStatusFail") ?? Items1.GetString(Items1ElementName, "incomingDocNumber") ?? null; }

        [XmlIgnore]
        public INotificationOfReceiptErrors[] Errors
        {
            get
            {
                return Items1.GetObjects<NotificationOfReceiptErrors>();
            }
        }

        public NotificationOfReceipt() { }

        [XmlIgnore]
        public string FileName => this.fileName;

        [XmlIgnore]
        public string FileHash => this.fileHash;

        [XmlIgnore]
        public DateTime DocDateTime { get => this.dateTime; }

        [XmlIgnore]
        public DateTime SendingDateTime { get; set; }

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
            outgoingDocDate = document.dateDocMoscow.ToString("yyyy-MM-dd");
            outgoingDocNumber = document.regNumberDoc;
        }

        public void FillFields(XElement xDocHeader)
        {
            var innValue = xDocHeader?.Attribute("inn")?.Value;
            var ogrnValue = xDocHeader?.Attribute("ogrn")?.Value;
            var sourceIdValue = xDocHeader?.Attribute("sourceID")?.Value;
            var regNumberDocValue = xDocHeader?.Attribute("regNumberDoc")?.Value;
            var dateDocValue = xDocHeader?.Attribute("dateDoc")?.Value;

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

            outgoingDocDate = dateDocValue ?? "-";
            outgoingDocNumber = regNumberDocValue ?? "-";
        }

        public void FillFields(XElement xDocHeader, string fileName, string fileHash, DateTime uploadCreated)
        {
            FillFields(xDocHeader);

            this.Items1ElementName = [Items1ChoiceType.acceptanceStatusOK, Items1ChoiceType.incomingDocDate, Items1ChoiceType.incomingDocNumber];
            this.Items1 = [string.Empty, System.DateTime.Now.RemoveMilliseconds(), DocumentHelper.GetNextRegNum()]; //.ToString("o")

            this.fileName = fileName;
            this.fileHash = fileHash;

            this.dateTime = uploadCreated.RemoveMilliseconds();
        }
    }
}