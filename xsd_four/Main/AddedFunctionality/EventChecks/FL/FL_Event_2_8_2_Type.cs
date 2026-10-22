namespace Xsd.VersionFour.Main
{
    public partial class FL_Event_2_8_2_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataFL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(FL_17_DealUid.Validate(this, source));
            errorList.AddRange(FL_29_MonthlyPayment.Validate(this, null));
            errorList.AddRange(FL_41_SourceBankruptcy.Validate(this, FL_17_DealUid));
            errorList.AddRange(FL_56_Participation.Validate(this, FL_17_DealUid, null, null));
            return errorList;
        }
    }
}
