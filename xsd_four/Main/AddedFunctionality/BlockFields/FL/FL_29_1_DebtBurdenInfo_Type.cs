using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class FL_29_1_DebtBurdenInfo_Type
    {
        ValueField<bool?> loadFactField;
        ValueField<bool?> loadCalculationFactField;

        [XmlIgnore]
        public bool? loadFact
        {
            get
            {
                if (!loadFactField.IsInited)
                    loadFactField.Value = Item.GetBool(ItemElementName, "loadFact");
                return loadFactField.Value;
            }
        }

        [XmlIgnore]
        public bool? loadCalculationFact
        {
            get
            {
                if (!loadCalculationFactField.IsInited)
                    loadCalculationFactField.Value = Item1.GetBool(Item1ElementName, "loadCalculationFact");
                return loadCalculationFactField.Value;
            }
        }
    }
}
