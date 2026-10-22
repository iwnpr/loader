using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_12_Amount_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_12_Amount;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, UL_11_Deal_Type? dealBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (!CurrencyDictionary.Contains(currency))
            {
                errorData.AddError("currency", currency, "Валюта обязательства ЮЛ_12.2 не соответствует Общероссийскому классификатору валют");
            }
            if (calcDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("calcDate", calcDate, "Дата расчета ЮЛ_12.6 позднее даты и времени наступления события eventDateTime");
            }
            if (calcDate.DateLess(dealBlock?.startDate))
            {
                errorData.AddError("calcDate", calcDate, "Дата расчета ЮЛ_12.6 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_12_Amount_Type block, IEventData eventData, UL_11_Deal_Type? dealBlock = null)
        {
            return [.. (block?.Check(eventData, dealBlock)).OrEmptyIfNull()];
        }
    }
}
