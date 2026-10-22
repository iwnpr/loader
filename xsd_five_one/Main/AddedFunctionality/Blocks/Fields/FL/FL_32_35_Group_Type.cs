using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_32_35_Group_Type
    {
        ValueField<FL_32_35_Group_TypeFL_32_Collateral?> FL_32_CollateralField;
        ValueField<FL_32_35_Group_TypeFL_35_Insurance[]> FL_35_InsuranceField;
        ValueField<bool?> assetKindField;
        ValueField<string?> propertyIdField;

        [XmlIgnore]
        public FL_32_35_Group_TypeFL_32_Collateral? FL_32_Collateral
        {
            get
            {
                if (!FL_32_CollateralField.IsInited)
                    FL_32_CollateralField.Value = (FL_32_35_Group_TypeFL_32_Collateral?)Items.GetObject(ItemsElementName, "FL_32_Collateral");
                return FL_32_CollateralField.Value;
            }
        }

        [XmlIgnore]
        public FL_32_35_Group_TypeFL_35_Insurance[] FL_35_Insurance
        {
            get
            {
                if (!FL_35_InsuranceField.IsInited)
                    FL_35_InsuranceField.Value = Items.GetObjects(ItemsElementName, "FL_35_Insurance").Select(x => (FL_32_35_Group_TypeFL_35_Insurance)x).ToArray();
                return FL_35_InsuranceField.Value;
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