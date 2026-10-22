using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class FL_31_NonMonetarySubject_Type
    {
        ValueField<bool?> fulfillmentFailExistField;

        [XmlIgnore]
        public bool? fulfillmentFailExist
        {
            get
            {
                if (!fulfillmentFailExistField.IsInited)
                    fulfillmentFailExistField.Value = Item.GetBool(ItemElementName, "fulfillmentFailExist");
                return fulfillmentFailExistField.Value;
            }
        }
    }
}
