using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_37_WarrantyReturn_Type
    {
        ValueField<DateTime?> calcDateField;
        ValueField<bool?> complianceField;
        ValueField<string?> currencyField;
        ValueField<bool?> existField;
        ValueField<decimal?> paidSumField;
        ValueField<decimal?> sumField;

        [XmlIgnore]
        public DateTime? calcDate
        {
            get
            {
                if (!calcDateField.IsInited)
                    calcDateField.Value = Items.GetDateTime(ItemsElementName, "calcDate");
                return calcDateField.Value;
            }
        }

        [XmlIgnore]
        public bool? compliance
        {
            get
            {
                if (!complianceField.IsInited)
                    complianceField.Value = Items.GetBool(ItemsElementName, "compliance");
                return complianceField.Value;
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
        public decimal? paidSum
        {
            get
            {
                if (!paidSumField.IsInited)
                    paidSumField.Value = Items.GetDecimal(ItemsElementName, "paidSum");
                return paidSumField.Value;
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