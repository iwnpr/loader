using System.Xml;

namespace loader_utils
{
    public static class XmlReaderExtensions
    {
        public static bool SafeRead(this XmlReader xmlReader, out string? error)
        {
            try
            {
                error = "";
                return xmlReader.Read();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public static bool SafeRead(this XmlReader xmlReader)
        {
            try
            {
                return xmlReader.Read();
            }
            catch (Exception ex)
            {
                throw new XmlReaderException(ex.Message);
            }
        }
    }

    public class XmlReaderException : Exception
    {
        public XmlReaderException(string message)
       : base(message) { }
    }
}
