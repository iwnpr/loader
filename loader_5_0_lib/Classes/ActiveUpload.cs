using System.Xml.Linq;

namespace loader_5_0_lib.Classes
{
    public class ActiveUpload
    {
        public long KeyId { get; }
        public DateTime Created { get; }
        public string? SourceItn { get; }
        public string? SourcePsrn { get; }
        public int LegalPosition { get; }
        public UploadInfo UploadInfo { get; }
        public XElement UploadingXML { get; }
        public string UploadPath { get; set; }
        public string FileSystemProcessingPath { get; set; } = string.Empty;
        public ActiveUpload(long keyId, DateTime created, string? sourceItn, string? sourcePsrn, int legalPosition, XElement uploadingXML)
        {
            KeyId = keyId;
            Created = created;
            SourceItn = sourceItn;
            SourcePsrn = sourcePsrn;
            LegalPosition = legalPosition;
            UploadingXML = uploadingXML;

            UploadInfo = new(UploadingXML);

            UploadPath = Path.Combine(
                UploadInfo.UserInfo.ClientId.ToString().PadLeft(5, '0'),
                UploadInfo.PackageInfo.UploadId.ToString()[..10],
                UploadInfo.PackageInfo.UploadId.ToString());
        }

    }
}
