using System.Xml;

namespace loader_utils
{
    public static class ReaderHelpers
    {
        public static XmlReaderSettings GetXmlReaderSettings()
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
    }
}
