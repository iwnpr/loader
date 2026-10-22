namespace loader_5_0_lib.Classes
{
    public class UploadPackageInfoExt : UploadPackageInfo
    {
        public readonly string OutgoingDocDate;
        public readonly string OutgoingDocNumber;

        public UploadPackageInfoExt(long uploadKeyId, string uploadId, long legalPersonId, string fileName, string outgoingDocDate, string outgoingDocNumber) : base(uploadKeyId, uploadId, legalPersonId, fileName)
        {
            this.OutgoingDocDate = outgoingDocDate;
            this.OutgoingDocNumber = outgoingDocNumber;
        }
    }
}