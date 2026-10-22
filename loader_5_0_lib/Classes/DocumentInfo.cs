using System.Xml.Linq;

namespace loader_5_0_lib.Classes
{
    public class DocumentInfo
    {
        public XElement Xml { get; }
        public string FormatType
        {
            get
            {
                return Xml?.Attribute("format-type")?.Value ?? string.Empty;
            }
        }
        public string FileName
        {
            get
            {
                return Xml?.Attribute("file-name")?.Value ?? string.Empty;
            }
        }
        public RefProcessingStatus ProcessingStatus
        {
            set
            {
                Xml.SetAttributeValue("processing-status", (int)value);
            }
        }

        public int ProcessedElementsSend
        {
            get
            {
                return int.TryParse(Xml.Attribute("processed-elements-send")?.Value, out int sendCnt) ? sendCnt : 0;
            }
            set
            {
                Xml.SetAttributeValue("processed-elements-send", value);
            }
        }
        public string OutgoingRegNum
        {
            set
            {
                Xml.SetAttributeValue("outgoing-reg-num", value);
            }
        }
        public string OutgoingDateTime
        {
            set
            {
                Xml.SetAttributeValue("outgoing-date-time", value);
            }
        }

        public string IncomingRegNum
        {
            set
            {
                Xml.SetAttributeValue("incoming-reg-num", value);
            }
        }
        public string IncomingDateTime
        {
            set
            {
                Xml.SetAttributeValue("incoming-date-time", value);
            }
        }
        public Dictionary<RefProcessingStage, ProcessingStageInfo> ProcessingStages { get; } = new();
        public ProcessingStageInfo ProcessingStage(RefProcessingStage processingStageId)
        {
            if (!ProcessingStages.ContainsKey(processingStageId))
            {
                Xml.Elements("processing-stage").Where(x => x.Attribute("id") == null ? true : x.Attribute("id").Value.Equals(((int)processingStageId).ToString())).Remove();

                ProcessingStages.Add(processingStageId, new(processingStageId));

                Xml.Add(ProcessingStages[processingStageId].Xml);
            }
            return ProcessingStages[processingStageId];
        }

        public DocumentInfo(XElement xml)
        {
            Xml = xml;
        }
    }
}