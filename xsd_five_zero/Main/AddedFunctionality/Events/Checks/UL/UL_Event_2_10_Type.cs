namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_Event_2_10_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(UL_10_DealUid.Validate(this, source));
            errorList.AddRange(UL_35_StopSend.Validate(this, UL_10_DealUid));
            return errorList;
        }
    }
}
