using loader_5_0_lib.Service;
using System.Xml.Linq;

namespace loader_5_0_lib.NotificationEF
{
    public class NotificationDocInfo22
    {
        UniDoc uniDoc { get; }
        //public string DataReceivedStatus { get { return GetReceivedStatus(); } }



        public NotificationDocInfo22(UniDoc uniDoc)
        {
            this.uniDoc = uniDoc;
        }
        public XElement Xml { get { return GetXml(); } }
        private XElement GetXml()
        {
            XElement xRet = new("DocInfo");

            //xRet.Add(uniDoc.Source.Xml.Elements());
            //xRet.Add(new XElement("dataReceivedStatus", DataReceivedStatus));
            //xRet.Add(new XElement("outgoingDocDate", uniDoc.OutgoingDoc.DocDate));
            //xRet.Add(new XElement("outgoingDocNumber", uniDoc.OutgoingDoc.DocNumber));
            //xRet.Add(new XElement("incomingDocDate", uniDoc.IncomingDoc.DocDate));
            //xRet.Add(new XElement("incomingDocNumber", uniDoc.IncomingDoc.DocNumber));
            //xRet.Add(new XElement("dateTime", uniDoc.DateTime));
            //xRet.Add(new XElement("fileName", uniDoc.FileName));
            //xRet.Add(new XElement("fileHash", uniDoc.FileHash));
            //xRet.Add(new XElement("subjectsReceived", uniDoc.SubjectsReceived.ToString()));
            //xRet.Add(new XElement("eventsReceived", uniDoc.EventsReceived.ToString()));
            //xRet.Add(new XElement("eventsAccepted", uniDoc.EventsAccepted.ToString()));
            //xRet.Add(new XElement("eventsRejected", uniDoc.EventsRejected.ToString()));

            return xRet;
        }
    }
}