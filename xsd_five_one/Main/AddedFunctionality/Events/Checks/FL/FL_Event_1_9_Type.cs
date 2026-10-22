namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_Event_1_9_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_8_AddrReg.Validate(this, source, title));
            errorList.AddRange(FL_9_AddrFact.Validate(this));
            errorList.AddRange(FL_11_IndividualEntrepreneur.Validate(this, source, title));
            return errorList;
        }
    }
}
