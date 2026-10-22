using loader_5_0_lib.Classes;
using System.IO.Compression;
using System.Xml.Schema;

namespace loader_5_0_lib.Service.Interfaces
{
    public interface IAppParam
    {
        string[] DateTimeFormats { get; }
        string? DateTimeFormat { get; }
        string? DateFormat { get; }
        string UploadPath { get; }
        string AppBaseDirectory { get; }
        string XsdFullPath { get; }
        Dictionary<string, XmlSchemaSet> XmlSchemaSets { get; }
        string XsdFileName { get; }
        string DataTemplateFL { get; }
        string DataTemplateUL { get; }
        bool IsCompress { get; }
        CompressionLevel CompressionLevel { get; }
        Dictionary<string, UploadFormatInfo> UploadFormats { get; }
        DateOnly GetDate(string strDate);
        bool FileSystemUse { get; }
        string FileSystemUploadFilePath { get; }
        string SendToDbPath { get; }
        bool ForceReload { get; }
        Dictionary<string, string> UsedPrioritytopics { get; set; }
        void ValidateAppsettingsParams();
    }
}

