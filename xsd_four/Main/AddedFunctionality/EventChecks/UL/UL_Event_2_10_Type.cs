namespace Xsd.VersionFour.Main
{
    public partial class UL_Event_2_10_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataUL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(UL_10_DealUid.Validate(this, source));
            errorList.AddRange(UL_35_StopSend.Validate(this, UL_10_DealUid));
            return errorList;
        }
    }
}
