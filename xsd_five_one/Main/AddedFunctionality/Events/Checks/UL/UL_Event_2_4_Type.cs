namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_Event_2_4_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(UL_10_DealUid.Validate(this, source));
            errorList.AddRange(UL_23_26_Group.Validate(this, UL_10_DealUid));
            errorList.AddRange(UL_24_Warranty.Validate(this));
            errorList.AddRange(UL_25_Guarantee.Validate(this));
            errorList.AddRange(UL_27_ProvisionPayment.Validate(this));
            return errorList;
        }
    }
}
