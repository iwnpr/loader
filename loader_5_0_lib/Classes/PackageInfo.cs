using System.Globalization;
using System.Xml.Linq;

namespace loader_5_0_lib.Classes
{
    public class PackageInfo
    {
        readonly string[] dateFormats = new string[] { "dd.MM.yyyy HH:mm:ss", "dd.MM.yyyy", "yyyy-MM-dd" };
        internal XElement Xml { get; }
        public PackageReceivedNumberOfNew ReceivedNumberOfNew { get; }
        public PackageInfo(XElement xml)
        {
            Xml = xml;
            ReceivedNumberOfNew = new PackageReceivedNumberOfNew(xml);
        }
        public string UploadId
        {
            get { return Xml?.Attribute("upload-id")?.Value; }
            set { Xml.SetAttributeValue("upload-id", value); }
        }
        public string SourceFileName
        {
            get { return Xml?.Attribute("source-file-name").Value; }
        }
        public int PackageDocumentCount
        {
            get
            {
                _ = int.TryParse(Xml?.Attribute("package-document-count").Value, out int docCount);
                return docCount;
            }
        }
        public RefProcessingStatus ProcessingStatus
        {
            get
            {
                _ = int.TryParse(Xml?.Attribute("upload-id")?.Value, out int processingStatus);
                return (RefProcessingStatus)processingStatus;
            }
            set { Xml.SetAttributeValue("processing-status", ((int)value).ToString()); }
        }
        public long UploadKeyId
        {
            get
            {
                _ = long.TryParse(Xml?.Attribute("upload_key_id").Value, out long uploadKeyId);
                return uploadKeyId;
            }
            set { Xml.SetAttributeValue("upload_key_id", value); }
        }
        public DateTime ProcessingStartDatetime
        {
            get
            {
                _ = DateTime.TryParseExact(Xml?.Attribute("processing-start-datetime").Value, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime processingStartDatetime);
                return processingStartDatetime;
            }
            set { Xml.SetAttributeValue("processing-start-datetime", value.ToString("dd.MM.yyyy HH:mm:ss")); }
        }
        public DateTime ProcessingFinishDatetime
        {
            get
            {
                _ = DateTime.TryParseExact(Xml?.Attribute("processing-finish-datetime").Value, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime processingFinishDatetime);
                return processingFinishDatetime;
            }
            set { Xml.SetAttributeValue("processing-finish-datetime", value.ToString("dd.MM.yyyy HH:mm:ss")); }
        }
        //ProcessingFinishDatetime
        public void ProcessingFinish(DateTime finishDateTime)
        {
            ProcessingStatus = RefProcessingStatus.Finish;
            ProcessingFinishDatetime = finishDateTime;
        }
        public void ProcessingStart(DateTime startDateTime)
        {
            ProcessingStatus = RefProcessingStatus.Running;
            ProcessingStartDatetime = startDateTime;
        }

    }
}