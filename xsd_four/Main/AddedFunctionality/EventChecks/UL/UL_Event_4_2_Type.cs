namespace Xsd.VersionFour.Main
{
    public partial class UL_Event_4_2_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataUL title)
        {
            var errorList = BaseCheck(source, title);
            //errorList.AddRange(UL_10_DealUid_R.Validate(this, source));
            return errorList;
        }
    }
}
