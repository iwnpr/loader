using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_Event_2_11_1_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataFL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(FL_17_DealUid.Validate(this, source));
            errorList.AddRange(FL_18_Deal.Validate(this, source, title.FL_6_Tax, FL_17_DealUid));
            errorList.AddRange(FL_19_Amount.Validate(this, FL_18_Deal));
            foreach (var amountInfo in FL_19_1_AmountInfo.OrEmptyIfNull())
            {
                errorList.AddRange(amountInfo.Validate(this, FL_18_Deal));
            }
            errorList.AddRange(FL_21_PaymentTerms.Validate(this, FL_17_DealUid, FL_18_Deal));
            errorList.AddRange(FL_22_TotalCost.Validate(this, FL_17_DealUid, FL_18_Deal));
            errorList.AddRange(FL_25_26_27_28_Group.Validate(this, FL_18_Deal, FL_29_MonthlyPayment, FL_56_Participation));
            errorList.AddRange(FL_29_MonthlyPayment.Validate(this, FL_17_DealUid, FL_18_Deal, FL_25_26_27_28_Group));
            errorList.AddRange(FL_29_1_DebtBurdenInfo.Validate(this, source, FL_17_DealUid, FL_18_Deal, FL_19_Amount, null));
            errorList.AddRange(FL_54_Accounting.Validate(this, FL_18_Deal));
            errorList.AddRange(FL_31_NonMonetarySubject.Validate(this, FL_18_Deal));
            errorList.AddRange(FL_20_JointDebtors.Validate(this));
            errorList.AddRange(FL_30_NonMonetarySource.Validate(this, FL_17_DealUid, FL_18_Deal, null));
            errorList.AddRange(FL_56_Participation.Validate(this, FL_17_DealUid, FL_18_Deal, FL_25_26_27_28_Group));
            return errorList;
        }
    }
}
