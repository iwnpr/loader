using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_32_35_Group_TypeFL_35_Insurance
    {
        ValueField<ushort?> endCodeField;
        ValueField<bool?> existField;
        ValueField<bool?> franchiseExistField;
        ValueField<string?> insuranceEndDateField;
        ValueField<DateTime?> insuranceFactEndDateField;
        ValueField<string?> startDateField;

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
        public bool? franchiseExist
        {
            get
            {
                if (!franchiseExistField.IsInited)
                    franchiseExistField.Value = Items.GetBool(ItemsElementName, "franchiseExist");
                return franchiseExistField.Value;
            }
        }

        [XmlIgnore]
        public string? insuranceEndDate
        {
            get
            {
                if (!insuranceEndDateField.IsInited)
                    insuranceEndDateField.Value = Items.GetString(ItemsElementName, "insuranceEndDate");
                return insuranceEndDateField.Value;
            }
        }

        [XmlIgnore]
        public DateTime? insuranceFactEndDate
        {
            get
            {
                if (!insuranceFactEndDateField.IsInited)
                    insuranceFactEndDateField.Value = Items.GetDateTime(ItemsElementName, "insuranceFactEndDate");
                return insuranceFactEndDateField.Value;
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