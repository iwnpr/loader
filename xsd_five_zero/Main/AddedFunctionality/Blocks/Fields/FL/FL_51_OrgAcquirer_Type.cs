using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_51_OrgAcquirer_Type
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
