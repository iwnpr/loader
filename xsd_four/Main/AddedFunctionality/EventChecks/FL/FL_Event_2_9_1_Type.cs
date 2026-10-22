using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_Event_2_9_1_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataFL title)
        {
            var errorList = BaseCheck(source, title);
            errorList.AddRange(FL_17_DealUid.Validate(this, source));
            errorList.AddRange(FL_18_Deal.Validate(this, source, title.FL_6_Tax, FL_17_DealUid));
            errorList.AddRange(FL_19_Amount.Validate(this));
            foreach (var amountInfo in FL_19_1_AmountInfo.OrEmptyIfNull())
                errorList.AddRange(amountInfo.Validate(this, FL_18_Deal));
            errorList.AddRange(FL_42_SourceLiquidation.Validate(this, FL_17_DealUid));

            return errorList;
        }
    }
}
