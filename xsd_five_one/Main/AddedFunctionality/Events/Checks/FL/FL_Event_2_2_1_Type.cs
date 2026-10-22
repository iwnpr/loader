using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_Event_2_2_1_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_17_DealUid.Validate(this, source));
            errorList.AddRange(FL_18_Deal.Validate(this, source, title, FL_17_DealUid));
            errorList.AddRange(FL_19_Amount.Validate(this, FL_18_Deal));
            errorList.AddRange(FL_19_1_AmountInfo.Validate(this, FL_18_Deal));
            errorList.AddRange(FL_21_PaymentTerms.Validate(this, FL_17_DealUid, FL_18_Deal));
            errorList.AddRange(FL_24_Fund.Validate(this, FL_17_DealUid));
            errorList.AddRange(FL_31_NonMonetarySubject.Validate(this, FL_18_Deal));
            errorList.AddRange(FL_30_NonMonetarySource.Validate(this, FL_17_DealUid, FL_18_Deal, FL_24_Fund.ConvertToArray()));
            errorList.AddRange(FL_54_Accounting.Validate(this, FL_18_Deal));
            errorList.AddRange(FL_56_Participation.Validate(this, FL_17_DealUid, FL_18_Deal, FL_24_Fund.ConvertToArray(), null));
            return errorList;
        }
    }
}
