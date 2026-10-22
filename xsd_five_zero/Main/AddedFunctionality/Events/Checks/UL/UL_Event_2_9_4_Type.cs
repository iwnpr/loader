namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_Event_2_9_4_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(UL_10_DealUid.Validate(this, source));
            errorList.AddRange(UL_35_StopSend.Validate(this, UL_10_DealUid));
            errorList.AddRange(UL_41_OrgAcquirer.Validate(this, UL_10_DealUid));
            errorList.AddRange(UL_42_PersonAcquirer.Validate(this, source, UL_10_DealUid));
            return errorList;
        }
    }
}
