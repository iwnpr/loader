using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_22_NonMonetarySubject_Type : Block_Type
    {
        public UL_22_NonMonetarySubject_Type()
        {
            blockName = refBlockNameType.UL_22_NonMonetarySubject;
        }

    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_22_NonMonetarySubject_Type[] blocks, EventData eventData, UL_11_Deal_Type dealBlock = null)
        {
            var errorList = new List<INotificationOfAcceptanceError>();

            if (dealBlock?.monetarySubjectExist == false && blocks.OrEmptyIfNull().Count() == 0)
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_22_NonMonetarySubject, null, null, "Отсутствует блок 22. Сведения о неденежном обязательстве субъекта"));
            }
            if (dealBlock?.monetarySubjectExist == true && blocks.OrEmptyIfNull().Count() != 0)
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_22_NonMonetarySubject, null, null, "Блок 22. Сведения о неденежном обязательстве субъекта должен отсутствовать"));
            }

            return errorList;
        }
    }

}
