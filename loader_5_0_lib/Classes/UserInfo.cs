using System.Xml.Linq;

namespace loader_5_0_lib.Classes
{
    public class UserInfo
    {
        public XElement Xml { get; }
        internal UserInfo(XElement xml)
        {
            Xml = xml;
        }
        public int UserId
        {
            get
            {
                int.TryParse(Xml?.Attribute("user-id")?.Value, out int userId);
                return userId;
            }
            set { Xml.SetAttributeValue("user-id", value); }
        }
        public string UserName
        {
            get { return Xml?.Attribute("user-name")?.Value; }
            set { Xml.SetAttributeValue("user-name", value); }
        }
        public int ClientId
        {
            get
            {
                int clientId = default;
                int.TryParse(Xml?.Attribute("client-id")?.Value, out clientId);
                return clientId;
            }
            set { Xml.SetAttributeValue("client-id", value); }
        }
        public string ClientName
        {
            get { return Xml?.Attribute("client-name")?.Value; }
            set { Xml.SetAttributeValue("client-name", value); }
        }
        public string CertificateId
        {
            get { return Xml?.Attribute("certificate-id")?.Value; }
            set { Xml.SetAttributeValue("certificate-id", value); }
        }
        public string RemoteAddress
        {
            get { return Xml?.Attribute("remote-address")?.Value; }
            set { Xml.SetAttributeValue("remote-address", value); }
        }
        public string ClientItn
        {
            get { return Xml?.Attribute("clientitn")?.Value; }
            set { Xml.SetAttributeValue("clientitn", value); }
        }
        public string ClilentOgrn
        {
            get { return Xml?.Attribute("clilentogrn")?.Value; }
            set { Xml.SetAttributeValue("clilentogrn", value); }
        }
    }
}