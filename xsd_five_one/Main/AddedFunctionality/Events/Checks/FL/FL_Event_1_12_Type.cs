namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_Event_1_12_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_13_Bankruptcy.Validate(this, source, title));
            errorList.AddRange(FL_14_BankruptcyEnd.Validate(this, title, FL_13_Bankruptcy));
            return errorList;
        }
    }
}
