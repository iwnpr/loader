using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_41_1_PrevOrgAcquirer_Type
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
