namespace Xsd.VersionFour.Main
{
    public partial class FL_Event_2_12_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataFL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(FL_17_DealUid.Validate(this, source));
            errorList.AddRange(FL_53_ServiceOrg.Validate(this));
            return errorList;
        }
    }
}
