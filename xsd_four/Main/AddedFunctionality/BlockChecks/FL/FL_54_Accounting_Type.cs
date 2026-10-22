using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_54_Accounting_Type : Block_Type
    {
        public FL_54_Accounting_Type()
        {
            blockName = refBlockNameType.FL_54_Accounting;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, FL_18_Deal_Type dealBlock = null)
        {
            InitCheck(eventData);

            if (exist == false && !CurrencyDictionary.Contains(currency))
            {
                AddError("currency", currency, "Валюта суммы задолженности, учтенной на внебалансовых счетах ФЛ_54.8 не соответствует Общероссийскому классификатору валют");
            }
            if (minInterest > maxInterest)
            {
                AddError("minInterest", minInterest, "Минимальная процентная ставка ФЛ_54.2 больше максимальной процентной ставки ФЛ_54.6");
            }

            if (minInterest > maxInterest)
            {
                AddError("maxInterest", maxInterest, "Максимальная процентная ставка ФЛ_54.6 меньше минимальной процентной ставки ФЛ_54.2");
            }

            if (calcDate.DateGreater(eventData?.eventDate))
            {
                AddError("calcDate", calcDate, "Дата расчета ФЛ_54.7 позднее даты наступления события eventDate");
            }
            if (calcDate.DateLess(dealBlock?.startDate))
            {
                AddError("calcDate", calcDate, "Дата расчета ФЛ_54.7 раньше даты возникновения обязательства субъекта ФЛ_18.19");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_54_Accounting_Type block, EventData eventData, FL_18_Deal_Type dealBlock = null)
        {
            return (block?.Check(eventData, dealBlock)).OrEmptyIfNull().ToList();
        }
    }
}
