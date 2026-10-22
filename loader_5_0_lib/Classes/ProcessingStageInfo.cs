using System.Xml.Linq;

namespace loader_5_0_lib.Classes
{
    public class ProcessingStageInfo
    {
        public XElement Xml { get; set; }
        public RefProcessingStatus Id
        {
            get
            {
                return (RefProcessingStatus)int.Parse(Xml.Attribute("id").Value);
            }
        }
        public string Description
        {
            set
            {
                Xml.SetAttributeValue("description", value);
            }
        }
        public RefProcessingStatus ProcessingStatus
        {
            set
            {
                Xml.SetAttributeValue("processing-status", ((int)value).ToString());
            }
        }
        private SummaryInfo _summary { get; set; }
        public SummaryInfo Summary
        {
            get
            {
                if (_summary == null)
                {
                    _summary = new SummaryInfo();
                    Xml.Add(Summary.Xml);
                }
                return _summary;
            }
        }
        public ProcessingStageInfo(RefProcessingStage id)
        {
            Xml = new XElement("processing-stage");
            Xml.SetAttributeValue("id", ((int)id).ToString());
            ProcessingStatus = 0;

        }
    }
}