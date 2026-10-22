namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_Event_1_6_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(UL_33_Collection.Validate(this));
            errorList.AddRange(UL_46_Participation.Validate(this, null, null, null));
            return errorList;
        }
    }
}
