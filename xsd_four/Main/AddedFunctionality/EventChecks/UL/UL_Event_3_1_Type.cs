using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_Event_3_1_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataUL title)
        {
            var currentBlockCashe = title.CurrentBlockCache;
            if (title.SkipTitleChecksInChangeEvents)
            {
                title = null;
            }

            var errorList = BaseCheck(source, title);

            errorList.AddRange(UL_10_DealUid.Validate(this, source));
            errorList.AddRange(CheckEquality(UL_23_26_Group, refBlockNameType.UL_23_26_Group, currentBlockCashe));
            foreach (var group in UL_23_26_Group.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_23_26_Group_new.Validate(this, UL_10_DealUid));
            }

            errorList.AddRange(CheckEquality(UL_24_Warranty, refBlockNameType.UL_24_Warranty, currentBlockCashe));
            foreach (var group in UL_24_Warranty.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_24_Warranty_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(UL_25_Guarantee, refBlockNameType.UL_25_Guarantee, currentBlockCashe));
            foreach (var group in UL_25_Guarantee.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_25_Guarantee_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(UL_27_ProvisionPayment, refBlockNameType.UL_27_ProvisionPayment, currentBlockCashe));
            foreach (var group in UL_27_ProvisionPayment.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_27_ProvisionPayment_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(UL_27_1_ProvisionPaymentOffset, refBlockNameType.UL_27_1_ProvisionPaymentOffset, currentBlockCashe));
            foreach (var group in UL_27_1_ProvisionPaymentOffset.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_27_1_ProvisionPaymentOffset_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(UL_28_WarrantyReturn, refBlockNameType.UL_28_WarrantyReturn, currentBlockCashe));
            foreach (var group in UL_28_WarrantyReturn.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_28_WarrantyReturn_new.Validate(this, source));
            }

            errorList.AddRange(CheckEquality(UL_30_Court, refBlockNameType.UL_30_Court, currentBlockCashe));
            foreach (var group in UL_30_Court.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_30_Court_new.Validate(this, UL_10_DealUid));
            }

            errorList.AddRange(CheckEquality(UL_31_SourceBankruptcy, refBlockNameType.UL_31_SourceBankruptcy, currentBlockCashe));
            foreach (var group in UL_31_SourceBankruptcy.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_31_SourceBankruptcy_new.Validate(this, UL_10_DealUid));
            }

            errorList.AddRange(CheckEquality(UL_32_SourceLiquidation, refBlockNameType.UL_32_SourceLiquidation, currentBlockCashe));
            foreach (var group in UL_32_SourceLiquidation.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_32_SourceLiquidation_new.Validate(this, UL_10_DealUid));
            }

            errorList.AddRange(CheckEquality(UL_35_StopSend, refBlockNameType.UL_35_StopSend, currentBlockCashe));
            foreach (var group in UL_35_StopSend.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_35_StopSend_new.Validate(this, UL_10_DealUid));
            }

            errorList.AddRange(CheckEquality(UL_41_OrgAcquirer, refBlockNameType.UL_41_OrgAcquirer, currentBlockCashe));
            foreach (var group in UL_41_OrgAcquirer.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_41_OrgAcquirer_new.Validate(this, UL_10_DealUid));
            }

            errorList.AddRange(CheckEquality(UL_42_PersonAcquirer, refBlockNameType.UL_42_PersonAcquirer, currentBlockCashe));
            foreach (var group in UL_42_PersonAcquirer.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_42_PersonAcquirer_new.Validate(this, source, UL_10_DealUid));
            }

            errorList.AddRange(CheckEquality(UL_43_ServiceOrg, refBlockNameType.UL_43_ServiceOrg, currentBlockCashe));
            foreach (var group in UL_43_ServiceOrg.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_43_ServiceOrg_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(UL_44_Accounting, refBlockNameType.UL_44_Accounting, currentBlockCashe));
            foreach (var group in UL_44_Accounting.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_44_Accounting_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(UL_1_Name, refBlockNameType.UL_1_Name, currentBlockCashe));

            errorList.AddRange(CheckEquality(UL_2_Address, refBlockNameType.UL_2_Address, currentBlockCashe));
            foreach (var group in UL_2_Address.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_2_Address_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(UL_3_Reg, refBlockNameType.UL_3_Reg, currentBlockCashe));
            foreach (var group in UL_3_Reg.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_3_Reg_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(UL_4_Tax, refBlockNameType.UL_4_Tax, currentBlockCashe));
            foreach (var group in UL_4_Tax.OrEmptyIfNull())
            {
                foreach (var tax in group.UL_4_Tax_new.OrEmptyIfNull())
                {
                    errorList.AddRange(tax.Validate(this));
                }
            }

            errorList.AddRange(CheckEquality(UL_5_Reorg, refBlockNameType.UL_5_Reorg, currentBlockCashe));
            foreach (var group in UL_5_Reorg.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_5_Reorg_new.Validate(this, source));
            }

            errorList.AddRange(CheckEquality(UL_6_Bankruptcy, refBlockNameType.UL_6_Bankruptcy, currentBlockCashe));
            foreach (var group in UL_6_Bankruptcy.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_6_Bankruptcy_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(UL_7_BankruptcyEnd, refBlockNameType.UL_7_BankruptcyEnd, currentBlockCashe));
            foreach (var group in UL_7_BankruptcyEnd.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_7_BankruptcyEnd_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(UL_33_Collection, refBlockNameType.UL_33_Collection, currentBlockCashe));
            foreach (var group in UL_33_Collection.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_33_Collection_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(UL_45_47_Group, refBlockNameType.UL_45_47_Group, currentBlockCashe));
            foreach (var group in UL_45_47_Group.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_45_47_Group_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(UL_46_Participation, refBlockNameType.UL_46_Participation, currentBlockCashe));
            foreach (var group in UL_46_Participation.OrEmptyIfNull())
            {
                errorList.AddRange(group.UL_46_Participation_new.Validate(this));
            }

            return errorList;
        }
    }
}
