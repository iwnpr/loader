namespace Xsd.VersionFour.Main
{
    public partial class FL_Event_1_3_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataFL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(FL_55_Application.Validate(this));
            errorList.AddRange(FL_57_Reject.Validate(this, FL_55_Application));
            return errorList;
        }
    }
}
