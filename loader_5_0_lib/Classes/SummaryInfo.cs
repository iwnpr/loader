using System.Xml.Linq;

namespace loader_5_0_lib.Classes
{
    public class SummaryInfo
    {
        internal XElement Xml { get; set; }
        public string ForElementType { set { Xml.SetAttributeValue("for-element-type", value); } }
        public long TotalElementsCount { set { Xml.SetAttributeValue("total-elements-count", value.ToString()); } }
        public long ProcessedElementsCount
        {
            set { Xml.SetAttributeValue("processed-elements-count", value.ToString()); }
        }
        public Dictionary<object, ErrorCountInfo> Errors { get; set; } = new();
        public SummaryInfo()
        {
            Xml = new XElement("summary");
        }
        public void AddError(int errorCode)
        {
            if (!Errors.ContainsKey(errorCode))
            {
                ErrorCountInfo errorCount = new(errorCode);
                Errors.Add(errorCode, errorCount);
                Xml.Add(errorCount.Xml);
            }
            Errors[errorCode].Count++;
        }
    }
}