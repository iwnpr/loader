using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_25_26_27_28_Group_TypeFL_26_DebtDue
    {
        ValueField<bool?> debtDueExistField;
        ValueField<decimal?> debtDueMainSumField;
        ValueField<decimal?> debtDueOtherSumField;
        ValueField<decimal?> debtDuePercentSumField;
        ValueField<string?> debtDueStartDateField;
        ValueField<decimal?> debtDueSumField;

        [XmlIgnore]
        public bool? debtDueExist
        {
            get
            {
                if (!debtDueExistField.IsInited)
                    debtDueExistField.Value = Items.GetBool(ItemsElementName, "debtDueExist");
                return debtDueExistField.Value;
            }
        }

        [XmlIgnore]
        public decimal? debtDueMainSum
        {
            get
            {
                if (!debtDueMainSumField.IsInited)
                    debtDueMainSumField.Value = Items.GetDecimal(ItemsElementName, "debtDueMainSum");
                return debtDueMainSumField.Value;
            }
        }

        [XmlIgnore]
        public decimal? debtDueOtherSum
        {
            get
            {
                if (!debtDueOtherSumField.IsInited)
                    debtDueOtherSumField.Value = Items.GetDecimal(ItemsElementName, "debtDueOtherSum");
                return debtDueOtherSumField.Value;
            }
        }

        [XmlIgnore]
        public decimal? debtDuePercentSum
        {
            get
            {
                if (!debtDuePercentSumField.IsInited)
                    debtDuePercentSumField.Value = Items.GetDecimal(ItemsElementName, "debtDuePercentSum");
                return debtDuePercentSumField.Value;
            }
        }

        [XmlIgnore]
        public string? debtDueStartDate
        {
            get
            {
                if (!debtDueStartDateField.IsInited)
                    debtDueStartDateField.Value = Items.GetString(ItemsElementName, "debtDueStartDate");
                return debtDueStartDateField.Value;
            }
        }

        [XmlIgnore]
        public decimal? debtDueSum
        {
            get
            {
                if (!debtDueSumField.IsInited)
                    debtDueSumField.Value = Items.GetDecimal(ItemsElementName, "debtDueSum");
                return debtDueSumField.Value;
            }
        }
    }
}