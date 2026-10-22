using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    [XmlRoot(ElementName = "Title")]
    public partial class SubjectTitleDataFL : ISubjectTitleData
    {
        readonly CurrentBlockCache _currentBlockCache = new();
        bool _skipTitleChecksInChangeEvents;

        [XmlIgnore]
        public SubjectType SubjectType
        {
            get
            {
                return SubjectType.FL;
            }
        }

        [XmlIgnore]
        public CurrentBlockCache CurrentBlockCache
        {
            get
            {
                return _currentBlockCache;
            }
        }

        [XmlIgnore]
        public bool SkipTitleChecksInChangeEvents
        {
            get
            {
                return _skipTitleChecksInChangeEvents;
            }
            set
            {
                _skipTitleChecksInChangeEvents = value;
            }
        }
    }

    [XmlRoot(ElementName = "Title")]
    public partial class SubjectTitleDataUL : ISubjectTitleData
    {
        readonly CurrentBlockCache _currentBlockCache = new();
        bool _skipTitleChecksInChangeEvents;

        [XmlIgnore]
        public SubjectType SubjectType
        {
            get
            {
                return SubjectType.UL;
            }
        }

        [XmlIgnore]
        public CurrentBlockCache CurrentBlockCache
        {
            get
            {
                return _currentBlockCache;
            }
        }

        [XmlIgnore]
        public bool SkipTitleChecksInChangeEvents
        {
            get
            {
                return _skipTitleChecksInChangeEvents;
            }
            set
            {
                _skipTitleChecksInChangeEvents = value;
            }
        }
    }
}