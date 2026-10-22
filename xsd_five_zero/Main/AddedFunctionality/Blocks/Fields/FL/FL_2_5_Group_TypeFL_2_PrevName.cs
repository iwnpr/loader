using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_2_5_Group_TypeFL_2_PrevName
    {
        ValueField<DateTime?> dateField;
        ValueField<string?> firstNameField;
        ValueField<string?> lastNameField;
        ValueField<string?> middleNameField;
        ValueField<bool?> prevNameFlagField;

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
        public string? firstName
        {
            get
            {
                if (!firstNameField.IsInited)
                    firstNameField.Value = Items.GetString(ItemsElementName, "firstName");
                return firstNameField.Value;
            }
        }

        [XmlIgnore]
        public string? lastName
        {
            get
            {
                if (!lastNameField.IsInited)
                    lastNameField.Value = Items.GetString(ItemsElementName, "lastName");
                return lastNameField.Value;
            }
        }

        [XmlIgnore]
        public string? middleName
        {
            get
            {
                if (!middleNameField.IsInited)
                    middleNameField.Value = Items.GetString(ItemsElementName, "middleName");
                return middleNameField.Value;
            }
        }

        [XmlIgnore]
        public bool? prevNameFlag
        {
            get
            {
                if (!prevNameFlagField.IsInited)
                    prevNameFlagField.Value = Items.GetBool(ItemsElementName, "prevNameFlag");
                return prevNameFlagField.Value;
            }
        }
    }
}