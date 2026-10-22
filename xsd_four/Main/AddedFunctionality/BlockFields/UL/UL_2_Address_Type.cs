using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class UL_2_Address_Type
    {
        ValueField<bool?> foreignerFactField;

        [XmlIgnore]
        public bool? foreignerFact
        {
            get
            {
                if (!foreignerFactField.IsInited)
                    foreignerFactField.Value = Item.GetBool(ItemElementName, "foreignerFact");
                return foreignerFactField.Value;
            }
        }
    }
}
