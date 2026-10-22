namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_Event_4_2_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            //errorList.AddRange(FL_17_DealUid_R.Validate(this, source));
            return errorList;
        }
    }
}
