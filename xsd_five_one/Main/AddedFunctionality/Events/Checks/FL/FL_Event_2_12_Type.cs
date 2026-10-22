namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_Event_2_12_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_17_DealUid.Validate(this, source));
            errorList.AddRange(FL_53_ServiceOrg.Validate(this, FL_17_DealUid));
            return errorList;
        }
    }
}
