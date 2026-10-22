using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_36_ProvisionPayment_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_36_ProvisionPayment;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData)
        {
            ErrorData errorData = new(blockName, eventData);

            if (exist == true)
            {
                if (date.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("openDate", date, "Дата погашения требований за счет обеспечения ФЛ_36.3 позднее даты и времени наступления события eventDateTime");
                }
                if (!CurrencyDictionary.Contains(currency))
                {
                    errorData.AddError("currency", currency, "Валюта суммы требований, погашенных за счет обеспечения ФЛ_36.5 не соответствует  Общероссийскому классификатору валют");
                }
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_36_ProvisionPayment_Type block, IEventData eventData)
        {
            return [.. (block?.Check(eventData)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_36_ProvisionPayment_Type> blocks, IEventData eventData)
        {
            List<INotificationOfAcceptanceError> errorList = [];
            foreach (var block in blocks.OrEmptyIfNull())
            {
                errorList.AddRange(block.Check(eventData));
            }
            return errorList;
        }
    }
}
