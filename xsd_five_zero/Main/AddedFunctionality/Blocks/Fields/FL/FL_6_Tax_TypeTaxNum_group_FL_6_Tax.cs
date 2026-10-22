using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_6_Tax_TypeTaxNum_group_FL_6_Tax
    {
        ValueField<bool?> innCheckedField;

        [XmlIgnore]
        public bool? innChecked
        {
            get
            {
                if (!innCheckedField.IsInited)
                    innCheckedField.Value = Item.GetBool(ItemElementName, "innChecked");
                return innCheckedField.Value;
            }
        }
    }
}
