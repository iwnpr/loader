namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_Event_1_1_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_55_Application.Validate(this, null));
            return errorList;
        }
    }
}
