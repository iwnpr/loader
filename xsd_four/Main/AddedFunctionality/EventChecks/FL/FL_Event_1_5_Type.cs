using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_Event_1_5_Type
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
            errorList.AddRange(FL_12_Capacity.Validate(this, title.FL_3_Birth));
            errorList.AddRange(FL_43_Collection.Validate(this));
            errorList.AddRange(FL_56_Participation.Validate(this, null, null, null));
            return errorList;
        }
    }
}
