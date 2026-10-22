using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class SubjectTitleDataUL
    {
        public List<INotificationOfAcceptanceError> Check(ISource source)
        {
            var errorList = new List<INotificationOfAcceptanceError>();

            foreach (var address in UL_2_Address.OrEmptyIfNull())
            {
                errorList.AddRange(address.Validate(null));
            }
            errorList.AddRange(UL_3_Reg.Validate(null, UL_2_Address, UL_4_Tax));

            foreach (var tax in UL_4_Tax.OrEmptyIfNull())
            {
                errorList.AddRange(tax.Validate(null, UL_3_Reg));
            }

            foreach (var reorg in UL_5_Reorg.OrEmptyIfNull())
            {
                errorList.AddRange(reorg.Validate(null, source));
            }

            return errorList;
        }
    }
}
