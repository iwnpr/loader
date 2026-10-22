namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_Event_1_7_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);

            errorList.AddRange(UL_2_Address.Validate(this));
            errorList.AddRange(UL_3_Reg.Validate(this, UL_2_Address, UL_4_Tax));
            errorList.AddRange(UL_4_Tax.Validate(this, UL_3_Reg));
            errorList.AddRange(UL_5_Reorg.Validate(this, source));

            return errorList;
        }
    }
}
