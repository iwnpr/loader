namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_Event_2_11_1_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_17_DealUid.Validate(this, source));
            errorList.AddRange(FL_18_Deal.Validate(this, source, title, FL_17_DealUid));
            errorList.AddRange(FL_19_Amount.Validate(this, FL_18_Deal));
            errorList.AddRange(FL_19_1_AmountInfo.Validate(this, FL_18_Deal));
            errorList.AddRange(FL_21_PaymentTerms.Validate(this, FL_17_DealUid, FL_18_Deal));
            errorList.AddRange(FL_22_TotalCost.Validate(this, FL_17_DealUid, FL_18_Deal));
            errorList.AddRange(FL_25_26_27_28_Group.Validate(this, FL_18_Deal, FL_29_MonthlyPayment, FL_54_Accounting, FL_56_Participation));
            errorList.AddRange(FL_29_MonthlyPayment.Validate(this, FL_17_DealUid, FL_18_Deal, FL_25_26_27_28_Group));
            errorList.AddRange(FL_29_1_DebtBurdenInfo.Validate(this, source, FL_17_DealUid, FL_18_Deal, FL_19_Amount, null));
            errorList.AddRange(FL_54_Accounting.Validate(this, FL_18_Deal));
            errorList.AddRange(FL_31_NonMonetarySubject.Validate(this, FL_18_Deal));
            errorList.AddRange(FL_20_JointDebtors.Validate(this));
            errorList.AddRange(FL_23_ContractChanges.Validate(this, FL_17_DealUid));
            errorList.AddRange(FL_23_1_ContractTermsChanges.Validate(this, FL_17_DealUid));
            errorList.AddRange(FL_30_NonMonetarySource.Validate(this, FL_17_DealUid, FL_18_Deal, null));
            errorList.AddRange(FL_51_OrgAcquirer.Validate(this, FL_17_DealUid));
            errorList.AddRange(FL_52_PersonAcquirer.Validate(this, source, FL_17_DealUid));
            errorList.AddRange(FL_56_Participation.Validate(this, FL_17_DealUid, FL_18_Deal, null, FL_25_26_27_28_Group));
            return errorList;
        }
    }
}
