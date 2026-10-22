using System.Xml.Linq;

namespace loader_5_0_lib.NotificationEF
{
    public class NotificationOfAcceptanceWarningMessageEF
    {
        public long Id { get; set; }
        public long NotificationOfAcceptanceId { get; set; }
        public NotificationOfAcceptanceEF NotificationOfAcceptance { get; set; }
        public string WarningMessage { get; set; }
        public NotificationOfAcceptanceWarningMessageEF(string warningMessage)
        {
            WarningMessage = warningMessage;
        }

        internal XElement? GetXml()
        {
            return new XElement("warningMessage", WarningMessage);
        }
    }
}
