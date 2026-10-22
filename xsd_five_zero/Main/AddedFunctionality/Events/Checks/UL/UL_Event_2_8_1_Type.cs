namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_Event_2_8_1_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(UL_10_DealUid.Validate(this, source));
            errorList.AddRange(UL_11_Deal.Validate(this, UL_10_DealUid));
            errorList.AddRange(UL_12_Amount.Validate(this));
            errorList.AddRange(UL_12_1_AmountInfo.Validate(this, UL_11_Deal));
            errorList.AddRange(UL_31_SourceBankruptcy.Validate(this, UL_10_DealUid));

            return errorList;
        }
    }
}
