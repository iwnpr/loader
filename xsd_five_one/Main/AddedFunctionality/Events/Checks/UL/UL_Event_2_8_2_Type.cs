namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_Event_2_8_2_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(UL_10_DealUid.Validate(this, source));
            errorList.AddRange(UL_31_SourceBankruptcy.Validate(this, UL_10_DealUid));
            errorList.AddRange(UL_46_Participation.Validate(this, UL_10_DealUid, null, null));
            return errorList;
        }
    }
}
