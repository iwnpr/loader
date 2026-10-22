namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_Event_4_4_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            //errorList.AddRange(FL_55_Application_R.Validate(this, null));
            //errorList.AddRange(FL_29_1_DebtBurdenInfo.Validate(this, source, null, null, null, FL_55_Application_R));
            return errorList;
        }
    }
}
