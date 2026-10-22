using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_46_UL_36_OrgSource_Type
    {
        ValueField<bool?> sourceRegistrationFactField;

        [XmlIgnore]
        public bool? sourceRegistrationFact
        {
            get
            {
                if (!sourceRegistrationFactField.IsInited)
                    sourceRegistrationFactField.Value = Item.GetBool(ItemElementName, "sourceRegistrationFact");
                return sourceRegistrationFactField.Value;
            }
        }
    }
}
