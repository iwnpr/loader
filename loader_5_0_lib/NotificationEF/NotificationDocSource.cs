using System.Xml.Linq;

namespace loader_5_0_lib.NotificationEF
{
    public class NotificationDocSource
    {
        public string SourceID { get; }
        public string Inn { get; }
        public string Ogrn { get; }
        private bool HasSourceId { get { return string.IsNullOrEmpty(string.Concat(Inn, Ogrn)); } }
        public XElement Xml { get { return GetXml(); } }
        public NotificationDocSource(string inn, string ogrn, string sourceID)
        {
            Inn = inn;
            Ogrn = ogrn;
            SourceID = sourceID;
        }
        public NotificationDocSource(string inn, string ogrn)
        {
            Inn = inn;
            Ogrn = ogrn;
        }
        public NotificationDocSource(string sourceID)
        {
            SourceID = sourceID;
        }
        private XElement GetXml()
        {
            XElement xRet = new("root");
            if (HasSourceId)
            {
                xRet.Add(new XElement("sourceId", SourceID));
            }
            else
            {
                xRet.Add(new XElement("inn", Inn));
                xRet.Add(new XElement("ogrn", Ogrn));
            }
            return xRet;
        }
    }
}