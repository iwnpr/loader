using loader_utils;
using System.Xml.Linq;
using System.Xml.Serialization;
using xsd_base.Notifications;

namespace Xsd.VersionFiveZero.Acceptance
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
                    itemsElementNameField = [ItemsChoiceType.sourceId];
                    itemsField = [sourceIdValue];
                }
                else
                {
                    itemsElementNameField = [ItemsChoiceType.inn, ItemsChoiceType.ogrn];
                    itemsField = [innValue, ogrnValue];
                }

                DateTimeOffset.TryParse(xDocHeader?.Attribute("dateTimeDoc")?.Value, out outgoingDocDateTimeField);
                outgoingDocNumberField = xDocHeader?.Attribute("regNumberDoc")?.Value;

                incomingDocDateTimeField = DateTime.Now.RemoveMilliseconds();
                incomingDocNumber = DocumentHelper.GetNextRegNum();

                ulong.TryParse(xDocHeader?.Attribute("subjectsCount")?.Value, out subjectsReceivedField);
                ulong.TryParse(xDocHeader?.Attribute("groupBlocksCount")?.Value, out eventsReceivedField);
                ulong.TryParse(xDocHeader?.Attribute("groupBlocksCount")?.Value, out eventsAcceptedField);
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
