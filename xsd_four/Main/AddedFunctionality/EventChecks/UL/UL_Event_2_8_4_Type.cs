namespace Xsd.VersionFour.Main
{
    public partial class UL_Event_2_8_4_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataUL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(UL_10_DealUid.Validate(this, source));
            errorList.AddRange(UL_35_StopSend.Validate(this, UL_10_DealUid));
            errorList.AddRange(UL_41_OrgAcquirer.Validate(this, UL_10_DealUid));
            errorList.AddRange(UL_42_PersonAcquirer.Validate(this, source, UL_10_DealUid));
            return errorList;
        }
    }
}
