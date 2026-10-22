using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_3_Birth_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_3_Birth;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData? eventData, bool isAF)
        {
            ErrorData errorData = new(blockName, eventData);

            if (isAF && birthDate == "-")
            {
                errorData.AddError("birthDate", birthDate, "Дата рождения ФЛ_3_1 не может быть заполнена символом «-»");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_3_Birth_Type? block, IEventData? eventData, bool isAF = false)
        {
            return [.. (block?.Check(eventData, isAF)).OrEmptyIfNull()];
        }
    }
}
