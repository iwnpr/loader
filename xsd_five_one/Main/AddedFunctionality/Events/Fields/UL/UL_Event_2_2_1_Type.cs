using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_Event_2_2_1_Type
    {
        ValueField<UL_12_1_AmountInfo_Type[]> UL_12_1_AmountInfoField;
        ValueField<UL_12_Amount_Type> UL_12_AmountField;
        ValueField<UL_14_PaymentTerms_Type> UL_14_PaymentTermsField;
        ValueField<UL_16_Fund_Type> UL_16_FundField;
        ValueField<UL_22_NonMonetarySubject_Type[]> UL_22_NonMonetarySubjectField;

        [XmlIgnore]
        public UL_12_1_AmountInfo_Type[] UL_12_1_AmountInfo
        {
            get
            {
                if (!UL_12_1_AmountInfoField.IsInited)
                    UL_12_1_AmountInfoField.Value = Items.GetObjects<UL_12_1_AmountInfo_Type>();
                return UL_12_1_AmountInfoField.Value;
            }
        }

        [XmlIgnore]
        public UL_12_Amount_Type UL_12_Amount
        {
            get
            {
                if (!UL_12_AmountField.IsInited)
                    UL_12_AmountField.Value = Items.GetObject<UL_12_Amount_Type>();
                return UL_12_AmountField.Value;
            }
        }

        [XmlIgnore]
        public UL_14_PaymentTerms_Type UL_14_PaymentTerms
        {
            get
            {
                if (!UL_14_PaymentTermsField.IsInited)
                    UL_14_PaymentTermsField.Value = Items.GetObject<UL_14_PaymentTerms_Type>();
                return UL_14_PaymentTermsField.Value;
            }
        }

        [XmlIgnore]
        public UL_16_Fund_Type UL_16_Fund
        {
            get
            {
                if (!UL_16_FundField.IsInited)
                    UL_16_FundField.Value = Items.GetObject<UL_16_Fund_Type>();
                return UL_16_FundField.Value;
            }
        }

        [XmlIgnore]
        public UL_22_NonMonetarySubject_Type[] UL_22_NonMonetarySubject
        {
            get
            {
                if (!UL_22_NonMonetarySubjectField.IsInited)
                    UL_22_NonMonetarySubjectField.Value = Items.GetObjects<UL_22_NonMonetarySubject_Type>();
                return UL_22_NonMonetarySubjectField.Value;
            }
        }
    }
}
