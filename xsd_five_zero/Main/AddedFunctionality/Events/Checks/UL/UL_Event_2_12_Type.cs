namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_Event_2_12_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(UL_10_DealUid.Validate(this, source));
            errorList.AddRange(UL_43_ServiceOrg.Validate(this));
            return errorList;
        }
    }
}
