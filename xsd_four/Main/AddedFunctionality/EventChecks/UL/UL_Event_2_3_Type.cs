using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_Event_2_3_Type
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
            errorList.AddRange(UL_17_18_19_20_Group.Validate(this, UL_11_Deal, UL_46_Participation));
            errorList.AddRange(UL_22_NonMonetarySubject.Validate(this, UL_11_Deal));
            errorList.AddRange(UL_13_JointDebtors.Validate(this));
            errorList.AddRange(UL_21_NonMonetarySource.Validate(this, UL_10_DealUid, UL_11_Deal, null));
            errorList.AddRange(UL_27_1_ProvisionPaymentOffset.Validate(this));
            errorList.AddRange(UL_28_WarrantyReturn.Validate(this, source, UL_11_Deal, UL_12_Amount));
            errorList.AddRange(UL_44_Accounting.Validate(this, UL_11_Deal));
            errorList.AddRange(UL_45_Application.Validate(this));
            errorList.AddRange(UL_46_Participation.Validate(this, UL_10_DealUid, UL_11_Deal, UL_17_18_19_20_Group));
            return errorList;
        }
    }
}
