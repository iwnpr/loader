using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_32_35_Group_TypeFL_32_Collateral
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
