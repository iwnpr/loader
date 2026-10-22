using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_Event_4_1_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var currentBlockCashe = title.CurrentBlockCache;
            ISubjectTitleData? titleForChecks = title;
            if (title.SkipTitleChecksInChangeEvents)
            {
                titleForChecks = null;
            }

            var errorList = base.Check(source, titleForChecks);

            errorList.AddRange(FL_17_DealUid_R.Validate(this, source));
            errorList.AddRange(CheckEquality(FL_32_35_Group, refBlockNameType.FL_32_35_Group, currentBlockCashe));
            foreach (var group in FL_32_35_Group.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_32_35_Group_new.Validate(this, FL_17_DealUid_R));
            }

            errorList.AddRange(CheckEquality(FL_33_Warranty, refBlockNameType.FL_33_Warranty, currentBlockCashe));
            foreach (var group in FL_33_Warranty.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_33_Warranty_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(FL_34_Guarantee, refBlockNameType.FL_34_Guarantee, currentBlockCashe));
            foreach (var group in FL_34_Guarantee.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_34_Guarantee_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(FL_36_ProvisionPayment, refBlockNameType.FL_36_ProvisionPayment, currentBlockCashe));
            foreach (var group in FL_36_ProvisionPayment.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_36_ProvisionPayment_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(FL_36_1_ProvisionPaymentOffset, refBlockNameType.FL_36_1_ProvisionPaymentOffset, currentBlockCashe));
            foreach (var group in FL_36_1_ProvisionPaymentOffset.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_36_1_ProvisionPaymentOffset_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(FL_37_WarrantyReturn, refBlockNameType.FL_37_WarrantyReturn, currentBlockCashe));
            foreach (var group in FL_37_WarrantyReturn.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_37_WarrantyReturn_new.Validate(this, source));
            }

            errorList.AddRange(CheckEquality(FL_39_Court, refBlockNameType.FL_39_Court, currentBlockCashe));
            foreach (var group in FL_39_Court.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_39_Court_new.Validate(this, FL_17_DealUid_R));
            }

            errorList.AddRange(CheckEquality(FL_41_SourceBankruptcy, refBlockNameType.FL_41_SourceBankruptcy, currentBlockCashe));
            foreach (var group in FL_41_SourceBankruptcy.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_41_SourceBankruptcy_new.Validate(this, FL_17_DealUid_R));
            }

            errorList.AddRange(CheckEquality(FL_42_SourceLiquidation, refBlockNameType.FL_42_SourceLiquidation, currentBlockCashe));
            foreach (var group in FL_42_SourceLiquidation.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_42_SourceLiquidation_new.Validate(this, FL_17_DealUid_R));
            }

            errorList.AddRange(CheckEquality(FL_45_StopSend, refBlockNameType.FL_45_StopSend, currentBlockCashe));
            foreach (var group in FL_45_StopSend.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_45_StopSend_new.Validate(this, FL_17_DealUid_R));
            }

            errorList.AddRange(CheckEquality(FL_51_OrgAcquirer, refBlockNameType.FL_51_OrgAcquirer, currentBlockCashe));
            foreach (var group in FL_51_OrgAcquirer.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_51_OrgAcquirer_new.Validate(this, FL_17_DealUid_R));
            }

            errorList.AddRange(CheckEquality(FL_51_1_PrevOrgAcquirer, refBlockNameType.FL_51_1_PrevOrgAcquirer, currentBlockCashe));
            foreach (var group in FL_51_1_PrevOrgAcquirer.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_51_1_PrevOrgAcquirer_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(FL_52_PersonAcquirer, refBlockNameType.FL_52_PersonAcquirer, currentBlockCashe));
            foreach (var group in FL_52_PersonAcquirer.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_52_PersonAcquirer_new.Validate(this, source, FL_17_DealUid_R));
            }

            errorList.AddRange(CheckEquality(FL_52_1_PrevPersonAcquirer, refBlockNameType.FL_52_1_PrevPersonAcquirer, currentBlockCashe));
            foreach (var group in FL_52_1_PrevPersonAcquirer.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_52_1_PrevPersonAcquirer_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(FL_53_ServiceOrg, refBlockNameType.FL_53_ServiceOrg, currentBlockCashe));
            foreach (var group in FL_53_ServiceOrg.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_53_ServiceOrg_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(FL_54_Accounting, refBlockNameType.FL_54_Accounting, currentBlockCashe));
            foreach (var group in FL_54_Accounting.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_54_Accounting_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(FL_1_4_Group, refBlockNameType.FL_1_4_Group, currentBlockCashe));
            foreach (var group in FL_1_4_Group.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_1_4_Group_new.Validate(this, source, null, title));
            }

            errorList.AddRange(CheckEquality(FL_2_5_Group, refBlockNameType.FL_2_5_Group, currentBlockCashe));
            foreach (var group in FL_2_5_Group.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_2_5_Group_new.Validate(this, source, title));
            }

            errorList.AddRange(CheckEquality(FL_3_Birth, refBlockNameType.FL_3_Birth, currentBlockCashe));
            foreach (var group in FL_3_Birth.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_3_Birth_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(FL_6_Tax, refBlockNameType.FL_6_Tax, currentBlockCashe));
            errorList.AddRange(FL_6_Tax.OrEmptyIfNull().Where(x => x.FL_6_Tax_new != null).Select(x => x.FL_6_Tax_new).Validate(this));

            errorList.AddRange(CheckEquality(FL_7_Social, refBlockNameType.FL_7_Social, currentBlockCashe));
            foreach (var group in FL_7_Social.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_7_Social_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(FL_8_AddrReg, refBlockNameType.FL_8_AddrReg, currentBlockCashe));
            foreach (var group in FL_8_AddrReg.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_8_AddrReg_new.Validate(this, source, title));
            }

            errorList.AddRange(CheckEquality(FL_9_AddrFact, refBlockNameType.FL_9_AddrFact, currentBlockCashe));
            foreach (var group in FL_9_AddrFact.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_9_AddrFact_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(FL_10_Contact, refBlockNameType.FL_10_Contact, currentBlockCashe));

            errorList.AddRange(CheckEquality(FL_11_IndividualEntrepreneur, refBlockNameType.FL_11_IndividualEntrepreneur, currentBlockCashe));
            foreach (var group in FL_11_IndividualEntrepreneur.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_11_IndividualEntrepreneur_new.Validate(this, source, title is SubjectTitleDataFL ? ((SubjectTitleDataFL)title).FL_6_Tax : null));
            }

            errorList.AddRange(CheckEquality(FL_12_Capacity, refBlockNameType.FL_12_Capacity, currentBlockCashe));
            foreach (var group in FL_12_Capacity.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_12_Capacity_new.Validate(this, title));
            }

            errorList.AddRange(CheckEquality(FL_13_Bankruptcy, refBlockNameType.FL_13_Bankruptcy, currentBlockCashe));
            foreach (var group in FL_13_Bankruptcy.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_13_Bankruptcy_new.Validate(this, source, title));
            }

            errorList.AddRange(CheckEquality(FL_14_BankruptcyEnd, refBlockNameType.FL_14_BankruptcyEnd, currentBlockCashe));
            foreach (var group in FL_14_BankruptcyEnd.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_14_BankruptcyEnd_new.Validate(this, title));
            }

            errorList.AddRange(CheckEquality(FL_29_1_DebtBurdenInfo, refBlockNameType.FL_29_1_DebtBurdenInfo, currentBlockCashe));
            foreach (var group in FL_29_1_DebtBurdenInfo.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_29_1_DebtBurdenInfo_new.Validate(this, source));
            }

            errorList.AddRange(CheckEquality(FL_43_Collection, refBlockNameType.FL_43_Collection, currentBlockCashe));
            foreach (var group in FL_43_Collection.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_43_Collection_new.Validate(this));
            }

            errorList.AddRange(CheckEquality(FL_55_57_Group_R, refBlockNameType.FL_55_57_Group, currentBlockCashe));
            foreach (var group in FL_55_57_Group_R.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_55_57_Group_R_new.Validate(this, FL_17_DealUid_R));
            }

            errorList.AddRange(CheckEquality(FL_56_Participation, refBlockNameType.FL_56_Participation, currentBlockCashe));
            foreach (var group in FL_56_Participation.OrEmptyIfNull())
            {
                errorList.AddRange(group.FL_56_Participation_new.Validate(this));
            }

            return errorList;
        }
    }
}
