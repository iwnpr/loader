using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_7_BankruptcyEnd_Type
    {
        ValueField<bool?> bankruptcyEndFactField;
        ValueField<DateTime?> completionDateField;

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
    }
}
