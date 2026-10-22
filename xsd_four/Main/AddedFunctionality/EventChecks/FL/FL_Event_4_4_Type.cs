namespace Xsd.VersionFour.Main
{
    public partial class FL_Event_4_4_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataFL title)
        {
            var errorList = BaseCheck(source, title);
            //errorList.AddRange(FL_55_Application_R.Validate(this));
            return errorList;
        }
    }
}
