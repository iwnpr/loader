using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_Event_3_1_Type
    {
        ValueField<FL_Event_3_1_TypeFL_32_35_Group[]> FL_32_35_GroupField;
        ValueField<FL_Event_3_1_TypeFL_33_Warranty[]> FL_33_WarrantyField;
        ValueField<FL_Event_3_1_TypeFL_34_Guarantee[]> FL_34_GuaranteeField;
        ValueField<FL_Event_3_1_TypeFL_36_1_ProvisionPaymentOffset[]> FL_36_1_ProvisionPaymentOffsetField;
        ValueField<FL_Event_3_1_TypeFL_36_ProvisionPayment[]> FL_36_ProvisionPaymentField;
        ValueField<FL_Event_3_1_TypeFL_37_WarrantyReturn[]> FL_37_WarrantyReturnField;
        ValueField<FL_Event_3_1_TypeFL_39_Court[]> FL_39_CourtField;
        ValueField<FL_Event_3_1_TypeFL_41_SourceBankruptcy[]> FL_41_SourceBankruptcyField;
        ValueField<FL_Event_3_1_TypeFL_42_SourceLiquidation[]> FL_42_SourceLiquidationField;
        ValueField<FL_Event_3_1_TypeFL_45_StopSend[]> FL_45_StopSendField;
        ValueField<FL_Event_3_1_TypeFL_51_OrgAcquirer[]> FL_51_OrgAcquirerField;
        ValueField<FL_Event_3_1_TypeFL_51_1_PrevOrgAcquirer[]> FL_51_1_PrevOrgAcquirerField;
        ValueField<FL_Event_3_1_TypeFL_52_PersonAcquirer[]> FL_52_PersonAcquirerField;
        ValueField<FL_Event_3_1_TypeFL_52_1_PrevPersonAcquirer[]> FL_52_1_PrevPersonAcquirerField;
        ValueField<FL_Event_3_1_TypeFL_53_ServiceOrg[]> FL_53_ServiceOrgField;
        ValueField<FL_Event_3_1_TypeFL_54_Accounting[]> FL_54_AccountingField;
        ValueField<FL_Event_3_1_TypeFL_10_Contact[]> FL_10_ContactField;
        ValueField<FL_Event_3_1_TypeFL_11_IndividualEntrepreneur[]> FL_11_IndividualEntrepreneurField;
        ValueField<FL_Event_3_1_TypeFL_12_Capacity[]> FL_12_CapacityField;
        ValueField<FL_Event_3_1_TypeFL_13_Bankruptcy[]> FL_13_BankruptcyField;
        ValueField<FL_Event_3_1_TypeFL_14_BankruptcyEnd[]> FL_14_BankruptcyEndField;
        ValueField<FL_Event_3_1_TypeFL_1_4_Group[]> FL_1_4_GroupField;
        ValueField<FL_Event_3_1_TypeFL_29_1_DebtBurdenInfo[]> FL_29_1_DebtBurdenInfoField;
        ValueField<FL_Event_3_1_TypeFL_2_5_Group[]> FL_2_5_GroupField;
        ValueField<FL_Event_3_1_TypeFL_3_Birth[]> FL_3_BirthField;
        ValueField<FL_Event_3_1_TypeFL_43_Collection[]> FL_43_CollectionField;
        ValueField<FL_Event_3_1_TypeFL_55_57_Group[]> FL_55_57_GroupField;
        ValueField<FL_Event_3_1_TypeFL_56_Participation[]> FL_56_ParticipationField;
        ValueField<FL_Event_3_1_TypeFL_6_Tax[]> FL_6_TaxField;
        ValueField<FL_Event_3_1_TypeFL_7_Social[]> FL_7_SocialField;
        ValueField<FL_Event_3_1_TypeFL_8_AddrReg[]> FL_8_AddrRegField;
        ValueField<FL_Event_3_1_TypeFL_9_AddrFact[]> FL_9_AddrFactField;

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_32_35_Group[] FL_32_35_Group
        {
            get
            {
                if (!FL_32_35_GroupField.IsInited)
                    FL_32_35_GroupField.Value = Items.GetObjects<FL_Event_3_1_TypeFL_32_35_Group>();
                return FL_32_35_GroupField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_33_Warranty[] FL_33_Warranty
        {
            get
            {
                if (!FL_33_WarrantyField.IsInited)
                    FL_33_WarrantyField.Value = Items.GetObjects<FL_Event_3_1_TypeFL_33_Warranty>();
                return FL_33_WarrantyField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_34_Guarantee[] FL_34_Guarantee
        {
            get
            {
                if (!FL_34_GuaranteeField.IsInited)
                    FL_34_GuaranteeField.Value = Items.GetObjects<FL_Event_3_1_TypeFL_34_Guarantee>();
                return FL_34_GuaranteeField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_36_1_ProvisionPaymentOffset[] FL_36_1_ProvisionPaymentOffset
        {
            get
            {
                if (!FL_36_1_ProvisionPaymentOffsetField.IsInited)
                    FL_36_1_ProvisionPaymentOffsetField.Value = Items.GetObjects<FL_Event_3_1_TypeFL_36_1_ProvisionPaymentOffset>();
                return FL_36_1_ProvisionPaymentOffsetField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_36_ProvisionPayment[] FL_36_ProvisionPayment
        {
            get
            {
                if (!FL_36_ProvisionPaymentField.IsInited)
                    FL_36_ProvisionPaymentField.Value = Items.GetObjects<FL_Event_3_1_TypeFL_36_ProvisionPayment>();
                return FL_36_ProvisionPaymentField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_37_WarrantyReturn[] FL_37_WarrantyReturn
        {
            get
            {
                if (!FL_37_WarrantyReturnField.IsInited)
                    FL_37_WarrantyReturnField.Value = Items.GetObjects<FL_Event_3_1_TypeFL_37_WarrantyReturn>();
                return FL_37_WarrantyReturnField.Value;

            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_39_Court[] FL_39_Court
        {
            get
            {
                if (!FL_39_CourtField.IsInited)
                    FL_39_CourtField.Value = Items.GetObjects<FL_Event_3_1_TypeFL_39_Court>();
                return FL_39_CourtField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_41_SourceBankruptcy[] FL_41_SourceBankruptcy
        {
            get
            {
                if (!FL_41_SourceBankruptcyField.IsInited)
                    FL_41_SourceBankruptcyField.Value = Items.GetObjects<FL_Event_3_1_TypeFL_41_SourceBankruptcy>();
                return FL_41_SourceBankruptcyField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_42_SourceLiquidation[] FL_42_SourceLiquidation
        {
            get
            {
                if (!FL_42_SourceLiquidationField.IsInited)
                    FL_42_SourceLiquidationField.Value = Items.GetObjects<FL_Event_3_1_TypeFL_42_SourceLiquidation>();
                return FL_42_SourceLiquidationField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_45_StopSend[] FL_45_StopSend
        {
            get
            {
                if (!FL_45_StopSendField.IsInited)
                    FL_45_StopSendField.Value = Items.GetObjects<FL_Event_3_1_TypeFL_45_StopSend>();
                return FL_45_StopSendField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_51_OrgAcquirer[] FL_51_OrgAcquirer
        {
            get
            {
                if (!FL_51_OrgAcquirerField.IsInited)
                    FL_51_OrgAcquirerField.Value = Items.GetObjects<FL_Event_3_1_TypeFL_51_OrgAcquirer>();
                return FL_51_OrgAcquirerField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_51_1_PrevOrgAcquirer[] FL_51_1_PrevOrgAcquirer
        {
            get
            {
                if (!FL_51_1_PrevOrgAcquirerField.IsInited)
                    FL_51_1_PrevOrgAcquirerField.Value = Items.GetObjects<FL_Event_3_1_TypeFL_51_1_PrevOrgAcquirer>();
                return FL_51_1_PrevOrgAcquirerField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_52_PersonAcquirer[] FL_52_PersonAcquirer
        {
            get
            {
                if (!FL_52_PersonAcquirerField.IsInited)
                    FL_52_PersonAcquirerField.Value = Items.GetObjects<FL_Event_3_1_TypeFL_52_PersonAcquirer>();
                return FL_52_PersonAcquirerField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_52_1_PrevPersonAcquirer[] FL_52_1_PrevPersonAcquirer
        {
            get
            {
                if (!FL_52_1_PrevPersonAcquirerField.IsInited)
                    FL_52_1_PrevPersonAcquirerField.Value = Items.GetObjects<FL_Event_3_1_TypeFL_52_1_PrevPersonAcquirer>();
                return FL_52_1_PrevPersonAcquirerField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_53_ServiceOrg[] FL_53_ServiceOrg
        {
            get
            {
                if (!FL_53_ServiceOrgField.IsInited)
                    FL_53_ServiceOrgField.Value = Items.GetObjects<FL_Event_3_1_TypeFL_53_ServiceOrg>();
                return FL_53_ServiceOrgField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_54_Accounting[] FL_54_Accounting
        {
            get
            {
                if (!FL_54_AccountingField.IsInited)
                    FL_54_AccountingField.Value = Items.GetObjects<FL_Event_3_1_TypeFL_54_Accounting>();
                return FL_54_AccountingField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_10_Contact[] FL_10_Contact
        {
            get
            {
                if (!FL_10_ContactField.IsInited)
                    FL_10_ContactField.Value = Items1.GetObjects<FL_Event_3_1_TypeFL_10_Contact>();
                return FL_10_ContactField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_11_IndividualEntrepreneur[] FL_11_IndividualEntrepreneur
        {
            get
            {
                if (!FL_11_IndividualEntrepreneurField.IsInited)
                    FL_11_IndividualEntrepreneurField.Value = Items1.GetObjects<FL_Event_3_1_TypeFL_11_IndividualEntrepreneur>();
                return FL_11_IndividualEntrepreneurField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_12_Capacity[] FL_12_Capacity
        {
            get
            {
                if (!FL_12_CapacityField.IsInited)
                    FL_12_CapacityField.Value = Items1.GetObjects<FL_Event_3_1_TypeFL_12_Capacity>();
                return FL_12_CapacityField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_13_Bankruptcy[] FL_13_Bankruptcy
        {
            get
            {
                if (!FL_13_BankruptcyField.IsInited)
                    FL_13_BankruptcyField.Value = Items1.GetObjects<FL_Event_3_1_TypeFL_13_Bankruptcy>();
                return FL_13_BankruptcyField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_14_BankruptcyEnd[] FL_14_BankruptcyEnd
        {
            get
            {
                if (!FL_14_BankruptcyEndField.IsInited)
                    FL_14_BankruptcyEndField.Value = Items1.GetObjects<FL_Event_3_1_TypeFL_14_BankruptcyEnd>();
                return FL_14_BankruptcyEndField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_1_4_Group[] FL_1_4_Group
        {
            get
            {
                if (!FL_1_4_GroupField.IsInited)
                    FL_1_4_GroupField.Value = Items1.GetObjects<FL_Event_3_1_TypeFL_1_4_Group>();
                return FL_1_4_GroupField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_29_1_DebtBurdenInfo[] FL_29_1_DebtBurdenInfo
        {
            get
            {
                if (!FL_29_1_DebtBurdenInfoField.IsInited)
                    FL_29_1_DebtBurdenInfoField.Value = Items1.GetObjects<FL_Event_3_1_TypeFL_29_1_DebtBurdenInfo>();
                return FL_29_1_DebtBurdenInfoField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_2_5_Group[] FL_2_5_Group
        {
            get
            {
                if (!FL_2_5_GroupField.IsInited)
                    FL_2_5_GroupField.Value = Items1.GetObjects<FL_Event_3_1_TypeFL_2_5_Group>();
                return FL_2_5_GroupField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_3_Birth[] FL_3_Birth
        {
            get
            {
                if (!FL_3_BirthField.IsInited)
                    FL_3_BirthField.Value = Items1.GetObjects<FL_Event_3_1_TypeFL_3_Birth>();
                return FL_3_BirthField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_43_Collection[] FL_43_Collection
        {
            get
            {
                if (!FL_43_CollectionField.IsInited)
                    FL_43_CollectionField.Value = Items1.GetObjects<FL_Event_3_1_TypeFL_43_Collection>();
                return FL_43_CollectionField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_55_57_Group[] FL_55_57_Group
        {
            get
            {
                if (!FL_55_57_GroupField.IsInited)
                    FL_55_57_GroupField.Value = Items1.GetObjects<FL_Event_3_1_TypeFL_55_57_Group>();
                return FL_55_57_GroupField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_56_Participation[] FL_56_Participation
        {
            get
            {
                if (!FL_56_ParticipationField.IsInited)
                    FL_56_ParticipationField.Value = Items1.GetObjects<FL_Event_3_1_TypeFL_56_Participation>();
                return FL_56_ParticipationField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_6_Tax[] FL_6_Tax
        {
            get
            {
                if (!FL_6_TaxField.IsInited)
                    FL_6_TaxField.Value = Items1.GetObjects<FL_Event_3_1_TypeFL_6_Tax>();
                return FL_6_TaxField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_7_Social[] FL_7_Social
        {
            get
            {
                if (!FL_7_SocialField.IsInited)
                    FL_7_SocialField.Value = Items1.GetObjects<FL_Event_3_1_TypeFL_7_Social>();
                return FL_7_SocialField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_8_AddrReg[] FL_8_AddrReg
        {
            get
            {
                if (!FL_8_AddrRegField.IsInited)
                    FL_8_AddrRegField.Value = Items1.GetObjects<FL_Event_3_1_TypeFL_8_AddrReg>();
                return FL_8_AddrRegField.Value;
            }
        }

        [XmlIgnore]
        public FL_Event_3_1_TypeFL_9_AddrFact[] FL_9_AddrFact
        {
            get
            {
                if (!FL_9_AddrFactField.IsInited)
                    FL_9_AddrFactField.Value = Items1.GetObjects<FL_Event_3_1_TypeFL_9_AddrFact>();
                return FL_9_AddrFactField.Value;
            }
        }
    }
}
