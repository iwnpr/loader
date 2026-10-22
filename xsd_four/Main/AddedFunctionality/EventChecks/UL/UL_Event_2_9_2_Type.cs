namespace Xsd.VersionFour.Main
{
    public partial class UL_Event_2_9_2_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataUL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(UL_10_DealUid.Validate(this, source));
            errorList.AddRange(UL_32_SourceLiquidation.Validate(this, UL_10_DealUid));
            errorList.AddRange(UL_46_Participation.Validate(this, UL_10_DealUid, null, null));
            return errorList;
        }
    }
}
