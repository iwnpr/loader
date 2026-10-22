using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_23_ContractChanges_Type
    {
        ValueField<DateTime?> actualEndDateField;
        ValueField<string?> changeDateField;
        ValueField<refContractChangeType?> codeField;
        ValueField<decimal?> currencyRateField;
        ValueField<refReasonTerminationContractChangeType?> endCodeField;
        ValueField<string?> endDateField;
        ValueField<bool?> existField;
        ValueField<string?> otherDescField;
        ValueField<refSpecialContractChangeType?> specialCodeField;
        ValueField<string?> startDateField;

        [XmlIgnore]
        public DateTime? actualEndDate
        {
            get
            {
                if (!actualEndDateField.IsInited)
                    actualEndDateField.Value = Items.GetDateTime(ItemsElementName, "actualEndDate");
                return actualEndDateField.Value;
            }
        }

        [XmlIgnore]
        public string? changeDate
        {
            get
            {
                if (!changeDateField.IsInited)
                    changeDateField.Value = Items.GetString(ItemsElementName, "changeDate");
                return changeDateField.Value;
            }
        }

        [XmlIgnore]
        public refContractChangeType? code
        {
            get
            {
                if (!codeField.IsInited)
                    codeField.Value = (refContractChangeType?)Items.GetObject(ItemsElementName, "code");
                return codeField.Value;
            }
        }

        [XmlIgnore]
        public decimal? currencyRate
        {
            get
            {
                if (!currencyRateField.IsInited)
                    currencyRateField.Value = Items.GetDecimal(ItemsElementName, "currencyRate");
                return currencyRateField.Value;
            }
        }

        [XmlIgnore]
        public refReasonTerminationContractChangeType? endCode
        {
            get
            {
                if (!endCodeField.IsInited)
                    endCodeField.Value = (refReasonTerminationContractChangeType?)Items.GetObject(ItemsElementName, "endCode");
                return endCodeField.Value;
            }
        }

        [XmlIgnore]
        public string? endDate
        {
            get
            {
                if (!endDateField.IsInited)
                    endDateField.Value = Items.GetString(ItemsElementName, "endDate");
                return endDateField.Value;
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
        public string? otherDesc
        {
            get
            {
                if (!otherDescField.IsInited)
                    otherDescField.Value = Items.GetString(ItemsElementName, "otherDesc");
                return otherDescField.Value;
            }
        }

        [XmlIgnore]
        public refSpecialContractChangeType? specialCode
        {
            get
            {
                if (!specialCodeField.IsInited)
                    specialCodeField.Value = (refSpecialContractChangeType?)Items.GetObject(ItemsElementName, "specialCode");
                return specialCodeField.Value;
            }
        }

        [XmlIgnore]
        public string? startDate
        {
            get
            {
                if (!startDateField.IsInited)
                    startDateField.Value = Items.GetString(ItemsElementName, "startDate");
                return startDateField.Value;
            }
        }
    }
}