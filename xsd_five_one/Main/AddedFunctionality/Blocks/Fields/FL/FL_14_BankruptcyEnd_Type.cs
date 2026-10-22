using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_14_BankruptcyEnd_Type
    {
        ValueField<bool?> bankruptcyEndFactField;
        ValueField<DateTime?> completionDateField;
        ValueField<DateTime?> resumeDateField;
        ValueField<DateTime?> settlementDateField;
        ValueField<bool?> settlementExistField;

        [XmlIgnore]
        public bool? bankruptcyEndFact
        {
            get
            {
                if (!bankruptcyEndFactField.IsInited)
                    bankruptcyEndFactField.Value = Items.GetBool(ItemsElementName, "bankruptcyEndFact");
                return bankruptcyEndFactField.Value;
            }
        }

        [XmlIgnore]
        public DateTime? completionDate
        {
            get
            {
                if (!completionDateField.IsInited)
                    completionDateField.Value = Items.GetDateTime(ItemsElementName, "completionDate");
                return completionDateField.Value;
            }
        }

        [XmlIgnore]
        public DateTime? resumeDate
        {
            get
            {
                if (!resumeDateField.IsInited)
                    resumeDateField.Value = Items.GetDateTime(ItemsElementName, "resumeDate");
                return resumeDateField.Value;
            }
        }

        [XmlIgnore]
        public DateTime? settlementDate
        {
            get
            {
                if (!settlementDateField.IsInited)
                    settlementDateField.Value = Items.GetDateTime(ItemsElementName, "settlementDate");
                return settlementDateField.Value;
            }
        }

        [XmlIgnore]
        public bool? settlementExist
        {
            get
            {
                if (!settlementExistField.IsInited)
                    settlementExistField.Value = Items.GetBool(ItemsElementName, "settlementExist");
                return settlementExistField.Value;
            }
        }
    }
}
