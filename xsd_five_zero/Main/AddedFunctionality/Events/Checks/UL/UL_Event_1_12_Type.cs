namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_Event_1_12_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(UL_6_Bankruptcy.Validate(this, source));
            errorList.AddRange(UL_7_BankruptcyEnd.Validate(this, UL_6_Bankruptcy));
            return errorList;
        }
    }
}
