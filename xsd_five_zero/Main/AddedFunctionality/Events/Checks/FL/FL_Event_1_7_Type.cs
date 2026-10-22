namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_Event_1_7_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            errorList.AddRange(FL_1_4_Group.Validate(this, source, FL_2_5_Group, FL_3_Birth));
            errorList.AddRange(FL_2_5_Group.Validate(this, source, FL_3_Birth));
            errorList.AddRange(FL_3_Birth.Validate(this));
            errorList.AddRange(FL_6_Tax.Validate(this));
            errorList.AddRange(FL_7_Social.Validate(this));
            return errorList;
        }
    }
}
