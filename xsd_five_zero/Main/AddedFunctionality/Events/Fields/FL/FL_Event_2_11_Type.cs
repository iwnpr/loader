using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_Event_2_11_Type
    {
        ValueField<FL_51_OrgAcquirer_Type> FL_51_OrgAcquirerField;
        ValueField<FL_52_PersonAcquirer_Type> FL_52_PersonAcquirerField;

        [XmlIgnore]
        public FL_51_OrgAcquirer_Type FL_51_OrgAcquirer
        {
            get
            {
                if (!FL_51_OrgAcquirerField.IsInited)
                    FL_51_OrgAcquirerField.Value = Item.GetObject<FL_51_OrgAcquirer_Type>();
                return FL_51_OrgAcquirerField.Value;
            }
        }

        [XmlIgnore]
        public FL_52_PersonAcquirer_Type FL_52_PersonAcquirer
        {
            get
            {
                if (!FL_52_PersonAcquirerField.IsInited)
                    FL_52_PersonAcquirerField.Value = Item.GetObject<FL_52_PersonAcquirer_Type>();
                return FL_52_PersonAcquirerField.Value;
            }
        }
    }
}
