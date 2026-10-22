using loader_5_0_lib.Classes;
using System.Text;
using System.Xml;

namespace loader_5_0_lib.Helpers
{
    public static class ReaderHelper
    {
        internal static XmlReaderSettings GetXmlReaderSettings()
        {
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings()
            {
                ValidationType = ValidationType.None,
                IgnoreComments = true,
                IgnoreProcessingInstructions = true,
                IgnoreWhitespace = true
            };
            return xmlReaderSettings;
        }

        internal static XmlReader GetXmlReader(MemoryStream memoryStream)
        {
            memoryStream.Position = 0;
            return XmlReader.Create(memoryStream, GetXmlReaderSettings());
        }

        internal static XmlReader GetXmlReader(string fullFullName)
        {
            return XmlReader.Create(fullFullName, GetXmlReaderSettings());
        }

        public static List<string> CheckXmlSyntax(MemoryStream memoryStream)
        {
            List<string> result = [];

            using XmlReader reader = XmlReader.Create(memoryStream);
            try
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element) { }
                }
            }
            catch (Exception ex)
            {
                StringBuilder xmessage = new(ex.Message);

                for (int i = 0; i < xmessage.Length - 1; i++)
                {
                    if (!XmlConvert.IsXmlChar(xmessage[i]))
                    {
                        xmessage.Remove(i, 1);
                    }
                }

                result.Add(xmessage.ToString());
                return result;
            }
            return result;
        }
    }
}
