namespace Xsd.VersionFour.Main
{
    public partial class FL_Event_4_2_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataFL title)
        {
            var errorList = BaseCheck(source, title);
            //errorList.AddRange(FL_17_DealUid_R.Validate(this, source));
            return errorList;
        }
    }
}
