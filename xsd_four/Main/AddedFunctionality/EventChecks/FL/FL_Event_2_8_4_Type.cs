namespace Xsd.VersionFour.Main
{
    public partial class FL_Event_2_8_4_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataFL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(FL_17_DealUid.Validate(this, source));
            errorList.AddRange(FL_45_StopSend.Validate(this, FL_17_DealUid));
            errorList.AddRange(FL_51_OrgAcquirer.Validate(this, FL_17_DealUid));
            errorList.AddRange(FL_52_PersonAcquirer.Validate(this, source, FL_17_DealUid));
            return errorList;
        }
    }
}
