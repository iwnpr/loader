namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_Event_1_3_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_55_Application.Validate(this, null));
            errorList.AddRange(FL_57_Reject.Validate(this, FL_55_Application));
            return errorList;
        }
    }
}
