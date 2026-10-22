using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_Event_2_1_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataUL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(UL_10_DealUid.Validate(this, source));
            errorList.AddRange(UL_11_Deal.Validate(this, UL_10_DealUid));
            errorList.AddRange(UL_12_Amount.Validate(this, UL_11_Deal));
            foreach (var amountInfo in UL_12_1_AmountInfo.OrEmptyIfNull())
            {
                errorList.AddRange(amountInfo.Validate(this, UL_11_Deal));
            }
            errorList.AddRange(UL_14_PaymentTerms.Validate(this, UL_10_DealUid, UL_11_Deal));
            errorList.AddRange(UL_17_18_19_20_Group.Validate(this, UL_11_Deal, null));
            errorList.AddRange(UL_22_NonMonetarySubject.Validate(this, UL_11_Deal));
            errorList.AddRange(UL_13_JointDebtors.Validate(this));
            foreach (var contractChanges in UL_15_ContractChanges.OrEmptyIfNull())
            {
                errorList.AddRange(contractChanges.Validate(this, UL_10_DealUid));
            }
            foreach (var contractTermsChanges in UL_15_1_ContractTermsChanges.OrEmptyIfNull())
            {
                errorList.AddRange(contractTermsChanges.Validate(this, UL_10_DealUid));
            }
            errorList.AddRange(UL_21_NonMonetarySource.Validate(this, UL_10_DealUid, UL_11_Deal, null));
            errorList.AddRange(UL_44_Accounting.Validate(this, UL_11_Deal));
            return errorList;
        }
    }
}
