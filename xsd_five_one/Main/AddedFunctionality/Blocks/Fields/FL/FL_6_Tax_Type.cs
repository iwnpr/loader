using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_6_Tax_Type
    {
        ValueField<bool?> specialModeField;

        [XmlIgnore]
        public bool? specialMode
        {
            get
            {
                if (!specialModeField.IsInited)
                    specialModeField.Value = Item.GetBool(ItemElementName, "specialMode");
                return specialModeField.Value;
            }
        }
    }
}
