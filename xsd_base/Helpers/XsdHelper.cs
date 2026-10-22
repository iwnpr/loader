using System.Collections.Concurrent;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Xsd
{
    public enum SchemaSetType
    {
        Unmodified,
        DateTimeCheck
    }

    public static class XsdHelper
    {
        private readonly static ConcurrentDictionary<string, Dictionary<SchemaSetType, XmlSchemaSet>> _xmlSchemaDictionary = [];

        private static XmlSchemaSet InitXmlSchemaSet(string fileLocation)
        {
            XmlSchemaSet xmlSchemaSet = new()
            {
                XmlResolver = new XmlUrlResolver()
            };
            xmlSchemaSet.Add(null, Path.Combine(AppContext.BaseDirectory, fileLocation));
            return xmlSchemaSet;
        }

        private static XmlSchemaSet GetXmlSchemaSet(string schemaVersion, SchemaSetType schemaType = SchemaSetType.Unmodified)
        {
            if (!_xmlSchemaDictionary.ContainsKey(schemaVersion))
            {
                Dictionary<SchemaSetType, XmlSchemaSet> schemaDictionary = [];
                schemaDictionary.Add(SchemaSetType.Unmodified, InitXmlSchemaSet($"{schemaVersion}/MainValidation.xsd"));
                schemaDictionary.Add(SchemaSetType.DateTimeCheck, InitXmlSchemaSet($"{schemaVersion}/MainValidation.DateCheck.xsd"));
                _xmlSchemaDictionary.TryAdd(schemaVersion, schemaDictionary);
            }
            return _xmlSchemaDictionary[schemaVersion][schemaType];
        }

        private static XDocument GetSubjectForXsdCheck(XElement xmlElement, SubjectType subjectType) => subjectType switch
        {
            SubjectType.FL => new XDocument(new XElement("Subject_FL", xmlElement)),
            SubjectType.UL => new XDocument(new XElement("Subject_UL", xmlElement)),
            SubjectType.AF => new XDocument(new XElement("Subject_AF", xmlElement)),
            _ => throw new NotImplementedException()
        };

        private static XDocument GetEventForXsdCheck(XElement xmlElement, SubjectType subjectType) => subjectType switch
        {
            SubjectType.FL => new XDocument(new XElement("Event_FL", xmlElement)),
            SubjectType.UL => new XDocument(new XElement("Event_UL", xmlElement)),
            SubjectType.AF => new XDocument(new XElement("Event_AF", xmlElement)),
            _ => throw new NotImplementedException()
        };

        public static string? ValidateByXsd(XDocument xmlDocument, string schemaVersion, SchemaSetType schemaType = SchemaSetType.Unmodified)
        {
            string? errorMessage = null;
            xmlDocument.Validate(GetXmlSchemaSet(schemaVersion, schemaType), (o, e) =>
            {
                errorMessage = e.Message;
            });
            return errorMessage;
        }

        public static string? ValidateSubjectByXsd(XElement xmlElement, string schemaVersion, SubjectType subjectType, SchemaSetType schemaType = SchemaSetType.Unmodified)
        {
            return ValidateByXsd(GetSubjectForXsdCheck(xmlElement, subjectType), schemaVersion, schemaType);
        }

        public static string? ValidateEventByXsd(XElement xmlElement, string schemaVersion, SubjectType subjectType, SchemaSetType schemaType = SchemaSetType.Unmodified)
        {
            return ValidateByXsd(GetEventForXsdCheck(xmlElement, subjectType), schemaVersion, schemaType);
        }
    }
}
