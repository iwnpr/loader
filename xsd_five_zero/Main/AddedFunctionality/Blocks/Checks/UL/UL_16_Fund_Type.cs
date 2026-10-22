using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_16_Fund_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_16_Fund;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (dateSpecified)
            {
                if (date.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("date", date, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ЮЛ_16.1 позднее даты и времени наступления события eventDateTime");
                }
                if (date.DateLess(dealUidBlock?.openDate))
                {
                    errorData.AddError("date", date, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ЮЛ_16.1 раньше даты совершения сделки ЮЛ_10.4");
                }
            }

            if (!CurrencyDictionary.Contains(currency))
            {
                errorData.AddError("currency", currency, "Валюта задолженности на дату передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ЮЛ_16.4 не соответствует Общероссийскому классификатору валют");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_16_Fund_Type block, IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<UL_16_Fund_Type> blocks, IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock = null)
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
