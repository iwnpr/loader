using loader_utils;
using System.Xml.Linq;
using System.Xml.Serialization;
using xsd_base.Notifications;

namespace Xsd.VersionFiveOne.Acceptance
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
        }

        [XmlIgnore]
        public string? ogrn
        {
            get
            {
                return Items.GetString(ItemsElementName, "ogrn");
            }
        }

        [XmlIgnore]
        public string? sourceId
        {
            get
            {
                return Items.GetString(ItemsElementName, "sourceId");
            }
        }

        public void FillFields(XElement xDocHeader, string pFileName, string pFileHash, DateTime pUploadCreated)
        {
            FillFields(xDocHeader);

            fileNameField = pFileName;
            fileHashField = pFileHash;
            docDateTimeField = pUploadCreated;
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
                    this.itemsElementNameField = [ItemsChoiceType.sourceId];
                    this.itemsField = [sourceIdValue];
                }
                else
                {
                    this.itemsElementNameField = [ItemsChoiceType.inn, ItemsChoiceType.ogrn];
                    this.itemsField = [innValue, ogrnValue];
                }

                if (DateTimeOffset.TryParse(xDocHeader?.Attribute("dateTimeDoc")?.Value, out DateTimeOffset dateTimeDoc))
                {
                    this.outgoingDocDateTimeField = dateTimeDoc;
                }

                this.outgoingDocNumberField = xDocHeader?.Attribute("regNumberDoc")?.Value;
                this.incomingDocDateTimeField = DateTime.Now.RemoveMilliseconds();
                this.incomingDocNumber = DocumentHelper.GetNextRegNum();

                if (ulong.TryParse(xDocHeader?.Attribute("subjectsCount")?.Value, out ulong subjectsCount))
                {
                    this.subjectsReceivedField = subjectsCount;
                }

                if (ulong.TryParse(xDocHeader?.Attribute("groupBlocksCount")?.Value, out ulong groupBlocksCount))
                {
                    this.eventsReceivedField = groupBlocksCount;
                    this.eventsAcceptedField = groupBlocksCount;
                }

                eventsRejectedField = 0;
            }
        }

        public void ReceiveSubjects(ulong subjectsCount)
        {
            subjectsReceivedField += subjectsCount;
        }
        public void ReceiveEvents(ulong eventsCount)
        {
            eventsReceivedField += eventsCount;
        }
        public void AcceptEvents(ulong eventsCount)
        {
            eventsAcceptedField += eventsCount;
        }
        public void RejectEvents(ulong eventsCount)
        {
            eventsRejectedField += eventsCount;

            if (eventsAcceptedField >= eventsCount)
            {
                eventsAcceptedField -= eventsCount;
            }
        }
    }
}
