using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_Event_2_6_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataFL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(FL_17_DealUid.Validate(this, source));
            foreach (var court in FL_39_Court.OrEmptyIfNull())
            {
                errorList.AddRange(court.Validate(this, FL_17_DealUid));
            }
            return errorList;
        }
    }
}
