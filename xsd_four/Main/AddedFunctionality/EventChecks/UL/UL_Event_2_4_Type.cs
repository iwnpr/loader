using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_Event_2_4_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataUL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(UL_10_DealUid.Validate(this, source));
            errorList.AddRange(UL_23_26_Group.Validate(this, UL_10_DealUid));
            foreach (var warranty in UL_24_Warranty.OrEmptyIfNull())
            {
                errorList.AddRange(warranty.Validate(this));
            }
            foreach (var guarantee in UL_25_Guarantee.OrEmptyIfNull())
            {
                errorList.AddRange(guarantee.Validate(this));
            }
            foreach (var provisionPayment in UL_27_ProvisionPayment.OrEmptyIfNull())
            {
                errorList.AddRange(provisionPayment.Validate(this));
            }
            return errorList;
        }
    }
}
