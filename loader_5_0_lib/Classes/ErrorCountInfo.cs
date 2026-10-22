using System.Xml.Linq;

namespace loader_5_0_lib.Classes
{
    public class ErrorCountInfo
    {
        public XElement Xml;
        private int _code { get; set; }
        public int Code
        {
            get { return _code; }
            set
            {
                _code = value;
                Xml.SetAttributeValue("code", value);
            }
        }
        private int _count { get; set; }
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                Xml.SetAttributeValue("count", value);
            }
        }
        public ErrorCountInfo(int code)
        {
            Xml = new XElement("error-count");
            Code = code;
            Count = 0;
        }
    }
}