using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_12_Capacity_Type
    {
        ValueField<FL_12_Capacity_TypeIncapacity_group_FL_12_Capacity?> Incapacity_group_FL_12_CapacityField;
        ValueField<FL_12_Capacity_TypeLegalCapacity_group_FL_12_Capacity?> LegalCapacity_group_FL_12_CapacityField;

        [XmlIgnore]
        public FL_12_Capacity_TypeIncapacity_group_FL_12_Capacity? Incapacity_group_FL_12_Capacity
        {
            get
            {
                if (!Incapacity_group_FL_12_CapacityField.IsInited)
                    Incapacity_group_FL_12_CapacityField.Value = Item.GetObject<FL_12_Capacity_TypeIncapacity_group_FL_12_Capacity>();
                return Incapacity_group_FL_12_CapacityField.Value;
            }
        }

        [XmlIgnore]
        public FL_12_Capacity_TypeLegalCapacity_group_FL_12_Capacity? LegalCapacity_group_FL_12_Capacity
        {
            get
            {
                if (!LegalCapacity_group_FL_12_CapacityField.IsInited)
                    LegalCapacity_group_FL_12_CapacityField.Value = Item.GetObject<FL_12_Capacity_TypeLegalCapacity_group_FL_12_Capacity>();
                return LegalCapacity_group_FL_12_CapacityField.Value;
            }
        }
    }
}