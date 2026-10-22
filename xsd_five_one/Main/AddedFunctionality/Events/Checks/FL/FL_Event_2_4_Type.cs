namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_Event_2_4_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_17_DealUid.Validate(this, source));
            errorList.AddRange(FL_32_35_Group.Validate(this, FL_17_DealUid));
            errorList.AddRange(FL_33_Warranty.Validate(this));
            errorList.AddRange(FL_34_Guarantee.Validate(this));
            errorList.AddRange(FL_36_ProvisionPayment.Validate(this));
            return errorList;
        }
    }
}
