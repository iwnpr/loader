using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class FL_Event_2_11_1_Type
    {
        ValueField<FL_19_1_AmountInfo_Type[]> FL_19_1_AmountInfoField;
        ValueField<FL_19_Amount_Type> FL_19_AmountField;
        ValueField<FL_21_PaymentTerms_Type> FL_21_PaymentTermsField;
        ValueField<FL_22_TotalCost_Type> FL_22_TotalCostField;
        ValueField<FL_25_26_27_28_Group_Type> FL_25_26_27_28_GroupField;
        ValueField<FL_29_1_DebtBurdenInfo_Type> FL_29_1_DebtBurdenInfoField;
        ValueField<FL_29_MonthlyPayment_Type> FL_29_MonthlyPaymentField;
        ValueField<FL_31_NonMonetarySubject_Type[]> FL_31_NonMonetarySubjectField;
        ValueField<FL_54_Accounting_Type> FL_54_AccountingField;
        ValueField<FL_51_OrgAcquirer_Type> FL_51_OrgAcquirerField;
        ValueField<FL_52_PersonAcquirer_Type> FL_52_PersonAcquirerField;

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
        public FL_22_TotalCost_Type FL_22_TotalCost
        {
            get
            {
                if (!FL_22_TotalCostField.IsInited)
                    FL_22_TotalCostField.Value = Items.GetObject<FL_22_TotalCost_Type>();
                return FL_22_TotalCostField.Value;
            }
        }

        [XmlIgnore]
        public FL_25_26_27_28_Group_Type FL_25_26_27_28_Group
        {
            get
            {
                if (!FL_25_26_27_28_GroupField.IsInited)
                    FL_25_26_27_28_GroupField.Value = Items.GetObject<FL_25_26_27_28_Group_Type>();
                return FL_25_26_27_28_GroupField.Value;
            }
        }

        [XmlIgnore]
        public FL_29_1_DebtBurdenInfo_Type FL_29_1_DebtBurdenInfo
        {
            get
            {
                if (!FL_29_1_DebtBurdenInfoField.IsInited)
                    FL_29_1_DebtBurdenInfoField.Value = Items.GetObject<FL_29_1_DebtBurdenInfo_Type>();
                return FL_29_1_DebtBurdenInfoField.Value;
            }
        }

        [XmlIgnore]
        public FL_29_MonthlyPayment_Type FL_29_MonthlyPayment
        {
            get
            {
                if (!FL_29_MonthlyPaymentField.IsInited)
                    FL_29_MonthlyPaymentField.Value = Items.GetObject<FL_29_MonthlyPayment_Type>();
                return FL_29_MonthlyPaymentField.Value;
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

        [XmlIgnore]
        public FL_54_Accounting_Type FL_54_Accounting
        {
            get
            {
                if (!FL_54_AccountingField.IsInited)
                    FL_54_AccountingField.Value = Items.GetObject<FL_54_Accounting_Type>();
                return FL_54_AccountingField.Value;
            }
        }

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
