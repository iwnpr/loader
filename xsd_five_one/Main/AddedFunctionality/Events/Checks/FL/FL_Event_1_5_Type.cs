namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_Event_1_5_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_8_AddrReg.Validate(this, source, title));
            errorList.AddRange(FL_9_AddrFact.Validate(this));
            errorList.AddRange(FL_11_IndividualEntrepreneur.Validate(this, source, title));
            errorList.AddRange(FL_12_Capacity.Validate(this, title));
            errorList.AddRange(FL_43_Collection.Validate(this));
            errorList.AddRange(FL_56_Participation.Validate(this, null, null, null, null));
            return errorList;
        }
    }
}
