using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_31_NonMonetarySubject_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_31_NonMonetarySubject;
            }
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_31_NonMonetarySubject_Type> blocks, IEventData eventData, FL_18_Deal_Type? dealBlock)
        {
            List<INotificationOfAcceptanceError> errorList = [];

            if (dealBlock?.monetarySubjectExist == false && !blocks.OrEmptyIfNull().Any())
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_31_NonMonetarySubject, "Отсутствует блок 31. Сведения о неденежном обязательстве субъекта"));
            }
            if (dealBlock?.monetarySubjectExist == true && blocks.OrEmptyIfNull().Any())
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_31_NonMonetarySubject, "Блок 31. Сведения о неденежном обязательстве субъекта должен отсутствовать"));
            }

            return errorList;
        }
    }
}
