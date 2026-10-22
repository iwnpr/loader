using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class UL_43_ServiceOrg_Type
    {
        ValueField<bool?> regRusExistField;

        [XmlIgnore]
        public bool? regRusExist
        {
            get
            {
                if (!regRusExistField.IsInited)
                    regRusExistField.Value = Item.GetBool(ItemElementName, "regRusExist");
                return regRusExistField.Value;
            }
        }
    }
}
