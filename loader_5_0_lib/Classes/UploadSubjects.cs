using loader_5_0_lib.Helpers;
using loader_utils;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;

namespace loader_5_0_lib.Classes
{
    public class UploadSubjects(MemoryStream memoryStream)
    {
        public IEnumerator<UploadSubject> GetEnumerator()
        {
            string dataNodeName = string.Empty;
            ulong subjectNum = default;
            List<string> subjectNodes = ["Subject_FL", "Subject_UL"];

            memoryStream.Position = 0;

            using XmlReader reader = XmlReader.Create(memoryStream, ReaderHelper.GetXmlReaderSettings());

            reader.MoveToContent();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name.In("Data", "Data_AF"))
                    {
                        dataNodeName = reader.Name;
                    }

                    if (subjectNodes.Contains(reader.Name))
                    {
                        while (subjectNodes.Contains(reader.Name))
                        {
                            subjectNum++;

                            using StringWriter sw = new(CultureInfo.InvariantCulture);
                            XmlWriterSettings writerSettings = new()
                            {
                                Indent = true
                            };

                            using XmlWriter xmlWriter = XmlWriter.Create(sw, writerSettings);

                            xmlWriter.WriteStartElement(reader.Name);
                            xmlWriter.WriteAttributes(reader, default);
                            xmlWriter.WriteEndElement();
                            xmlWriter.Close();
                            sw.Close();

                            UploadSubject subject = new(subjectNum, XElement.Load(new StringReader(sw.ToString())), XElement.Parse(reader.ReadOuterXml()), dataNodeName);
                            yield return subject;
                        }
                    }
                }
            }
        }
    }
}
