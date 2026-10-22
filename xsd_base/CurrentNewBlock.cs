using System.Xml.Serialization;

namespace Xsd
{
    public abstract class CurrentNewBlock
    {
        [XmlIgnore]
        public abstract object Current
        { get; }

        [XmlIgnore]
        public abstract object New
        { get; }
    }
}
