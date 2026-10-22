using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_12_1_AmountInfo_Type
    {
        ValueField<string?> calcDateField;
        ValueField<ulong?> liabilityLimitField;
        ValueField<string?> limitCurrencyField;
        ValueField<string?> securityCurrencyField;
        ValueField<bool?> securityFactField;
        ValueField<decimal?> securitySumField;
        ValueField<ushort?> securityTypeCodeField;
        ValueField<string?> securityUidField;

        [XmlIgnore]
        public string? calcDate
        {
            get
            {
                if (!calcDateField.IsInited)
                    calcDateField.Value = Items.GetString(ItemsElementName, "calcDate");
                return calcDateField.Value;
            }
        }

        [XmlIgnore]
        public ulong? liabilityLimit
        {
            get
            {
                if (!liabilityLimitField.IsInited)
                    liabilityLimitField.Value = Items.GetUlong(ItemsElementName, "liabilityLimit");
                return liabilityLimitField.Value;
            }
        }

        [XmlIgnore]
        public string? limitCurrency
        {
            get
            {
                if (!limitCurrencyField.IsInited)
                    limitCurrencyField.Value = Items.GetString(ItemsElementName, "limitCurrency");
                return limitCurrencyField.Value;
            }
        }

        [XmlIgnore]
        public string? securityCurrency
        {
            get
            {
                if (!securityCurrencyField.IsInited)
                    securityCurrencyField.Value = Items.GetString(ItemsElementName, "securityCurrency");
                return securityCurrencyField.Value;
            }
        }

        [XmlIgnore]
        public bool? securityFact
        {
            get
            {
                if (!securityFactField.IsInited)
                    securityFactField.Value = Items.GetBool(ItemsElementName, "securityFact");
                return securityFactField.Value;
            }
        }

        [XmlIgnore]
        public decimal? securitySum
        {
            get
            {
                if (!securitySumField.IsInited)
                    securitySumField.Value = Items.GetDecimal(ItemsElementName, "securitySum");
                return securitySumField.Value;
            }
        }

        [XmlIgnore]
        public ushort? securityTypeCode
        {
            get
            {
                if (!securityTypeCodeField.IsInited)
                    securityTypeCodeField.Value = Items.GetUshort(ItemsElementName, "securityTypeCode");
                return securityTypeCodeField.Value;
            }
        }

        [XmlIgnore]
        public string? securityUid
        {
            get
            {
                if (!securityUidField.IsInited)
                    securityUidField.Value = Items.GetString(ItemsElementName, "securityUid");
                return securityUidField.Value;
            }
        }
    }
}