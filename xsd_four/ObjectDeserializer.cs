using System.Xml.Linq;

namespace Xsd.VersionFour.Main
{
    public class ObjectDeserializer : Xsd.ObjectDeserializer
    {
        public override IDocument GetDocumentObject(XElement documentElement)
        {
            return GetDocumentObject<Document>(documentElement);
        }

        public override ISource GetSourceObject(XElement sourceElement)
        {
            return GetSourceObject<DocumentSource>(sourceElement);
        }

        public override ISubjectTitleData GetTitleObject(XElement titleElement, SubjectType subjectType)
        {
            return GetTitleObject<SubjectTitleDataFL, SubjectTitleDataUL>(titleElement, subjectType);
        }

        public override IEventData GetEventObject(XElement eventElement, SubjectType subjectType)
        {
            return GetEventObject<SubjectEventDataFL, SubjectEventDataUL>(eventElement, subjectType);
        }
    }
}