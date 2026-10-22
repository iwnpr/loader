using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_39_Court_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_39_Court;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (date.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("openDate", date, "Дата принятия судебного акта ФЛ_39.3 позднее даты и времени наступления события eventDateTime");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("openDate", date, "Дата принятия судебного акта ФЛ_39.3 раньше даты совершения сделки ФЛ_17.4");
            }

            if (!CurrencyDictionary.Contains(currency))
            {
                errorData.AddError("currency", currency, "Валюта суммы требований, подлежащих удовлетворению ФЛ_39.10 не соответствует  Общероссийскому классификатору валют");
            }

            if (rightLimitExist != null)
            {
                errorData.AddError("rightLimitExist_0/rightLimitExist_1", null, "Признак ограничения права кредитора требовать исполнения заемщиком обязательств ФЛ_39.11 должен отсутствовать (вводится в действие с 01.07.2026)");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_39_Court_Type block, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_39_Court_Type> blocks, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            List<INotificationOfAcceptanceError> errorList = [];
            foreach (var block in blocks.OrEmptyIfNull())
            {
                errorList.AddRange(block.Check(eventData, dealUidBlock));
            }
            return errorList;
        }
    }
}
