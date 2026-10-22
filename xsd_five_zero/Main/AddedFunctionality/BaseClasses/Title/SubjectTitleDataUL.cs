namespace Xsd.VersionFiveZero.Main
{
    public partial class SubjectTitleDataUL
    {
        public List<INotificationOfAcceptanceError> Check(ISource source)
        {
            var errorList = new List<INotificationOfAcceptanceError>();

            errorList.AddRange(UL_2_Address.Validate(null));
            errorList.AddRange(UL_3_Reg.Validate(null, UL_2_Address, UL_4_Tax));
            errorList.AddRange(UL_4_Tax.Validate(null, UL_3_Reg));
            errorList.AddRange(UL_5_Reorg.Validate(null, source));

            return errorList;
        }
    }
}
