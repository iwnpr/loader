namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_Event_1_1_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(UL_45_Application.Validate(this));
            return errorList;
        }
    }
}
