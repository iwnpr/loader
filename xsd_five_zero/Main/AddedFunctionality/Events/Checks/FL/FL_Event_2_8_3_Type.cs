namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_Event_2_8_3_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_17_DealUid.Validate(this, source));
            errorList.AddRange(FL_29_MonthlyPayment.Validate(this, null));
            errorList.AddRange(FL_41_SourceBankruptcy.Validate(this, FL_17_DealUid));
            errorList.AddRange(FL_56_Participation.Validate(this, FL_17_DealUid, null, null, null));
            return errorList;
        }
    }
}
