using loader_utils;

namespace Xsd.VersionFour.Main
{
    public partial class UL_32_SourceLiquidation_Type : Block_Type
    {
        public UL_32_SourceLiquidation_Type()
        {
            blockName = refBlockNameType.UL_32_SourceLiquidation;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IUL_10_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (startDate != DateTime.MinValue)
            {
                if (startDate.DateGreater(eventData?.eventDate))
                {
                    AddError("startDate", startDate, "Дата открытия конкурсного производства ЮЛ_32.2 позднее даты наступления события eventDate");
                }
                if (startDate.DateLess(dealUidBlock?.openDate))
                {
                    AddError("startDate", startDate, "Дата открытия конкурсного производства ЮЛ_32.2 раньше даты совершения сделки ЮЛ_10.4");
                }
            }

            if (endDate != DateTime.MinValue)
            {
                if (endDate.DateGreater(eventData?.eventDate))
                {
                    AddError("endDate", endDate, "Дата завершения конкурсного производства ЮЛ_32.4 позднее даты наступления события eventDate");
                }
                if (endDate.DateLess(dealUidBlock?.openDate))
                {
                    AddError("endDate", endDate, "Дата завершения конкурсного производства ЮЛ_32.4 раньше даты совершения сделки ЮЛ_10.4");
                }
                if (endDate.DateLess(startDate))
                {
                    AddError("endDate", endDate, "Дата завершения конкурсного производства ЮЛ_32.4 раньше даты открытия конкурсного производства ЮЛ_32.2");
                }
            }

            if (lastPayDate.DateGreater(eventData?.eventDate))
            {
                AddError("lastPayDate", lastPayDate, "Дата последнего внесенного платежа ЮЛ_32.6 позднее даты наступления события eventDate");
            }

            if (!CurrencyDictionary.Contains(currency))
            {
                AddError("currency", currency, "Валюта задолженности ЮЛ_32.10 не соответствует Общероссийскому классификатору валют");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_32_SourceLiquidation_Type block, EventData eventData, IUL_10_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}
