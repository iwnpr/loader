using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class UL_27_1_ProvisionPaymentOffset_Type
    {
        ValueField<ushort?> codeField;
        ValueField<string?> currencyField;
        ValueField<DateTime?> dateField;
        ValueField<bool?> existField;
        ValueField<decimal?> sumField;
        ValueField<ushort?> wayCodeField;

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

        [XmlIgnore]
        public ushort? wayCode
        {
            get
            {
                if (!wayCodeField.IsInited)
                    wayCodeField.Value = Items.GetUshort(ItemsElementName, "wayCode");
                return wayCodeField.Value;
            }
        }
    }
}
