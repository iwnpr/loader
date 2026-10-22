using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class FL_Event_1_4_Type
    {
        ValueField<FL_19_1_AmountInfo_Type[]> FL_19_1_AmountInfoField;
        ValueField<FL_19_Amount_Type> FL_19_AmountField;
        ValueField<FL_21_PaymentTerms_Type> FL_21_PaymentTermsField;
        ValueField<FL_31_NonMonetarySubject_Type[]> FL_31_NonMonetarySubjectField;

        [XmlIgnore]
        public FL_19_1_AmountInfo_Type[] FL_19_1_AmountInfo
        {
            get
            {
                if (!FL_19_1_AmountInfoField.IsInited)
                    FL_19_1_AmountInfoField.Value = Items.GetObjects<FL_19_1_AmountInfo_Type>();
                return FL_19_1_AmountInfoField.Value;
            }
        }

        [XmlIgnore]
        public FL_19_Amount_Type FL_19_Amount
        {
            get
            {
                if (!FL_19_AmountField.IsInited)
                    FL_19_AmountField.Value = Items.GetObject<FL_19_Amount_Type>();
                return FL_19_AmountField.Value;
            }
        }

        [XmlIgnore]
        public FL_21_PaymentTerms_Type FL_21_PaymentTerms
        {
            get
            {
                if (!FL_21_PaymentTermsField.IsInited)
                    FL_21_PaymentTermsField.Value = Items.GetObject<FL_21_PaymentTerms_Type>();
                return FL_21_PaymentTermsField.Value;
            }
        }

        [XmlIgnore]
        public FL_31_NonMonetarySubject_Type[] FL_31_NonMonetarySubject
        {
            get
            {
                if (!FL_31_NonMonetarySubjectField.IsInited)
                    FL_31_NonMonetarySubjectField.Value = Items.GetObjects<FL_31_NonMonetarySubject_Type>();
                return FL_31_NonMonetarySubjectField.Value;
            }
        }
    }
}
