using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_12_Amount_Type : Block_Type
    {
        public UL_12_Amount_Type()
        {
            blockName = refBlockNameType.UL_12_Amount;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, UL_11_Deal_Type dealBlock = null)
        {
            InitCheck(eventData);

            if (!CurrencyDictionary.Contains(currency))
            {
                AddError("currency", currency, "Валюта обязательства ЮЛ_12.2 не соответствует Общероссийскому классификатору валют");
            }
            if (calcDate.DateGreater(eventData?.eventDate))
            {
                AddError("calcDate", calcDate, "Дата расчета ЮЛ_12.6 позднее даты наступления события eventDate");
            }
            if (calcDate.DateLess(dealBlock?.startDate))
            {
                AddError("calcDate", calcDate, "Дата расчета ЮЛ_12.6 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_12_Amount_Type block, EventData eventData, UL_11_Deal_Type dealBlock = null)
        {
            return (block?.Check(eventData, dealBlock)).OrEmptyIfNull().ToList();
        }
    }
}
