using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_Event_2_8_1_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataUL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(UL_10_DealUid.Validate(this, source));
            errorList.AddRange(UL_11_Deal.Validate(this, UL_10_DealUid));
            errorList.AddRange(UL_12_Amount.Validate(this));
            foreach (var amountInfo in UL_12_1_AmountInfo.OrEmptyIfNull())
            {
                errorList.AddRange(amountInfo.Validate(this, UL_11_Deal));
            }
            errorList.AddRange(UL_31_SourceBankruptcy.Validate(this, UL_10_DealUid));

            return errorList;
        }
    }
}
