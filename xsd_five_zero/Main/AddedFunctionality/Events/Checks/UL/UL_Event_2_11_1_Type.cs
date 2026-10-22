namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_Event_2_11_1_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(UL_10_DealUid.Validate(this, source));
            errorList.AddRange(UL_11_Deal.Validate(this, UL_10_DealUid));
            errorList.AddRange(UL_12_Amount.Validate(this, UL_11_Deal));
            errorList.AddRange(UL_12_1_AmountInfo.Validate(this, UL_11_Deal));
            errorList.AddRange(UL_14_PaymentTerms.Validate(this, UL_10_DealUid, UL_11_Deal));
            errorList.AddRange(UL_17_18_19_20_Group.Validate(this, UL_11_Deal, UL_44_Accounting, UL_46_Participation));
            errorList.AddRange(UL_44_Accounting.Validate(this, UL_11_Deal));
            errorList.AddRange(UL_22_NonMonetarySubject.Validate(this, UL_11_Deal));
            errorList.AddRange(UL_13_JointDebtors.Validate(this));
            errorList.AddRange(UL_15_ContractChanges.Validate(this, UL_10_DealUid));
            errorList.AddRange(UL_15_1_ContractTermsChanges.Validate(this, UL_10_DealUid));
            errorList.AddRange(UL_21_NonMonetarySource.Validate(this, UL_10_DealUid, UL_11_Deal, null));
            errorList.AddRange(UL_41_OrgAcquirer.Validate(this, UL_10_DealUid));
            errorList.AddRange(UL_42_PersonAcquirer.Validate(this, source, UL_10_DealUid));
            errorList.AddRange(UL_46_Participation.Validate(this, UL_10_DealUid, UL_11_Deal, UL_17_18_19_20_Group));
            return errorList;
        }
    }
}
