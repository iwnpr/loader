using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class FL_20_JointDebtors_Type
    {
        ValueField<bool?> existField;
        ValueField<ushort?> numField;

        [XmlIgnore]
        public bool? exist
        {
            get
            {
                if (!existField.IsInited)
                    existField.Value = Items.GetBool(ItemsElementName, "exist");
                return existField.Value;
            }
        }

        [XmlIgnore]
        public ushort? num
        {
            get
            {
                if (!numField.IsInited)
                    numField.Value = Items.GetUshort(ItemsElementName, "num");
                return numField.Value;
            }
        }
    }
}