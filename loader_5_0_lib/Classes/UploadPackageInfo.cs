namespace loader_5_0_lib.Classes
{
    public class UploadPackageInfo(long keyId, string id, long legalPersonId, string fileNmae)
    {
        public long UploadKeyId { get; } = keyId;
        public string UploadId { get; } = id;
        public long LegalPersonId { get; } = legalPersonId;
        public string FileName { get; } = fileNmae;
        public string FilePath
        {
            get
            {
                return Path.Combine(LegalPersonId.ToString().PadLeft(5, '0'), UploadId.ToString()[..10], UploadId.ToString(), FileName);
            }
        }
    }
}
