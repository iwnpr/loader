using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_33_Collection_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_33_Collection;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData)
        {
            ErrorData errorData = new(blockName, eventData);

            if (actDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("actDate", actDate, "Дата принятия судебного акта ЮЛ_33.4 позднее даты и времени наступления события eventDateTime");
            }

            if (endDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("endDate", endDate, "Дата прекращения взыскания ЮЛ_33.7 позднее даты и времени наступления события eventDateTime");

            }
            if (endDate.DateLess(actDate))
            {
                errorData.AddError("endDate", endDate, "Дата прекращения взыскания ЮЛ_33.7 раньше даты принятия судебного акта ЮЛ_33.4");
            }

            if (freqCode == refPaymentFreqType.Item7 && onceSum == null)
            {
                errorData.AddError("onceSum", onceSum, "Отсутствует сумма единовременного взыскания ЮЛ_33.8");
            }

            if (freqCode != refPaymentFreqType.Item7 && periodSum == null)
            {
                errorData.AddError("periodSum", periodSum, "Отсутствует сумма периодического взыскания ЮЛ_33.11");
            }
            if (freqCode == refPaymentFreqType.Item7 && periodSum != null)
            {
                errorData.AddError("periodSum", periodSum, "Сумма периодического взыскания ЮЛ_33.11 должна отсутствовать");
            }

            if (calcDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("calcDate", calcDate, "Дата расчета ЮЛ_33.10 позднее даты и времени наступления события eventDateTime");
            }

            if (!CurrencyDictionary.Contains(currency))
            {
                errorData.AddError("currency", currency, "Валюта взыскания ЮЛ_33.13 не соответствует Общероссийскому классификатору валют");
            }

            if (debtBalance > debtSum)
            {
                errorData.AddError("debtBalance", debtBalance, "Остаток долга ЮЛ_33.15 больше суммы долга ЮЛ_33.14");
            }

            if (!CurrencyDictionary.Contains(debtCurrency))
            {
                errorData.AddError("debtCurrency", debtCurrency, "Валюта суммы долга ЮЛ_33.16 не соответствует Общероссийскому классификатору валют");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_33_Collection_Type block, IEventData eventData)
        {
            return [.. (block?.Check(eventData)).OrEmptyIfNull()];
        }
    }
}
