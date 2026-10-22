using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_19_Amount_Type : Block_Type
    {
        public FL_19_Amount_Type()
        {
            blockName = refBlockNameType.FL_19_Amount;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, FL_18_Deal_Type dealBlock = null)
        {
            InitCheck(eventData);

            if (!CurrencyDictionary.Contains(currency))
            {
                AddError("currency", currency, "Валюта обязательства ФЛ_19.2 не соответствует  Общероссийскому классификатору валют");
            }
            if (calcDate.DateGreater(eventData?.eventDate))
            {
                AddError("calcDate", calcDate, "Дата расчета ФЛ_19.6 позднее даты наступления события eventDate");
            }
            if (calcDate.DateLess(dealBlock?.startDate))
            {
                AddError("calcDate", calcDate, "Дата расчета ФЛ_19.6 раньше даты возникновения обязательства субъекта ФЛ_18.19");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_19_Amount_Type block, EventData eventData, FL_18_Deal_Type dealBlock = null)
        {
            return (block?.Check(eventData, dealBlock)).OrEmptyIfNull().ToList();
        }
    }
}
