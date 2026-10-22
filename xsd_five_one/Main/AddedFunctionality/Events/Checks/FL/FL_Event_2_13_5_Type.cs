namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_Event_2_13_5_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_43_1_ProbableFraudInfo.Validate(this));
            return errorList;
        }
    }
}