namespace Xsd.VersionFour.Main
{
    public partial class UL_Event_1_1_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataUL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(UL_45_Application.Validate(this));
            return errorList;
        }
    }
}
