using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_15_1_ContractTermsChanges_Type
    {
        ValueField<bool?> changeFactField;
        ValueField<string?> changingDateField;
        ValueField<refContractOtherChangeType?> termsChangeCodeField;
        ValueField<string?> termsChangeDescField;

        [XmlIgnore]
        public bool? changeFact
        {
            get
            {
                if (!changeFactField.IsInited)
                    changeFactField.Value = Items.GetBool(ItemsElementName, "changeFact");
                return changeFactField.Value;
            }
        }

        [XmlIgnore]
        public string? changingDate
        {
            get
            {
                if (!changingDateField.IsInited)
                    changingDateField.Value = Items.GetString(ItemsElementName, "changingDate");
                return changingDateField.Value;
            }
        }

        [XmlIgnore]
        public refContractOtherChangeType? termsChangeCode
        {
            get
            {
                if (!termsChangeCodeField.IsInited)
                    termsChangeCodeField.Value = (refContractOtherChangeType?)Items.GetObject(ItemsElementName, "termsChangeCode");
                return termsChangeCodeField.Value;
            }
        }

        [XmlIgnore]
        public string? termsChangeDesc
        {
            get
            {
                if (!termsChangeDescField.IsInited)
                    termsChangeDescField.Value = Items.GetString(ItemsElementName, "termsChangeDesc");
                return termsChangeDescField.Value;
            }
        }
    }
}