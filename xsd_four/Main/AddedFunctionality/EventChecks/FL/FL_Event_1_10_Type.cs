namespace Xsd.VersionFour.Main
{
    public partial class FL_Event_1_10_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataFL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(FL_12_Capacity.Validate(this, title.FL_3_Birth));
            return errorList;
        }
    }
}
