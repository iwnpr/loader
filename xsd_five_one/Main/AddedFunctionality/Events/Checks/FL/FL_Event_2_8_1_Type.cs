namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_Event_2_8_1_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_17_DealUid.Validate(this, source));
            errorList.AddRange(FL_18_Deal.Validate(this, source, title, FL_17_DealUid));
            errorList.AddRange(FL_19_Amount.Validate(this));
            errorList.AddRange(FL_19_1_AmountInfo.Validate(this, FL_18_Deal));
            errorList.AddRange(FL_41_SourceBankruptcy.Validate(this, FL_17_DealUid));

            return errorList;
        }
    }
}
