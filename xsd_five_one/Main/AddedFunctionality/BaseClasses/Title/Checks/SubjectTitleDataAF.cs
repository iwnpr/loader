namespace Xsd.VersionFiveOne.Main
{
    public partial class SubjectTitleDataAF
    {
        public List<INotificationOfAcceptanceError> Check(ISource source)
        {
            var errorList = new List<INotificationOfAcceptanceError>();

            errorList.AddRange(FL_1_4_Group.Validate(null, source, FL_2_5_Group, FL_3_Birth));
            errorList.AddRange(FL_2_5_Group.Validate(null, source, FL_3_Birth));
            errorList.AddRange(FL_3_Birth.Validate(null, true));
            errorList.AddRange(FL_6_Tax.Validate(null, FL_1_4_Group, true));
            errorList.AddRange(FL_7_Social.Validate(null));


            return errorList;
        }
    }
}