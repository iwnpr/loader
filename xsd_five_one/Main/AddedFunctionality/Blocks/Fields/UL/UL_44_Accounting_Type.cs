using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_44_Accounting_Type
    {
        ValueField<string?> currencyField;
        ValueField<bool?> existField;
        ValueField<decimal?> sumField;

        [XmlIgnore]
        public string? currency
        {
            get
            {
                if (!currencyField.IsInited)
                    currencyField.Value = Items.GetString(ItemsElementName, "currency");
                return currencyField.Value;
            }
        }

        [XmlIgnore]
        public bool? exist
        {
            get
            {
                if (!existField.IsInited)
                    existField.Value = Items.GetBool(ItemsElementName, "exist");
                return existField.Value;
            }
        }

        [XmlIgnore]
        public decimal? sum
        {
            get
            {
                if (!sumField.IsInited)
                    sumField.Value = Items.GetDecimal(ItemsElementName, "sum");
                return sumField.Value;
            }
        }
    }
}