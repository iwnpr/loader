using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_25_26_27_28_Group_TypeFL_25_Debt
    {
        ValueField<bool?> graceUnconfExistField;

        [XmlIgnore]
        public bool? graceUnconfExist
        {
            get
            {
                if (!graceUnconfExistField.IsInited)
                    graceUnconfExistField.Value = Item.GetBool(ItemElementName, "graceUnconfExist");
                return graceUnconfExistField.Value;
            }
        }
    }
}
