using loader_5_0_lib.Classes;
using loader_5_0_lib.Domain;
using loader_5_0_lib.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.IO.Compression;
using System.Xml;
using System.Xml.Schema;

namespace loader_5_0_lib.Service.Implementation
{
    public class AppParam : IAppParam
    {
        private readonly IConfiguration _config;
        public string[] DateTimeFormats { get; }
        public string? DateTimeFormat { get; }
        public string? DateFormat { get; }
        public string UploadPath { get; }
        public string XsdFullPath { get; }
        public string XsdFileName { get; }
        public Dictionary<string, XmlSchemaSet> XmlSchemaSets { get; } = [];
        public string DataTemplateFL { get; }
        public string DataTemplateUL { get; }
        public string AppBaseDirectory { get; }
        public bool IsCompress { get; }
        public CompressionLevel CompressionLevel { get; }
        public Dictionary<string, UploadFormatInfo> UploadFormats { get; } = [];
        public StreamWriter sw { get; }
        public bool FileSystemUse { get; }
        public string FileSystemUploadFilePath { get; }
        public string SendToDbPath { get; }
        public bool ForceReload { get; }
        public Dictionary<string, string> UsedPrioritytopics { get; set; } = [];

        private static readonly string[] defaultDateTimeFirmats = [
                    "dd.MM.yyyy HH:mm:ss",
                    "dd.MM.yyyy",
                    "yyyy-MM-dd",
                    "yyyy-MM-ddTHH:mm:ss",
                    "yyyy-MM-dd_HH-mm-ss"
                ];
        public DateOnly GetDate(string strDate)
        {
            return DateOnly.ParseExact(strDate, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }
        private XmlSchemaSet? GetXmlSchemaSet(string schemaVersion)
        {
            string xsdFullFileName = Path.Combine(XsdFullPath, schemaVersion, XsdFileName);

            XmlSchemaSet schemaSet = new()
            {
                XmlResolver = new XmlUrlResolver()
            };
            schemaSet.Add(null, xsdFullFileName);

            return schemaSet;
        }
        public AppParam(IConfiguration config)
        {
            _config = config;

            if (_config.GetSection("FileSystem").Exists())
            {
                var sect = _config.GetSection("FileSystem");

                FileSystemUse = _config.GetSection("FileSystem").GetValue<bool>("FileSystemUse");
                ForceReload = _config.GetSection("FileSystem").GetValue<bool>("ForceReload", false);

                FileSystemUploadFilePath = _config.GetSection("FileSystem").GetValue<string>("FileSystemUploadFilePath");
            }

            DateTimeFormats = _config.GetSection("Settings").GetValue<string[]>("DateTimeFormat") ?? defaultDateTimeFirmats;

            DateTimeFormat = DateTimeFormats[0];
            DateFormat = DateTimeFormats[1];

            AppBaseDirectory = AppContext.BaseDirectory;

            if (_config.GetSection("Paths").GetSection("Upload").Exists())
            {
                UploadPath = Path.Combine(AppBaseDirectory, _config.GetSection("Paths").GetValue<string>("Upload") ?? "Upload");
            }

            XsdFullPath = Path.Combine(AppBaseDirectory, _config.GetSection("Paths").GetValue<string>("Xsd") ?? "Xsd");
            XsdFileName = _config.GetSection("Settings").GetValue<string>("XsdFileName") ?? string.Empty;

            var schemaVersions = _config.GetSection("Settings").GetSection("SchemaVersions").Get<string[]>() ?? [];

            foreach (var schemaVersion in schemaVersions)
            {
                XmlSchemaSet? schemaSet = GetXmlSchemaSet(schemaVersion);
                if (schemaSet != null)
                {
                    XmlSchemaSets.Add(schemaVersion, schemaSet);
                }
            }

            DataTemplateFL = _config.GetSection("Settings").GetValue<string>("DataTemplateFL") ?? string.Empty;
            DataTemplateUL = _config.GetSection("Settings").GetValue<string>("DataTemplateUL") ?? string.Empty;

            UploadFormats = _config.GetSection("Settings:UploadFormats").Get<Dictionary<string, UploadFormatInfo>>() ?? [];
        }
        public void ValidateAppsettingsParams()
        {
            static string GetMessageString(string paramName)
            {
                return $"Отсутствует параметр конфигурации: {paramName}";

            }

            List<string> checkParams = ["Settings:SchemaVersions", "Settings:DataTemplateFL", "Settings:DataTemplateUL", "DataBase:UpdateNotificationOfReceipt"];

            foreach (var checkParam in checkParams)
            {
                if (!_config.GetSection(checkParam).Exists())
                {
                    throw new KeyNotFoundException(GetMessageString(checkParam));
                }
            }
        }
    }
}
