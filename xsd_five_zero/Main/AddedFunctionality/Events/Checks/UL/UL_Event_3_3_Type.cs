namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_Event_3_3_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            //errorList.AddRange(UL_10_DealUid_R.Validate(this, source));
            return errorList;
        }
    }
}
