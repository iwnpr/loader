using System.Xml.Linq;

namespace loader_5_0_lib.Classes
{
    public interface IUploadDocumentHeader
    {
        string SchemaVersion { get; }
        string Inn { get; }
        string Ogrn { get; }
        string SourceID { get; }
        string RegNumberDoc { get; }
        string DateTimeDoc { get; }
        ulong SubjectsCount { get; }
        ulong GroupBlocksCount { get; }
        string RegNumberDocInaccept { get; }
        bool HasDataAF { get; set; }
        int? SourceCode { get; set; }
        XElement SourceXml { get; }
        XElement Xml { get; }
    }
}
