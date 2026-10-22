namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_Event_4_2_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            //errorList.AddRange(UL_10_DealUid_R.Validate(this, source));
            return errorList;
        }
    }
}
