using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_5_Reorg_Type
    {
        ValueField<DateTime?> dateField;
        ValueField<bool?> existField;
        ValueField<string?> infoField;
        ValueField<string?> fullNameField;
        ValueField<string?> regNumField;
        ValueField<string?[]> shortNameField;

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
        public string? info
        {
            get
            {
                if (!infoField.IsInited)
                    infoField.Value = Items.GetString(ItemsElementName, "info");
                return infoField.Value;
            }
        }

        [XmlIgnore]
        public string? fullName
        {
            get
            {
                if (!fullNameField.IsInited)
                    fullNameField.Value = Items.GetString(ItemsElementName, "fullName");
                return fullNameField.Value;
            }
        }

        [XmlIgnore]
        public string? regNum
        {
            get
            {
                if (!regNumField.IsInited)
                    regNumField.Value = Items.GetString(ItemsElementName, "regNum");
                return regNumField.Value;
            }
        }

        [XmlIgnore]
        public string?[] shortName
        {
            get
            {
                if (!shortNameField.IsInited)
                    shortNameField.Value = Items.GetObjects(ItemsElementName, "shortName").Select(x => x.ToString()).ToArray(); ;
                return shortNameField.Value;
            }
        }
    }
}
