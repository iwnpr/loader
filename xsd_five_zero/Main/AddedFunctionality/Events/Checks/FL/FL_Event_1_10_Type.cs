namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_Event_1_10_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_12_Capacity.Validate(this, title));
            return errorList;
        }
    }
}
