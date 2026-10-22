using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_27_ProvisionPayment_Type
    {
        ValueField<ushort?> codeField;
        ValueField<string?> currencyField;
        ValueField<DateTime?> dateField;
        ValueField<bool?> existField;
        ValueField<decimal?> sumField;

        [XmlIgnore]
        public decimal? code
        {
            get
            {
                if (!codeField.IsInited)
                    codeField.Value = Items.GetUshort(ItemsElementName, "code");
                return codeField.Value;
            }
        }

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
        public DateTime? date
        {
            get
            {
                if (!dateField.IsInited)
                    dateField.Value = Items.GetDateTime(ItemsElementName, "date");
                return dateField.Value;
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
