using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_Event_1_12_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataUL title)
        {
            var errorList = BaseCheck(source, title);
            foreach (var bankruptcy in UL_6_Bankruptcy.OrEmptyIfNull())
            {
                errorList.AddRange(bankruptcy.Validate(this));
            }
            errorList.AddRange(UL_7_BankruptcyEnd.Validate(this, UL_6_Bankruptcy));
            return errorList;
        }
    }
}
