using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_23_26_Group_Type
    {
        ValueField<UL_23_26_Group_TypeUL_23_Collateral?> UL_23_CollateralField;
        ValueField<UL_23_26_Group_TypeUL_26_Insurance[]> UL_26_InsuranceField;
        ValueField<bool?> assetKindField;
        ValueField<string?> propertyIdField;

        [XmlIgnore]
        public UL_23_26_Group_TypeUL_23_Collateral? UL_23_Collateral
        {
            get
            {
                if (!UL_23_CollateralField.IsInited)
                    UL_23_CollateralField.Value = (UL_23_26_Group_TypeUL_23_Collateral?)Items.GetObject(ItemsElementName, "UL_23_Collateral");
                return UL_23_CollateralField.Value;
            }
        }

        [XmlIgnore]
        public UL_23_26_Group_TypeUL_26_Insurance[] UL_26_Insurance
        {
            get
            {
                if (!UL_26_InsuranceField.IsInited)
                    UL_26_InsuranceField.Value = Items.GetObjects(ItemsElementName, "UL_26_Insurance").Select(x => (UL_23_26_Group_TypeUL_26_Insurance)x).ToArray();
                return UL_26_InsuranceField.Value;
            }
        }

        [XmlIgnore]
        public bool? assetKind
        {
            get
            {
                if (!assetKindField.IsInited)
                    assetKindField.Value = Items.GetBool(ItemsElementName, "assetKind");
                return assetKindField.Value;
            }
        }

        [XmlIgnore]
        public string? propertyId
        {
            get
            {
                if (!propertyIdField.IsInited)
                    propertyIdField.Value = Items.GetString(ItemsElementName, "propertyId");
                return propertyIdField.Value;
            }
        }
    }
}