using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_Event_2_6_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataUL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(UL_10_DealUid.Validate(this, source));
            foreach (var court in UL_30_Court.OrEmptyIfNull())
            {
                errorList.AddRange(court.Validate(this, UL_10_DealUid));
            }
            return errorList;
        }
    }
}
