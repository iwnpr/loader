using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Serialization;

namespace loader_utils
{
    public static class SerializeHelper
    {
        public static T Deserialize<T>(string xmlData)
        {
            using TextReader reader = new StringReader(xmlData);
            var serializer = new XmlSerializer(typeof(T));
            var deserialedobject = serializer.Deserialize(reader);
            return deserialedobject != null ? (T)deserialedobject : throw new Exception("Ошибка десериализации");
        }

        public static string? Serialize(object xmlObject, Encoding encoding)
        {
            if (xmlObject == null)
            {
                return null;
            }
            using var stringwriter = new ExtendedStringWriter(encoding);
            var serializer = new XmlSerializer(xmlObject.GetType());
            serializer.Serialize(stringwriter, xmlObject);
            return stringwriter.ToString();
        }

        [return: NotNullIfNotNull(nameof(xmlObject))]
        public static string? Serialize(object xmlObject)
        {
            if (xmlObject == null)
            {
                return null;
            }
            using var stringwriter = new StringWriter();
            var serializer = new XmlSerializer(xmlObject.GetType());
            serializer.Serialize(stringwriter, xmlObject);
            return stringwriter.ToString();
        }

        public class ExtendedStringWriter(Encoding encoding) : StringWriter
        {
            public override Encoding Encoding { get; } = encoding;
        }
    }
}
