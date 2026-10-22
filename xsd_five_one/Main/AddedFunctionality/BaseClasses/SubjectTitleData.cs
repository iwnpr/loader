using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    [XmlRoot(ElementName = "Title")]
    public partial class SubjectTitleDataFL : ISubjectTitleData
    {
        bool _skipTitleChecksInChangeEvents;
        readonly CurrentBlockCache _currentBlockCache = new();

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
        bool _skipTitleChecksInChangeEvents;
        readonly CurrentBlockCache _currentBlockCache = new();

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

    [XmlRoot(ElementName = "Title")]
    public partial class SubjectTitleDataAF : ISubjectTitleData
    {
        bool _skipTitleChecksInChangeEvents;
        readonly CurrentBlockCache _currentBlockCache = new();

        [XmlIgnore]
        public SubjectType SubjectType
        {
            get
            {
                return SubjectType.AF;
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