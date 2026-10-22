using loader_utils;
using loader_5_0_lib.Classes;
using loader_5_0_lib.Helpers;
using loader_5_0_lib.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using Xsd;

namespace loader_5_0_lib.Service.Implementation
{
    public interface IDocService
    {
        IUploadDocumentHeader DocHeader { get; set; }
        INotificationOfReceipt Receipt { get; set; }
        INotificationOfAcceptance Acceptance { get; set; }
        IUploadDocumentHeader? GetUploadDocumentHeader(MemoryStream memoryStream);
        UploadPackageInfoExt? GetPreviousRegNumberDocs(int legalPersonId, DateTime docDateTime, DateTime created, string outgoingDocDateTime, string outgoingDocNumber);
        void SetUploadDocumentSource(MemoryStream memoryStream);
    }
    public partial class DocService(ILogger<DocService> logger, IConfiguration config, IAppCommonParams appCommonParams, IDbService dbService) : IDocService
    {
        private readonly ILogger<DocService> _logger = logger;
        private readonly IConfiguration _config = config;
        private readonly IAppCommonParams _appCommonParams = appCommonParams;
        private readonly IDbService _dbService = dbService;


        public INotificationOfReceipt Receipt { get; set; }
        
        public INotificationOfAcceptance Acceptance { get; set; }
        
        public IUploadDocumentHeader DocHeader { get; set; }

        public void SetUploadDocumentSource(MemoryStream memoryStream)
        {
            memoryStream.Position = 0;
            using XmlReader reader = XmlReader.Create(memoryStream, ReaderHelper.GetXmlReaderSettings());

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (DocHeader.Xml != null && reader.Name.Equals("Source"))
                    {
                        DocHeader.Xml.Add(XElement.Parse(reader.ReadOuterXml()));
                        DocHeader.SourceCode = 1;
                    }

                    if (reader.Name.Equals("Data_AF"))
                    {
                        DocHeader.HasDataAF = true;
                        break;
                    }

                    if (reader.Name.Equals("Data"))
                    {
                        DocHeader.HasDataAF = false;
                        break;
                    }
                }
            }
        }
        
        public IUploadDocumentHeader? GetUploadDocumentHeader(MemoryStream memoryStream)
        {
            memoryStream.Position = 0;
            using XmlReader reader = XmlReader.Create(memoryStream, ReaderHelper.GetXmlReaderSettings());
            XElement? xUploadDocument = default;

            StringWriter sw = new(CultureInfo.InvariantCulture);

            XmlWriterSettings writerSettings = new()
            {
                Indent = true
            };

            var xmlWriter = XmlWriter.Create(sw, writerSettings);

            reader.MoveToContent();

            if (reader.NodeType == XmlNodeType.Element)
            {
                xmlWriter.WriteStartElement(reader.Name);
                xmlWriter.WriteAttributes(reader, default);
                xmlWriter.WriteEndElement();
                xmlWriter.Close();
                sw.Close();

                xUploadDocument = XElement.Parse(sw.ToString());
            }

            var schemaVersion = xUploadDocument?.Attribute("schemaVersion")?.Value;

            if (schemaVersion != null && xUploadDocument != null)
            {
                if (schemaVersion.Equals("5.0"))
                {
                    return new Classes.v50.UploadDocumentHeader(xUploadDocument);
                }
                else if (schemaVersion.Equals("5.1"))
                {
                    return new Classes.v51.UploadDocumentHeader(xUploadDocument);
                }
                else
                {
                    return new Classes.v41.UploadDocumentHeader(xUploadDocument);
                }
            }

            return null;
        }

        public UploadPackageInfoExt? GetPreviousRegNumberDocs(int legalPersonId, DateTime docDateTime, DateTime created, string outgoingDocDateTime, string outgoingDocNumber)
        {

            return _dbService.GetPreviousPackages(legalPersonId, docDateTime, created, outgoingDocDateTime, outgoingDocNumber);
        }
    }
}
