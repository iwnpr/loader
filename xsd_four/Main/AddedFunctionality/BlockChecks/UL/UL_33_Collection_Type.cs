using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_33_Collection_Type : Block_Type
    {
        public UL_33_Collection_Type()
        {
            blockName = refBlockNameType.UL_33_Collection;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            if (actDate.DateGreater(eventData?.eventDate))
            {
                AddError("actDate", actDate, "Дата принятия судебного акта ЮЛ_33.4 позднее даты наступления события eventDate");
            }

            if (endDate.DateGreater(eventData?.eventDate))
            {
                AddError("endDate", endDate, "Дата прекращения взыскания ЮЛ_33.7 позднее даты наступления события eventDate");

            }
            if (endDate.DateLess(actDate))
            {
                AddError("endDate", endDate, "Дата прекращения взыскания ЮЛ_33.7 раньше даты принятия судебного акта ЮЛ_33.4");
            }

            if (freqCode == 7 && onceSum == null)
            {
                AddError("onceSum", onceSum, "Отсутствует сумма единовременного взыскания ЮЛ_33.8");
            }

            if (freqCode != 7 && periodSum == null)
            {
                AddError("periodSum", periodSum, "Отсутствует сумма периодического взыскания ЮЛ_33.11");
            }
            if (freqCode == 7 && periodSum != null)
            {
                AddError("periodSum", periodSum, "Сумма периодического взыскания ЮЛ_33.11 должна отсутствовать");
            }

            if (calcDate.DateGreater(eventData?.eventDate))
            {
                AddError("calcDate", calcDate, "Дата расчета ЮЛ_33.10 позднее даты наступления события eventDate");
            }

            if (!CurrencyDictionary.Contains(currency))
            {
                AddError("currency", currency, "Валюта взыскания ЮЛ_33.13 не соответствует Общероссийскому классификатору валют");
            }

            if (debtBalance > debtSum)
            {
                AddError("debtBalance", debtBalance, "Остаток долга ЮЛ_33.15 больше суммы долга ЮЛ_33.14");
            }

            if (!CurrencyDictionary.Contains(debtCurrency))
            {
                AddError("debtCurrency", debtCurrency, "Валюта суммы долга ЮЛ_33.16 не соответствует Общероссийскому классификатору валют");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_33_Collection_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
