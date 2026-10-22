using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_Event_2_4_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataFL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(FL_17_DealUid.Validate(this, source));
            errorList.AddRange(FL_32_35_Group.Validate(this, FL_17_DealUid));
            foreach (var warranty in FL_33_Warranty.OrEmptyIfNull())
            {
                errorList.AddRange(warranty.Validate(this));
            }
            foreach (var guarantee in FL_34_Guarantee.OrEmptyIfNull())
            {
                errorList.AddRange(guarantee.Validate(this));
            }
            foreach (var provisionPayment in FL_36_ProvisionPayment.OrEmptyIfNull())
            {
                errorList.AddRange(provisionPayment.Validate(this));
            }
            return errorList;
        }
    }
}
