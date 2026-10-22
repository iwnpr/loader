using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_32_SourceLiquidation_Type
    {
        ValueField<bool?> ownerChangeExistField;
        ValueField<bool?> obligationEndExistField;
        ValueField<ushort?> obligationEndCodeField;

        [XmlIgnore]
        public bool? ownerChangeExist
        {
            get
            {
                if (!ownerChangeExistField.IsInited)
                    ownerChangeExistField.Value = Item.GetBool(ItemElementName, "ownerChangeExist");
                return ownerChangeExistField.Value;
            }
        }

        [XmlIgnore]
        public bool? obligationEndExist
        {
            get
            {
                if (!obligationEndExistField.IsInited)
                    obligationEndExistField.Value = Items.GetBool(ItemsElementName, "obligationEndExist");
                return obligationEndExistField.Value;
            }
        }

        [XmlIgnore]
        public ushort? obligationEndCode
        {
            get
            {
                if (!obligationEndCodeField.IsInited)
                    obligationEndCodeField.Value = Items.GetUshort(ItemsElementName, "obligationEndCode");
                return obligationEndCodeField.Value;
            }
        }
    }
}
