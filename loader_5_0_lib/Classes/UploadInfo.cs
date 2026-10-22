using System.Xml.Linq;

namespace loader_5_0_lib.Classes
{
    public class UploadInfo
    {
        public XElement Xml { get; }
        public UserInfo UserInfo { get; }
        public PackageInfo PackageInfo { get; }
        public Dictionary<int, DocumentInfo> DocsInfo { get; } = new();
        private List<UploadUserComment> Comments { get; set; } = new();
        public UploadInfo(XElement xml)
        {
            Xml = xml;

            UserInfo = new(Xml.Element("user-info"));
            PackageInfo = new(Xml.Element("package-info"));
            int docNum = 0;

            if (xml?.Element("comments")?.Element("user") != null)
            {
                foreach (var xComment in Xml.Element("comments").Elements("user"))
                {
                    Comments.Add(new UploadUserComment(xComment));
                }
            }
            foreach (var xDoc in Xml.Elements("document"))
            {
                docNum++;
                DocsInfo.Add(docNum, new DocumentInfo(xDoc));
            }
        }
        public void AddComment(UploadUserComment uploadUserComment)
        {
            Comments.Add(uploadUserComment);

            if (Xml.Element("comments") == null)
            {
                Xml.Add(new XElement("comments"));
            }

            Xml.Element("comments").Add(uploadUserComment.Xml);
        }
    }
}
