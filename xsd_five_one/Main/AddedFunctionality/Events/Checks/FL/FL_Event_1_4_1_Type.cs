namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_Event_1_4_1_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_8_AddrReg.Validate(this, source, title));
            errorList.AddRange(FL_9_AddrFact.Validate(this));
            errorList.AddRange(FL_11_IndividualEntrepreneur.Validate(this, source, title));
            errorList.AddRange(FL_12_Capacity.Validate(this, title));
            errorList.AddRange(FL_17_DealUid.Validate(this, source));
            errorList.AddRange(FL_18_Deal.Validate(this, source, title, FL_17_DealUid));
            errorList.AddRange(FL_19_Amount.Validate(this, FL_18_Deal));
            errorList.AddRange(FL_19_1_AmountInfo.Validate(this, FL_18_Deal));
            errorList.AddRange(FL_20_JointDebtors.Validate(this));
            errorList.AddRange(FL_21_PaymentTerms.Validate(this, FL_17_DealUid, FL_18_Deal));
            errorList.AddRange(FL_24_Fund.Validate(this, FL_17_DealUid));
            errorList.AddRange(FL_25_26_27_28_Group.Validate(this, FL_18_Deal, null, FL_54_Accounting, FL_56_Participation));
            errorList.AddRange(FL_31_NonMonetarySubject.Validate(this, FL_18_Deal));
            errorList.AddRange(FL_30_NonMonetarySource.Validate(this, FL_17_DealUid, FL_18_Deal, FL_24_Fund));
            errorList.AddRange(FL_54_Accounting.Validate(this, FL_18_Deal));
            errorList.AddRange(FL_55_Application.Validate(this, FL_17_DealUid));
            errorList.AddRange(FL_56_Participation.Validate(this, FL_17_DealUid, FL_18_Deal, FL_24_Fund, FL_25_26_27_28_Group));
            return errorList;
        }
    }
}
