using System.Xml.Linq;

namespace loader_5_0_lib.Classes
{
    public class PackageReceivedNumberOfNew
    {
        private const string LocalName = "received_number_of_new";
        private const string XNameSubjects = "subjects";
        private const string XNameContracts = "contracts";
        private const string XNameApplications = "applications";
        private readonly XElement _ownerXml;
        private XElement Xml
        {
            get
            {
                if (_ownerXml?.Element(LocalName) == null)
                {
                    _ownerXml.Add(new XElement(LocalName));
                }
                return _ownerXml?.Element(LocalName);
            }
        }
        public string Subjects
        {
            get { return Xml.Element(XNameSubjects)?.Value; }
            set
            {
                if (value != null)
                    Xml.SetElementValue(XNameSubjects, value);
            }
        }
        public string Contracts
        {
            get { return Xml.Element(XNameContracts)?.Value; }
            set
            {
                if (value != null)
                    Xml.SetElementValue(XNameContracts, value);
            }
        }
        public string Applications
        {
            get { return Xml.Element(XNameApplications)?.Value; }
            set
            {
                if (value != null)
                    Xml.SetElementValue(XNameApplications, value);
            }
        }
        public PackageReceivedNumberOfNew(XElement xml)
        {
            _ownerXml = xml;
        }
    }
}