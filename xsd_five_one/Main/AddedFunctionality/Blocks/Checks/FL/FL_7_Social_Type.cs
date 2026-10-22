using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_7_Social_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_7_Social;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData? eventData)
        {
            ErrorData errorData = new(blockName, eventData);

            if (!SnilsHelper.CheckSnils(socialNum))
            {
                errorData.AddError("socialNum", socialNum, "В СНИЛСе ФЛ_7.1 неверный контрольный символ");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_7_Social_Type? block, IEventData? eventData)
        {
            return [.. (block?.Check(eventData)).OrEmptyIfNull()];
        }
    }
}
