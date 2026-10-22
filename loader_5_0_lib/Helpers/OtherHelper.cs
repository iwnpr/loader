using loader_utils;
using System.Globalization;
using System.Xml.Linq;

namespace loader_5_0_lib.Helpers
{
    public static class OtherHelper
    {
        public static XElement GetUploadXml()
        {
            return GetUploadXml(null, null);
        }
        internal static XElement GetUploadXml(FileInfo uploadFileInfo)
        {
            return GetUploadXml(uploadFileInfo.Name, uploadFileInfo.Length);
        }
        internal static XElement GetUploadXml(string? fileName, long? fileSize)
        {
            int client_id = 0;
            string upload_id = string.Concat(DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture), DocumentHelper.GetNextRegNum(30));
            string upload_finish_datetime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            string? source_file_name = fileName;
            long? package_file_size = fileSize;
            int package_document_count = 1;
            int processing_status = 0;
            string format_type = "rsb-batch-uf30";

            XElement uploadXml = new("upload");
            uploadXml.Add(new XElement("user-info", new XAttribute("client-id", client_id)));
            uploadXml.Add(new XElement("package-info",
                new XAttribute("upload-id", upload_id),
                new XAttribute("upload-finish-datetime", upload_finish_datetime),
                string.IsNullOrEmpty(source_file_name) ? null : new XAttribute("source-file-name", source_file_name),
                package_file_size == null ? null : new XAttribute("package-file-size", package_file_size),
                new XAttribute("package-document-count", package_document_count),
                new XAttribute("processing-status", processing_status)
                ));
            uploadXml.Add(new XElement("document",
                string.IsNullOrEmpty(source_file_name) ? null : new XAttribute("file-name", source_file_name),
                new XAttribute("format-type", format_type),
                package_file_size == null ? null : new XAttribute("file-size", package_file_size)
                ));
            return uploadXml;

        }
        internal static XElement GetStandartSupportMessage(string message)
        {
            return new("user",
                new XAttribute("user-id", 5045021),
                new XAttribute("organization-id", 0),
                new XAttribute("email", "help@rs-cb.ru"),
                new XAttribute("name", "Служба поддержки"),
                new XAttribute("created", DateTime.Now.ToString(new CultureInfo("de-DE"))),
                new XElement("text", message),
                new XElement("notification-to",
                new XAttribute("email", "help@rs-cb.ru"),
                new XAttribute("name", "Тех.поддержка")));
        }
    }
}
