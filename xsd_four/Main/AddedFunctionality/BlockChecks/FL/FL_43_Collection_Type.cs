using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_43_Collection_Type : Block_Type
    {
        public FL_43_Collection_Type()
        {
            blockName = refBlockNameType.FL_43_Collection;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            if (actDate.DateGreater(eventData?.eventDate))
            {
                AddError("actDate", actDate, "Дата принятия судебного акта ФЛ_43.4 позднее даты наступления события eventDate");
            }

            if (endDate.DateGreater(eventData?.eventDate))
            {
                AddError("endDate", endDate, "Дата прекращения взыскания ФЛ_43.7 позднее даты наступления события eventDate");

            }
            if (endDate.DateLess(actDate))
            {
                AddError("endDate", endDate, "Дата прекращения взыскания ФЛ_43.7 раньше даты принятия судебного акта ФЛ_43.4");
            }

            if (freqCode == 7 && onceSum == null)
            {
                AddError("onceSum", onceSum, "Отсутствует сумма единовременного взыскания ФЛ_43.8");
            }

            if (freqCode != 7 && periodSum == null)
            {
                AddError("periodSum", periodSum, "Отсутствует сумма периодического взыскания ФЛ_43.11");
            }
            if (freqCode == 7 && periodSum != null)
            {
                AddError("periodSum", periodSum, "Сумма периодического взыскания ФЛ_43.11 должна отсутствовать");
            }

            if (calcDate.DateGreater(eventData?.eventDate))
            {
                AddError("calcDate", calcDate, "Дата расчета ФЛ_43.10 позднее даты наступления события eventDate");
            }

            if (!CurrencyDictionary.Contains(currency))
            {
                AddError("currency", currency, "Валюта взыскания ФЛ_43.13 не соответствует Общероссийскому классификатору валют");
            }

            if (debtBalance > debtSum)
            {
                AddError("debtBalance", debtBalance, "Остаток долга ФЛ_43.15 больше суммы долга ФЛ_43.14");
            }

            if (!CurrencyDictionary.Contains(debtCurrency))
            {
                AddError("debtCurrency", debtCurrency, "Валюта суммы долга ФЛ_43.16 не соответствует Общероссийскому классификатору валют");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_43_Collection_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
