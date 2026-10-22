using System.Xml.Linq;

namespace loader_5_0_lib.Classes.v50
{
    public class UploadDocumentHeader(XElement xDoc) : IUploadDocumentHeader
    {
        public string SchemaVersion { get; } = xDoc.Attribute("schemaVersion")?.Value ?? string.Empty;
        public string Inn { get; } = xDoc?.Attribute("inn")?.Value ?? string.Empty;
        public string Ogrn { get; } = xDoc?.Attribute("ogrn")?.Value ?? string.Empty;
        public string SourceID { get; } = xDoc?.Attribute("sourceID")?.Value ?? string.Empty;
        public string RegNumberDoc { get; } = xDoc?.Attribute("regNumberDoc")?.Value ?? string.Empty;
        public string DateTimeDoc { get; } = xDoc?.Attribute("dateTimeDoc")?.Value ?? string.Empty;
        public ulong SubjectsCount { get; } = ulong.TryParse(xDoc?.Attribute("subjectsCount")?.Value, out ulong subjectsCount) ? subjectsCount : ulong.MinValue;
        public ulong GroupBlocksCount { get; } = ulong.TryParse(xDoc?.Attribute("groupBlocksCount")?.Value, out ulong groupBlocksCount) ? groupBlocksCount : ulong.MinValue;
        public string RegNumberDocInaccept { get; } = xDoc?.Attribute("regNumberDocInaccept")?.Value ?? string.Empty;
        public XElement Xml { get; } = xDoc ?? new("Document");
        public int? SourceCode { get; set; }
        public XElement SourceXml { get { return Xml?.Element("Source") ?? new("Source"); } }
        public bool HasDataAF { get; set; }
    }
}
