using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class UL_33_Collection_Type
    {
        private bool itemFieldSpecified;

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ItemSpecified
        {
            get
            {
                return this.itemFieldSpecified;
            }
            set
            {
                this.itemFieldSpecified = value;
            }
        }

        ValueField<decimal?> onceSumField;
        ValueField<decimal?> periodSumField;

        [XmlIgnore]
        public decimal? onceSum
        {
            get
            {
                if (!onceSumField.IsInited)
                    onceSumField.Value = Item.GetDecimal(ItemElementName, "onceSum");
                return onceSumField.Value;
            }
        }

        [XmlIgnore]
        public decimal? periodSum
        {
            get
            {
                if (!periodSumField.IsInited)
                    periodSumField.Value = Item.GetDecimal(ItemElementName, "periodSum");
                return periodSumField.Value;
            }
        }
    }
}
