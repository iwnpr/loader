namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_Event_1_3_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(UL_45_Application.Validate(this));
            errorList.AddRange(UL_47_Reject.Validate(this, UL_45_Application));
            return errorList;
        }
    }
}
