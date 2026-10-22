using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class FL_23_1_ContractTermsChanges_Type
    {
        ValueField<bool?> changeFactField;
        ValueField<string?> changingDateField;
        ValueField<ushort?> termsChangeCodeField;
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
        public ushort? termsChangeCode
        {
            get
            {
                if (!termsChangeCodeField.IsInited)
                    termsChangeCodeField.Value = Items.GetUshort(ItemsElementName, "termsChangeCode");
                return termsChangeCodeField.Value;
            }
        }

        [XmlIgnore]
        public string termsChangeDesc
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
