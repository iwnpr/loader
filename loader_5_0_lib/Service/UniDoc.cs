using loader_5_0_lib.Classes;
using System.Security.Cryptography;

namespace loader_5_0_lib.Service
{
    public class UniDoc
    {
        public string? SchemaVersion { get; set; }
        public ulong SubjectsReceived { get; set; } = default;
        public ulong EventsReceived { get; set; } = default;
        public ulong EventsAccepted { get; set; } = default;
        public ulong EventsRejected { get; set; } = default;
        private FileInfo DocFileInfo { get; }
        public string FullFileName { get { return DocFileInfo.FullName; } }
        public string FileName { get { return DocFileInfo.Name; } }
        public string? FileHash { get; }
        public long UploadKeyId { get; }
        public int? ClientId { get; set; }
        public string? DateTime { get; set; }
        public ActiveUpload ActiveUpload { get; }
        public UniDoc(string docFullFileName, ActiveUpload activeUpload)
        {
            ActiveUpload = activeUpload;
            UploadKeyId = activeUpload.KeyId;
            DocFileInfo = new FileInfo(docFullFileName);

            using (SHA256 mySHA256 = SHA256.Create())
            {
                using (FileStream fileStream = DocFileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    fileStream.Position = 0;
                    byte[] hashValue = mySHA256.ComputeHash(fileStream);
                    FileHash = BitConverter.ToString(hashValue).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}