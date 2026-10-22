using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_Event_1_9_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataFL title)
        {
            var errorList = BaseCheck(source, title);
            foreach (var addrReg in FL_8_AddrReg.OrEmptyIfNull())
            {
                errorList.AddRange(addrReg.Validate(this, source, title.FL_3_Birth));
            }
            errorList.AddRange(FL_9_AddrFact.Validate(this));
            errorList.AddRange(FL_11_IndividualEntrepreneur.Validate(this, source, title.FL_6_Tax));
            return errorList;
        }
    }
}
