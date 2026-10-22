using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class FL_11_IndividualEntrepreneur_Type
    {
        ValueField<DateTime?> dateField;
        ValueField<DateTime?> endDateField;
        ValueField<bool?> regFactField;
        ValueField<string?> regNumField;

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
        public DateTime? endDate
        {
            get
            {
                if (!endDateField.IsInited)
                    endDateField.Value = Items.GetDateTime(ItemsElementName, "endDate");
                return endDateField.Value;
            }
        }

        [XmlIgnore]
        public bool? regFact
        {
            get
            {
                if (!regFactField.IsInited)
                    regFactField.Value = Items.GetBool(ItemsElementName, "regFact");
                return regFactField.Value;
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
    }
}