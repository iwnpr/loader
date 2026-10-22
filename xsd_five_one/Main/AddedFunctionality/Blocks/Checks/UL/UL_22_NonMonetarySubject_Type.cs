using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_22_NonMonetarySubject_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_22_NonMonetarySubject;
            }
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_22_NonMonetarySubject_Type[]? blocks, IEventData eventData, UL_11_Deal_Type? dealBlock = null)
        {
            List<INotificationOfAcceptanceError> errorList = [];

            if (dealBlock?.monetarySubjectExist == false && !blocks.OrEmptyIfNull().Any())
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_22_NonMonetarySubject, "Отсутствует блок 22. Сведения о неденежном обязательстве субъекта"));
            }
            if (dealBlock?.monetarySubjectExist == true && blocks.OrEmptyIfNull().Any())
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_22_NonMonetarySubject, "Блок 22. Сведения о неденежном обязательстве субъекта должен отсутствовать"));
            }

            return errorList;
        }
    }
}
