using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_46_Participation_Type
    {
        ValueField<bool?> overdueExistField;

        [XmlIgnore]
        public bool? overdueExist
        {
            get
            {
                if (!overdueExistField.IsInited)
                    overdueExistField.Value = Item.GetBool(ItemElementName, "overdueExist");
                return overdueExistField.Value;
            }
        }
    }
}
