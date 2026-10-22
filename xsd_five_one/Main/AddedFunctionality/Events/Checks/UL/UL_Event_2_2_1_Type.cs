using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_Event_2_2_1_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(UL_10_DealUid.Validate(this, source));
            errorList.AddRange(UL_11_Deal.Validate(this, UL_10_DealUid));
            errorList.AddRange(UL_12_Amount.Validate(this, UL_11_Deal));
            errorList.AddRange(UL_12_1_AmountInfo.Validate(this, UL_11_Deal));
            errorList.AddRange(UL_14_PaymentTerms.Validate(this, UL_10_DealUid, UL_11_Deal));
            errorList.AddRange(UL_16_Fund.Validate(this, UL_10_DealUid));
            errorList.AddRange(UL_22_NonMonetarySubject.Validate(this, UL_11_Deal));
            errorList.AddRange(UL_21_NonMonetarySource.Validate(this, UL_10_DealUid, UL_11_Deal, UL_16_Fund.ConvertToArray()));
            errorList.AddRange(UL_44_Accounting.Validate(this, UL_11_Deal));
            errorList.AddRange(UL_46_Participation.Validate(this, UL_10_DealUid, UL_11_Deal, null, UL_16_Fund.ConvertToArray()));
            return errorList;
        }
    }
}
