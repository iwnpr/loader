using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_42_SourceLiquidation_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_42_SourceLiquidation;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (startDate != DateTime.MinValue)
            {
                if (startDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("startDate", startDate, "Дата открытия конкурсного производства ФЛ_42.2 позднее даты и времени наступления события eventDateTime");
                }
                if (startDate.DateLess(dealUidBlock?.openDate))
                {
                    errorData.AddError("startDate", startDate, "Дата открытия конкурсного производства ФЛ_42.2 раньше даты совершения сделки ФЛ_17.4");
                }
            }

            if (endDate != DateTime.MinValue)
            {
                if (endDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("endDate", endDate, "Дата завершения конкурсного производства ФЛ_42.4 позднее даты и времени наступления события eventDateTime");
                }
                if (endDate.DateLess(dealUidBlock?.openDate))
                {
                    errorData.AddError("endDate", endDate, "Дата завершения конкурсного производства ФЛ_42.4 раньше даты совершения сделки ФЛ_17.4");
                }
                if (endDate.DateLess(startDate))
                {
                    errorData.AddError("endDate", endDate, "Дата завершения конкурсного производства ФЛ_42.4 раньше даты открытия конкурсного производства ФЛ_42.2");
                }
            }

            if (lastPayDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("lastPayDate", lastPayDate, "Дата последнего внесенного платежа ФЛ_42.6 позднее даты и времени наступления события eventDateTime");
            }

            if (!CurrencyDictionary.Contains(currency))
            {
                errorData.AddError("currency", currency, "Валюта задолженности ФЛ_42.10 не соответствует Общероссийскому классификатору валют");
            }
            if (transferDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("transferDate", transferDate, "Дата перехода прав кредитора к другому лицу или прекращения обязательства ФЛ_42.12 позднее даты и времени наступления события eventDateTime");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_42_SourceLiquidation_Type block, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }
    }
}
