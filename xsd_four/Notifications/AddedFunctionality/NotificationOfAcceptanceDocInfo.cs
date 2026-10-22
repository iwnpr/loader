using loader_utils;
using System.Xml.Linq;
using System.Xml.Serialization;
using xsd_base.Notifications;

namespace Xsd.VersionFour.Acceptance
{
    public partial class NotificationOfAcceptanceDocInfo : INotificationOfAcceptanceDocInfo
    {
        public NotificationOfAcceptanceDocInfo()
        {
            ItemsElementName = [];
        }
        [XmlIgnore]
        public string? inn
        {
            get
            {
                return Items.GetString(ItemsElementName, "inn");
            }
            set { Items.SetValue(value, Array.IndexOf(ItemsElementName, "inn")); }
        }

        [XmlIgnore]
        public string? ogrn
        {
            get
            {
                return Items.GetString(ItemsElementName, "ogrn");
            }
            set { Items.SetValue(value, Array.IndexOf(ItemsElementName, "ogrn")); }
        }

        [XmlIgnore]
        public string? sourceId
        {
            get
            {
                return Items.GetString(ItemsElementName, "sourceId");
            }
            set { Items.SetValue(value, Array.IndexOf(ItemsElementName, "sourceId")); }
        }

        [XmlIgnore]
        public DateTime outgoingDocDateTimeExt { get => outgoingDocDateField; set => outgoingDocDateField = value; }

        [XmlIgnore]
        public DateTime incomingDocDateTimeExt { get => incomingDocDateField; set => incomingDocDateField = value; }

        [XmlIgnore]
        public DateTime docDateTimeExt { get => dateTimeField; set => dateTimeField = value; }
        public void FillFields(XElement xDocHeader, string pFileName, string pFileHash, DateTime pUploadCreated)
        {
            FillFields(xDocHeader);

            fileNameField = pFileName;
            fileHashField = pFileHash;
            dateTimeField = pUploadCreated;
        }
        public void FillFields(XElement xDocHeader)
        {
            if (xDocHeader != null)
            {
                var innValue = xDocHeader.Attribute("inn")?.Value;
                var ogrnValue = xDocHeader.Attribute("ogrn")?.Value;
                var sourceIdValue = xDocHeader?.Attribute("sourceID")?.Value;

                if (string.IsNullOrEmpty(innValue))
                {
                    itemsElementNameField = [ItemsChoiceType.sourceId];
                    itemsField = [sourceIdValue];
                }
                else
                {
                    itemsElementNameField = [ItemsChoiceType.inn, ItemsChoiceType.ogrn];
                    itemsField = [innValue, ogrnValue];
                }

                DateTime.TryParse(xDocHeader?.Attribute("dateDoc")?.Value, out outgoingDocDateField);
                outgoingDocNumberField = xDocHeader?.Attribute("regNumberDoc")?.Value;

                incomingDocDate = DateTime.Now;
                incomingDocNumber = DocumentHelper.GetNextRegNum();

                ulong.TryParse(xDocHeader?.Attribute("subjectsCount")?.Value, out subjectsReceivedField);
                ulong.TryParse(xDocHeader?.Attribute("groupBlocksCount")?.Value, out eventsReceivedField);
                ulong.TryParse(xDocHeader?.Attribute("groupBlocksCount")?.Value, out eventsAcceptedField);
                eventsRejectedField = 0;
            }
        }

        public void ReceiveSubjects(ulong subjectsCount)
        {
            subjectsReceived += subjectsCount;
        }
        public void ReceiveEvents(ulong eventsCount)
        {
            eventsReceived += eventsCount;
        }
        public void AcceptEvents(ulong eventsCount)
        {
            eventsAcceptedField += eventsCount;
        }
        public void RejectEvents(ulong eventsCount)
        {
            eventsRejectedField += eventsCount;
        }
    }
}
