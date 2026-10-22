using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_32_SourceLiquidation_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_32_SourceLiquidation;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (startDate != DateTime.MinValue)
            {
                if (startDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("startDate", startDate, "Дата открытия конкурсного производства ЮЛ_32.2 позднее даты и времени наступления события eventDateTime");
                }
                if (startDate.DateLess(dealUidBlock?.openDate))
                {
                    errorData.AddError("startDate", startDate, "Дата открытия конкурсного производства ЮЛ_32.2 раньше даты совершения сделки ЮЛ_10.4");
                }
            }

            if (endDate != DateTime.MinValue)
            {
                if (endDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("endDate", endDate, "Дата завершения конкурсного производства ЮЛ_32.4 позднее даты и времени наступления события eventDateTime");
                }
                if (endDate.DateLess(dealUidBlock?.openDate))
                {
                    errorData.AddError("endDate", endDate, "Дата завершения конкурсного производства ЮЛ_32.4 раньше даты совершения сделки ЮЛ_10.4");
                }
                if (endDate.DateLess(startDate))
                {
                    errorData.AddError("endDate", endDate, "Дата завершения конкурсного производства ЮЛ_32.4 раньше даты открытия конкурсного производства ЮЛ_32.2");
                }
            }

            if (lastPayDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("lastPayDate", lastPayDate, "Дата последнего внесенного платежа ЮЛ_32.6 позднее даты и времени наступления события eventDateTime");
            }

            if (!CurrencyDictionary.Contains(currency))
            {
                errorData.AddError("currency", currency, "Валюта задолженности ЮЛ_32.10 не соответствует Общероссийскому классификатору валют");
            }
            if (transferDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("transferDate", transferDate, "Дата перехода прав кредитора к другому лицу или прекращения обязательства ЮЛ_32.12 позднее даты и времени наступления события eventDateTime");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_32_SourceLiquidation_Type block, IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }
    }
}
