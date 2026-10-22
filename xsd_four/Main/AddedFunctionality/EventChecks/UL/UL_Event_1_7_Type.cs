using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_Event_1_7_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataFL title)
        {
            var errorList = BaseCheck(source, title);

            foreach (var address in UL_2_Address.OrEmptyIfNull())
            {
                errorList.AddRange(address.Validate(this));
            }
            errorList.AddRange(UL_3_Reg.Validate(this, UL_2_Address, UL_4_Tax));

            foreach (var tax in UL_4_Tax.OrEmptyIfNull())
            {
                errorList.AddRange(tax.Validate(this, UL_3_Reg));
            }

            foreach (var reorg in UL_5_Reorg.OrEmptyIfNull())
            {
                errorList.AddRange(reorg.Validate(this, source));
            }
            return errorList;
        }
    }
}
