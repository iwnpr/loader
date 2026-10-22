using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_24_Fund_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_24_Fund;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (date.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("date", date, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ФЛ_24.1 позднее даты и времени наступления события eventDateTime");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("date", date, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ФЛ_24.1 раньше даты совершения сделки ФЛ_17.4");
            }

            //if (num.IsNotNull() && decimal.Parse(num) < 1)
            //{
            //    errorData.AddError("num", num, "Порядковый номер транша ФЛ_24.2 меньше «1»");
            //}

            if (!CurrencyDictionary.Contains(currency))
            {
                errorData.AddError("currency", currency, "Валюта задолженности на дату передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ФЛ_24.4 не соответствует Общероссийскому классификатору валют");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_24_Fund_Type block, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_24_Fund_Type> blocks, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null)
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
