namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_Event_2_6_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_17_DealUid.Validate(this, source));
            errorList.AddRange(FL_39_Court.Validate(this, FL_17_DealUid));
            return errorList;
        }
    }
}
