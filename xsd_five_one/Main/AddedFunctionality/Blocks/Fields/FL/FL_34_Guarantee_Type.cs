using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_34_Guarantee_Type
    {
        ValueField<string?> currencyField;
        ValueField<ushort?> endCodeField;
        ValueField<string?> endDateField;
        ValueField<bool?> existField;
        ValueField<DateTime?> factEndDateField;
        ValueField<string?> openDateField;
        ValueField<string?> sumField;
        ValueField<string?> uidField;

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
        public ushort? endCode
        {
            get
            {
                if (!endCodeField.IsInited)
                    endCodeField.Value = Items.GetUshort(ItemsElementName, "endCode");
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
        public DateTime? factEndDate
        {
            get
            {
                if (!factEndDateField.IsInited)
                    factEndDateField.Value = Items.GetDateTime(ItemsElementName, "factEndDate");
                return factEndDateField.Value;
            }
        }

        [XmlIgnore]
        public string? openDate
        {
            get
            {
                if (!openDateField.IsInited)
                    openDateField.Value = Items.GetString(ItemsElementName, "openDate");
                return openDateField.Value;
            }
        }

        [XmlIgnore]
        public string? sum
        {
            get
            {
                if (!sumField.IsInited)
                    sumField.Value = Items.GetString(ItemsElementName, "sum");
                return sumField.Value;
            }
        }

        [XmlIgnore]
        public string? uid
        {
            get
            {
                if (!uidField.IsInited)
                    uidField.Value = Items.GetString(ItemsElementName, "uid");
                return uidField.Value;
            }
        }
    }
}
