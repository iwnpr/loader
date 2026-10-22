namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_Event_1_2_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_29_1_DebtBurdenInfo.Validate(this, source, null, null, null, null));
            errorList.AddRange(FL_55_Application.Validate(this, null));
            return errorList;
        }
    }
}
