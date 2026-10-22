using loader_5_0_lib.Helpers;
using loader_utils;

namespace loader_5_0_lib.Classes
{
    public class UploadDocInfo(MemoryStream docMemStream, string? uploadFileName, ActiveUpload activeUpload)
    {
        private readonly MemoryStream _docMemStream = docMemStream;
        public string FileName = uploadFileName ?? string.Empty;
        public string FileHash = HashHelper.GetFileHash(docMemStream);
        public readonly int SourceId = activeUpload.UploadInfo.UserInfo.ClientId;
        public DateTime UploadCreated = activeUpload.Created.RemoveMilliseconds();
        public MemoryStream DocMemStream
        {
            get { _docMemStream.Position = 0; return _docMemStream; }
        }

        public UploadDocInfo(string uploadFullFileName, ActiveUpload activeUpload) :
            this(HashHelper.GetMemoryStream(uploadFullFileName), Path.GetFileName(uploadFullFileName), activeUpload)
        { }
    }
}