using loader_utils;
using System.Xml.Linq;

namespace Xsd
{
    public abstract class ObjectDeserializer
    {
        #region Document
        public abstract IDocument GetDocumentObject(XElement documentElement);

        protected static IDocument GetDocumentObject<T>(XElement documentElement) where T : IDocument
        {
            return SerializeHelper.Deserialize<T>(documentElement.ToString());
        }
        #endregion

        #region Source
        public abstract ISource GetSourceObject(XElement sourceElement);

        protected static ISource GetSourceObject<T>(XElement sourceElement) where T : IDocumentSource
        {
            return SerializeHelper.Deserialize<T>(sourceElement.ToString()).Source;
        }
        #endregion

        #region Title
        public abstract ISubjectTitleData GetTitleObject(XElement titleElement, SubjectType subjectType);

        protected static ISubjectTitleData GetTitleObject<T_FL, T_UL>(XElement titleElement, SubjectType subjectType)
           where T_FL : ISubjectTitleData
           where T_UL : ISubjectTitleData
       => subjectType switch
       {
           SubjectType.FL => SerializeHelper.Deserialize<T_FL>(titleElement.ToString()),
           SubjectType.UL => SerializeHelper.Deserialize<T_UL>(titleElement.ToString()),
           _ => throw new NotImplementedException()
       };

        protected static ISubjectTitleData GetTitleObject<T_FL, T_UL, T_AF>(XElement titleElement, SubjectType subjectType)
            where T_FL : ISubjectTitleData
            where T_UL : ISubjectTitleData
            where T_AF : ISubjectTitleData
        => subjectType switch
        {
            SubjectType.FL => SerializeHelper.Deserialize<T_FL>(titleElement.ToString()),
            SubjectType.UL => SerializeHelper.Deserialize<T_UL>(titleElement.ToString()),
            SubjectType.AF => SerializeHelper.Deserialize<T_AF>(titleElement.ToString()),
            _ => throw new NotImplementedException()
        };
        #endregion

        #region Event
        public abstract IEventData GetEventObject(XElement eventElement, SubjectType subjectType);

        protected static IEventData GetEventObject<T_FL, T_UL>(XElement eventElement, SubjectType subjectType)
            where T_FL : ISubjectEventData
            where T_UL : ISubjectEventData
        => subjectType switch
        {

            SubjectType.FL => SerializeHelper.Deserialize<T_FL>(new XElement("Event_FL", eventElement).ToString()).Event,
            SubjectType.UL => SerializeHelper.Deserialize<T_UL>(new XElement("Event_UL", eventElement).ToString()).Event,
            _ => throw new NotImplementedException()
        };

        protected static IEventData GetEventObject<T_FL, T_UL, T_AF>(XElement eventElement, SubjectType subjectType)
            where T_FL : ISubjectEventData
            where T_UL : ISubjectEventData
            where T_AF : ISubjectEventData
        => subjectType switch
        {

            SubjectType.FL => SerializeHelper.Deserialize<T_FL>(new XElement("Event_FL", eventElement).ToString()).Event,
            SubjectType.UL => SerializeHelper.Deserialize<T_UL>(new XElement("Event_UL", eventElement).ToString()).Event,
            SubjectType.AF => SerializeHelper.Deserialize<T_AF>(new XElement("Event_AF", eventElement).ToString()).Event,
            _ => throw new NotImplementedException()
        };
        #endregion
    }
}
