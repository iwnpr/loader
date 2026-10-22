using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_23_26_Group_TypeUL_23_Collateral
    {
        ValueField<bool?> acquiredExistField;
        ValueField<bool?> inhabitedExistField;

        [XmlIgnore]
        public bool? acquiredExist
        {
            get
            {
                if (!acquiredExistField.IsInited)
                    acquiredExistField.Value = Item.GetBool(ItemElementName, "acquiredExist");
                return acquiredExistField.Value;
            }
        }

        [XmlIgnore]
        public bool? inhabitedExist
        {
            get
            {
                if (!inhabitedExistField.IsInited)
                    inhabitedExistField.Value = Item1.GetBool(Item1ElementName, "inhabitedExist");
                return inhabitedExistField.Value;
            }
        }
    }
}
