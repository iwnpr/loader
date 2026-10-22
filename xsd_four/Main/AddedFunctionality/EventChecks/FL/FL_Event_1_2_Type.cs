namespace Xsd.VersionFour.Main
{
    public partial class FL_Event_1_2_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataFL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(FL_29_1_DebtBurdenInfo.Validate(this, source, null, null, null, null));
            errorList.AddRange(FL_55_Application.Validate(this));
            return errorList;
        }
    }
}
