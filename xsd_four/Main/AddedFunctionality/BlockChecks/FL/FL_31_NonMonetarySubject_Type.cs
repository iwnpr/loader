using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_31_NonMonetarySubject_Type : Block_Type
    {
        public FL_31_NonMonetarySubject_Type()
        {
            blockName = refBlockNameType.FL_31_NonMonetarySubject;
        }

    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_31_NonMonetarySubject_Type[] blocks, EventData eventData, FL_18_Deal_Type dealBlock = null)
        {
            var errorList = new List<INotificationOfAcceptanceError>();

            if (dealBlock?.monetarySubjectExist == false && blocks.OrEmptyIfNull().Count() == 0)
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_31_NonMonetarySubject, null, null, "Отсутствует блок 31. Сведения о неденежном обязательстве субъекта"));
            }
            if (dealBlock?.monetarySubjectExist == true && blocks.OrEmptyIfNull().Count() != 0)
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_31_NonMonetarySubject, null, null, "Блок 31. Сведения о неденежном обязательстве субъекта должен отсутствовать"));
            }

            return errorList;
        }
    }

}
