using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_30_Court_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_30_Court;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (date.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("openDate", date, "Дата принятия судебного акта ЮЛ_30.3 позднее даты и времени наступления события eventDateTime");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("openDate", date, "Дата принятия судебного акта ЮЛ_30.3 раньше даты совершения сделки ЮЛ_10.4");
            }

            if (!CurrencyDictionary.Contains(currency))
            {
                errorData.AddError("currency", currency, "Валюта суммы требований, подлежащих удовлетворению ЮЛ_30.10 не соответствует  Общероссийскому классификатору валют");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_30_Court_Type block, IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<UL_30_Court_Type> blocks, IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock = null)
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

