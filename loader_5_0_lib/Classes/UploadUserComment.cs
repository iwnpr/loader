using System.Globalization;
using System.Xml.Linq;

namespace loader_5_0_lib.Classes
{
    public class UploadUserComment
    {
        long UserId { get; }
        int OrganizationId { get; }
        string Email { get; }
        string Name { get; }
        DateTime Created { get; }
        string Text { get; set; }
        string NotificationToEmail { get; }
        string NotificationToName { get; }
        public XElement Xml { get { return GetXml(); } }

        public UploadUserComment(long userId, int organisationId, string email, string name, string text, string notificationToEmail, string notificationToName)
        {
            UserId = userId;
            OrganizationId = organisationId;
            Email = email;
            Name = name;
            Created = DateTime.Now;
            Text = text;
            NotificationToEmail = notificationToEmail;
            NotificationToName = notificationToName;
        }
        public UploadUserComment(XElement userComment)
        {
            UserId = long.Parse(userComment.Attribute("user-id").Value);
            OrganizationId = int.Parse(userComment.Attribute("organization-id").Value);
            Email = userComment.Attribute("email").Value;
            Name = userComment.Attribute("name").Value;
            Created = DateTime.Parse(userComment.Attribute("created").Value, new CultureInfo("de-DE"), DateTimeStyles.None);
            Text = userComment.Element("text").Value;
            NotificationToEmail = userComment.Element("notification-to").Attribute("email").Value;
            NotificationToName = userComment.Element("notification-to").Attribute("name").Value;
        }
        public XElement GetXml()
        {
            XElement ret = new XElement("user");
            ret.SetAttributeValue("user-id", UserId);
            ret.SetAttributeValue("organization-id", OrganizationId);
            ret.SetAttributeValue("email", Email);
            ret.SetAttributeValue("name", Name);
            ret.SetAttributeValue("created", Created);
            ret.Add(new XElement("text", Text));
            ret.Add(new XElement("notification-to", new XAttribute("email", NotificationToEmail), new XAttribute("name", NotificationToName)));
            return ret;
        }
    }
}