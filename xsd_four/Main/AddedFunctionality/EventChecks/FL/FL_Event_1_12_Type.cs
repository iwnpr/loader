using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_Event_1_12_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataFL title)
        {
            var errorList = BaseCheck(source, title);
            foreach (var bankruptcy in FL_13_Bankruptcy.OrEmptyIfNull())
            {
                errorList.AddRange(bankruptcy.Validate(this, title.FL_3_Birth));
            }
            errorList.AddRange(FL_14_BankruptcyEnd.Validate(this, title.FL_3_Birth, FL_13_Bankruptcy));
            return errorList;
        }
    }
}
