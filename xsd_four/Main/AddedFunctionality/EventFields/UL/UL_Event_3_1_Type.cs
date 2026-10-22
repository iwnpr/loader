using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class UL_Event_3_1_Type
    {
        ValueField<UL_Event_3_1_TypeUL_23_26_Group[]> UL_23_26_GroupField;
        ValueField<UL_Event_3_1_TypeUL_24_Warranty[]> UL_24_WarrantyField;
        ValueField<UL_Event_3_1_TypeUL_25_Guarantee[]> UL_25_GuaranteeField;
        ValueField<UL_Event_3_1_TypeUL_27_ProvisionPayment[]> UL_27_ProvisionPaymentField;
        ValueField<UL_Event_3_1_TypeUL_27_1_ProvisionPaymentOffset[]> UL_27_ProvisionPaymentOffsetField;
        ValueField<UL_Event_3_1_TypeUL_28_WarrantyReturn[]> UL_28_WarrantyReturnField;
        ValueField<UL_Event_3_1_TypeUL_30_Court[]> UL_30_CourtField;
        ValueField<UL_Event_3_1_TypeUL_31_SourceBankruptcy[]> UL_31_SourceBankruptcyField;
        ValueField<UL_Event_3_1_TypeUL_32_SourceLiquidation[]> UL_32_SourceLiquidationField;
        ValueField<UL_Event_3_1_TypeUL_35_StopSend[]> UL_35_StopSendField;
        ValueField<UL_Event_3_1_TypeUL_41_OrgAcquirer[]> UL_41_OrgAcquirerField;
        ValueField<UL_Event_3_1_TypeUL_42_PersonAcquirer[]> UL_42_PersonAcquirerField;
        ValueField<UL_Event_3_1_TypeUL_43_ServiceOrg[]> UL_43_ServiceOrgField;
        ValueField<UL_Event_3_1_TypeUL_44_Accounting[]> UL_44_AccountingField;


        ValueField<UL_Event_3_1_TypeUL_1_Name[]> UL_1_NameField;
        ValueField<UL_Event_3_1_TypeUL_2_Address[]> UL_2_AddressField;
        ValueField<UL_Event_3_1_TypeUL_3_Reg[]> UL_3_RegField;
        ValueField<UL_Event_3_1_TypeUL_4_Tax[]> UL_4_TaxField;
        ValueField<UL_Event_3_1_TypeUL_5_Reorg[]> UL_5_ReorgField;
        ValueField<UL_Event_3_1_TypeUL_6_Bankruptcy[]> UL_6_BankruptcyField;
        ValueField<UL_Event_3_1_TypeUL_7_BankruptcyEnd[]> UL_7_BankruptcyEndField;
        ValueField<UL_Event_3_1_TypeUL_33_Collection[]> UL_33_CollectionField;
        ValueField<UL_Event_3_1_TypeUL_45_47_Group[]> UL_45_47_GroupField;
        ValueField<UL_Event_3_1_TypeUL_46_Participation[]> UL_46_ParticipationField;


        [XmlIgnore]
        public UL_Event_3_1_TypeUL_23_26_Group[] UL_23_26_Group
        {
            get
            {
                if (!UL_23_26_GroupField.IsInited)
                    UL_23_26_GroupField.Value = Items.GetObjects<UL_Event_3_1_TypeUL_23_26_Group>();
                return UL_23_26_GroupField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_24_Warranty[] UL_24_Warranty
        {
            get
            {
                if (!UL_24_WarrantyField.IsInited)
                    UL_24_WarrantyField.Value = Items.GetObjects<UL_Event_3_1_TypeUL_24_Warranty>();
                return UL_24_WarrantyField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_25_Guarantee[] UL_25_Guarantee
        {
            get
            {
                if (!UL_25_GuaranteeField.IsInited)
                    UL_25_GuaranteeField.Value = Items.GetObjects<UL_Event_3_1_TypeUL_25_Guarantee>();
                return UL_25_GuaranteeField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_27_ProvisionPayment[] UL_27_ProvisionPayment
        {
            get
            {
                if (!UL_27_ProvisionPaymentField.IsInited)
                    UL_27_ProvisionPaymentField.Value = Items.GetObjects<UL_Event_3_1_TypeUL_27_ProvisionPayment>();
                return UL_27_ProvisionPaymentField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_27_1_ProvisionPaymentOffset[] UL_27_1_ProvisionPaymentOffset
        {
            get
            {
                if (!UL_27_ProvisionPaymentOffsetField.IsInited)
                    UL_27_ProvisionPaymentOffsetField.Value = Items.GetObjects<UL_Event_3_1_TypeUL_27_1_ProvisionPaymentOffset>();
                return UL_27_ProvisionPaymentOffsetField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_28_WarrantyReturn[] UL_28_WarrantyReturn
        {
            get
            {
                if (!UL_28_WarrantyReturnField.IsInited)
                    UL_28_WarrantyReturnField.Value = Items.GetObjects<UL_Event_3_1_TypeUL_28_WarrantyReturn>();
                return UL_28_WarrantyReturnField.Value;

            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_30_Court[] UL_30_Court
        {
            get
            {
                if (!UL_30_CourtField.IsInited)
                    UL_30_CourtField.Value = Items.GetObjects<UL_Event_3_1_TypeUL_30_Court>();
                return UL_30_CourtField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_31_SourceBankruptcy[] UL_31_SourceBankruptcy
        {
            get
            {
                if (!UL_31_SourceBankruptcyField.IsInited)
                    UL_31_SourceBankruptcyField.Value = Items.GetObjects<UL_Event_3_1_TypeUL_31_SourceBankruptcy>();
                return UL_31_SourceBankruptcyField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_32_SourceLiquidation[] UL_32_SourceLiquidation
        {
            get
            {
                if (!UL_32_SourceLiquidationField.IsInited)
                    UL_32_SourceLiquidationField.Value = Items.GetObjects<UL_Event_3_1_TypeUL_32_SourceLiquidation>();
                return UL_32_SourceLiquidationField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_35_StopSend[] UL_35_StopSend
        {
            get
            {
                if (!UL_35_StopSendField.IsInited)
                    UL_35_StopSendField.Value = Items.GetObjects<UL_Event_3_1_TypeUL_35_StopSend>();
                return UL_35_StopSendField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_41_OrgAcquirer[] UL_41_OrgAcquirer
        {
            get
            {
                if (!UL_41_OrgAcquirerField.IsInited)
                    UL_41_OrgAcquirerField.Value = Items.GetObjects<UL_Event_3_1_TypeUL_41_OrgAcquirer>();
                return UL_41_OrgAcquirerField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_42_PersonAcquirer[] UL_42_PersonAcquirer
        {
            get
            {
                if (!UL_42_PersonAcquirerField.IsInited)
                    UL_42_PersonAcquirerField.Value = Items.GetObjects<UL_Event_3_1_TypeUL_42_PersonAcquirer>();
                return UL_42_PersonAcquirerField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_43_ServiceOrg[] UL_43_ServiceOrg
        {
            get
            {
                if (!UL_43_ServiceOrgField.IsInited)
                    UL_43_ServiceOrgField.Value = Items.GetObjects<UL_Event_3_1_TypeUL_43_ServiceOrg>();
                return UL_43_ServiceOrgField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_44_Accounting[] UL_44_Accounting
        {
            get
            {
                if (!UL_44_AccountingField.IsInited)
                    UL_44_AccountingField.Value = Items.GetObjects<UL_Event_3_1_TypeUL_44_Accounting>();
                return UL_44_AccountingField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_1_Name[] UL_1_Name
        {
            get
            {
                if (!UL_1_NameField.IsInited)
                    UL_1_NameField.Value = Items1.GetObjects<UL_Event_3_1_TypeUL_1_Name>();
                return UL_1_NameField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_2_Address[] UL_2_Address
        {
            get
            {
                if (!UL_2_AddressField.IsInited)
                    UL_2_AddressField.Value = Items1.GetObjects<UL_Event_3_1_TypeUL_2_Address>();
                return UL_2_AddressField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_3_Reg[] UL_3_Reg
        {
            get
            {
                if (!UL_3_RegField.IsInited)
                    UL_3_RegField.Value = Items1.GetObjects<UL_Event_3_1_TypeUL_3_Reg>();
                return UL_3_RegField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_4_Tax[] UL_4_Tax
        {
            get
            {
                if (!UL_4_TaxField.IsInited)
                    UL_4_TaxField.Value = Items1.GetObjects<UL_Event_3_1_TypeUL_4_Tax>();
                return UL_4_TaxField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_5_Reorg[] UL_5_Reorg
        {
            get
            {
                if (!UL_5_ReorgField.IsInited)
                    UL_5_ReorgField.Value = Items1.GetObjects<UL_Event_3_1_TypeUL_5_Reorg>();
                return UL_5_ReorgField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_6_Bankruptcy[] UL_6_Bankruptcy
        {
            get
            {
                if (!UL_6_BankruptcyField.IsInited)
                    UL_6_BankruptcyField.Value = Items1.GetObjects<UL_Event_3_1_TypeUL_6_Bankruptcy>();
                return UL_6_BankruptcyField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_7_BankruptcyEnd[] UL_7_BankruptcyEnd
        {
            get
            {
                if (!UL_7_BankruptcyEndField.IsInited)
                    UL_7_BankruptcyEndField.Value = Items1.GetObjects<UL_Event_3_1_TypeUL_7_BankruptcyEnd>();
                return UL_7_BankruptcyEndField.Value;
            }
        }



        [XmlIgnore]
        public UL_Event_3_1_TypeUL_33_Collection[] UL_33_Collection
        {
            get
            {
                if (!UL_33_CollectionField.IsInited)
                    UL_33_CollectionField.Value = Items1.GetObjects<UL_Event_3_1_TypeUL_33_Collection>();
                return UL_33_CollectionField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_45_47_Group[] UL_45_47_Group
        {
            get
            {
                if (!UL_45_47_GroupField.IsInited)
                    UL_45_47_GroupField.Value = Items1.GetObjects<UL_Event_3_1_TypeUL_45_47_Group>();
                return UL_45_47_GroupField.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_3_1_TypeUL_46_Participation[] UL_46_Participation
        {
            get
            {
                if (!UL_46_ParticipationField.IsInited)
                    UL_46_ParticipationField.Value = Items1.GetObjects<UL_Event_3_1_TypeUL_46_Participation>();
                return UL_46_ParticipationField.Value;
            }
        }
    }
}
