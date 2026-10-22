namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_Event_1_6_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_43_Collection.Validate(this));
            errorList.AddRange(FL_56_Participation.Validate(this, null, null, null));
            return errorList;
        }
    }
}
