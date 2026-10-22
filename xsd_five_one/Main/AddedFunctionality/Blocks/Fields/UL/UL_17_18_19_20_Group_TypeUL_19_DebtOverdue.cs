using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_17_18_19_20_Group_TypeUL_19_DebtOverdue
    {
        ValueField<decimal?> debtOverdueMainSumField;
        ValueField<decimal?> debtOverdueOtherSumField;
        ValueField<decimal?> debtOverduePercentSumField;
        ValueField<string?> debtOverdueStartDateField;
        ValueField<decimal?> debtOverdueSumField;
        ValueField<DateTime?> mainMissDateField;
        ValueField<string?> missDurationField;
        ValueField<bool?> missFactField;
        ValueField<DateTime?> percentMissDateField;
        ValueField<ulong?> repaidMissDurationField;

        [XmlIgnore]
        public decimal? debtOverdueMainSum
        {
            get
            {
                if (!debtOverdueMainSumField.IsInited)
                    debtOverdueMainSumField.Value = Items.GetDecimal(ItemsElementName, "debtOverdueMainSum");
                return debtOverdueMainSumField.Value;
            }
        }

        [XmlIgnore]
        public decimal? debtOverdueOtherSum
        {
            get
            {
                if (!debtOverdueOtherSumField.IsInited)
                    debtOverdueOtherSumField.Value = Items.GetDecimal(ItemsElementName, "debtOverdueOtherSum");
                return debtOverdueOtherSumField.Value;
            }
        }

        [XmlIgnore]
        public decimal? debtOverduePercentSum
        {
            get
            {
                if (!debtOverduePercentSumField.IsInited)
                    debtOverduePercentSumField.Value = Items.GetDecimal(ItemsElementName, "debtOverduePercentSum");
                return debtOverduePercentSumField.Value;
            }
        }

        [XmlIgnore]
        public string? debtOverdueStartDate
        {
            get
            {
                if (!debtOverdueStartDateField.IsInited)
                    debtOverdueStartDateField.Value = Items.GetString(ItemsElementName, "debtOverdueStartDate");
                return debtOverdueStartDateField.Value;
            }
        }

        [XmlIgnore]
        public decimal? debtOverdueSum
        {
            get
            {
                if (!debtOverdueSumField.IsInited)
                    debtOverdueSumField.Value = Items.GetDecimal(ItemsElementName, "debtOverdueSum");
                return debtOverdueSumField.Value;
            }
        }

        [XmlIgnore]
        public DateTime? mainMissDate
        {
            get
            {
                if (!mainMissDateField.IsInited)
                    mainMissDateField.Value = Items.GetDateTime(ItemsElementName, "mainMissDate");
                return mainMissDateField.Value;
            }
        }

        [XmlIgnore]
        public string? missDuration
        {
            get
            {
                if (!missDurationField.IsInited)
                    missDurationField.Value = Items.GetString(ItemsElementName, "missDuration");
                return missDurationField.Value;
            }
        }

        [XmlIgnore]
        public bool? missFact
        {
            get
            {
                if (!missFactField.IsInited)
                    missFactField.Value = Items.GetBool(ItemsElementName, "missFact");
                return missFactField.Value;
            }
        }

        [XmlIgnore]
        public DateTime? percentMissDate
        {
            get
            {
                if (!percentMissDateField.IsInited)
                    percentMissDateField.Value = Items.GetDateTime(ItemsElementName, "percentMissDate");
                return percentMissDateField.Value;
            }
        }

        [XmlIgnore]
        public ulong? repaidMissDuration
        {
            get
            {
                if (!repaidMissDurationField.IsInited)
                    repaidMissDurationField.Value = Items.GetUlong(ItemsElementName, "repaidMissDuration");
                return repaidMissDurationField.Value;
            }
        }
    }
}