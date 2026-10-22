using loader_utils;

namespace Xsd.VersionFour.Main
{
    public partial class FL_42_SourceLiquidation_Type : Block_Type
    {
        public FL_42_SourceLiquidation_Type()
        {
            blockName = refBlockNameType.FL_42_SourceLiquidation;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (startDate != DateTime.MinValue)
            {
                if (startDate.DateGreater(eventData?.eventDate))
                {
                    AddError("startDate", startDate, "Дата открытия конкурсного производства ФЛ_42.2 позднее даты наступления события eventDate");
                }
                if (startDate.DateLess(dealUidBlock?.openDate))
                {
                    AddError("startDate", startDate, "Дата открытия конкурсного производства ФЛ_42.2 раньше даты совершения сделки ФЛ_17.4");
                }
            }

            if (endDate != DateTime.MinValue)
            {
                if (endDate.DateGreater(eventData?.eventDate))
                {
                    AddError("endDate", endDate, "Дата завершения конкурсного производства ФЛ_42.4 позднее даты наступления события eventDate");
                }
                if (endDate.DateLess(dealUidBlock?.openDate))
                {
                    AddError("endDate", endDate, "Дата завершения конкурсного производства ФЛ_42.4 раньше даты совершения сделки ФЛ_17.4");
                }
                if (endDate.DateLess(startDate))
                {
                    AddError("endDate", endDate, "Дата завершения конкурсного производства ФЛ_42.4 раньше даты открытия конкурсного производства ФЛ_42.2");
                }
            }

            if (lastPayDate.DateGreater(eventData?.eventDate))
            {
                AddError("lastPayDate", lastPayDate, "Дата последнего внесенного платежа ФЛ_42.6 позднее даты наступления события eventDate");
            }

            if (!CurrencyDictionary.Contains(currency))
            {
                AddError("currency", currency, "Валюта задолженности ФЛ_42.10 не соответствует Общероссийскому классификатору валют");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_42_SourceLiquidation_Type block, EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}
