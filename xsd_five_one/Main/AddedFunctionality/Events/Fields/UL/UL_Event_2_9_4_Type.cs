using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_Event_2_9_4_Type
    {
        ValueField<UL_41_OrgAcquirer_Type> UL_41_OrgAcquirerField;
        ValueField<UL_42_PersonAcquirer_Type> UL_42_PersonAcquirerField;

        [XmlIgnore]
        public UL_41_OrgAcquirer_Type UL_41_OrgAcquirer
        {
            get
            {
                if (!UL_41_OrgAcquirerField.IsInited)
                    UL_41_OrgAcquirerField.Value = Item.GetObject<UL_41_OrgAcquirer_Type>();
                return UL_41_OrgAcquirerField.Value;
            }
        }

        [XmlIgnore]
        public UL_42_PersonAcquirer_Type UL_42_PersonAcquirer
        {
            get
            {
                if (!UL_42_PersonAcquirerField.IsInited)
                    UL_42_PersonAcquirerField.Value = Item.GetObject<UL_42_PersonAcquirer_Type>();
                return UL_42_PersonAcquirerField.Value;
            }
        }
    }
}
