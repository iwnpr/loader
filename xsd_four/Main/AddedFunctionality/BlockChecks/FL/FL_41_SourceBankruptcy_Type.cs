using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_41_SourceBankruptcy_Type : Block_Type
    {
        public FL_41_SourceBankruptcy_Type()
        {
            blockName = refBlockNameType.FL_41_SourceBankruptcy;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (startDate.DateGreater(eventData?.eventDate))
            {
                AddError("startDate", startDate, "Дата открытия конкурсного производства ФЛ_41.2 позднее даты наступления события eventDate");
            }
            if (startDate.DateLess(dealUidBlock?.openDate))
            {
                AddError("startDate", startDate, "Дата открытия конкурсного производства ФЛ_41.2 раньше даты совершения сделки ФЛ_17.4");
            }
            if (endDate.DateLess(dealUidBlock?.openDate))
            {
                AddError("endDate", endDate, "Дата завершения конкурсного производства ФЛ_41.4 раньше даты открытия конкурсного производства ФЛ_41.2");
            }
            if (endDate.DateGreater(eventData?.eventDate))
            {
                AddError("endDate", endDate, "Дата завершения конкурсного производства ФЛ_41.4 позднее даты наступления события eventDate");
            }
            if (lastPayDate.DateGreater(eventData?.eventDate))
            {
                AddError("lastPayDate", lastPayDate, "Дата завершения конкурсного производства ФЛ_41.4 позднее даты наступления события eventDate");
            }
            if (!CurrencyDictionary.Contains(currency))
            {
                AddError("currency", currency, "Валюта задолженности ФЛ_41.10 не соответствует Общероссийскому классификатору валют");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_41_SourceBankruptcy_Type block, EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}