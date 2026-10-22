using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_7_Social_Type : Block_Type
    {
        public FL_7_Social_Type()
        {
            blockName = refBlockNameType.FL_7_Social;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            if (!SnilsHelper.CheckSnils(socialNum))
            {
                AddError("socialNum", socialNum, "В СНИЛСе ФЛ_7.1 неверный контрольный символ");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_7_Social_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
