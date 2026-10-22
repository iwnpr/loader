using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_Event_4_1_1_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var currentBlockCashe = title.CurrentBlockCache;
            if (title.SkipTitleChecksInChangeEvents)
            {
                title = null;
            }

            var errorList = base.Check(source, title);

            errorList.AddRange(CheckEquality(FL_43_1_ProbableFraudInfo.ConvertToArray()!, refBlockNameType.FL_32_35_Group, currentBlockCashe));
            errorList.AddRange(FL_43_1_ProbableFraudInfo.FL_43_1_ProbableFraudInfo_new.Validate(this));

            return errorList;
        }
    }
}
