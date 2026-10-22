namespace Xsd.VersionFiveOne.Main
{
    [System.Xml.Serialization.XmlRoot(ElementName = "Event_FL")]
    public partial class SubjectEventDataFL : ISubjectEventData
    {
        public IEventData Event
        {
            get
            {
                return Items[0];
            }
        }
    }

    [System.Xml.Serialization.XmlRoot(ElementName = "Event_UL")]
    public partial class SubjectEventDataUL : ISubjectEventData
    {
        public IEventData Event
        {
            get
            {
                return Items[0];
            }
        }
    }

    [System.Xml.Serialization.XmlRoot(ElementName = "Event_AF")]
    public partial class SubjectEventDataAF : ISubjectEventData
    {
        public IEventData Event
        {
            get
            {
                return Items[0];
            }
        }
    }
}