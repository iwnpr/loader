using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_36_1_ProvisionPaymentOffset_Type : Block_Type
    {
        public FL_36_1_ProvisionPaymentOffset_Type()
        {
            blockName = refBlockNameType.FL_36_1_ProvisionPaymentOffset;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            if (exist == true)
            {
                if (date.DateGreater(eventData?.eventDate))
                {
                    AddError("openDate", date, "Дата погашения требований за счет обеспечения ФЛ_36(1).3 позднее даты наступления события eventDate");
                }
                if (!CurrencyDictionary.Contains(currency))
                {
                    AddError("currency", currency, "Валюта суммы требований, погашенных за счет обеспечения ФЛ_36(1).5 не соответствует  Общероссийскому классификатору валют");
                }
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_36_1_ProvisionPaymentOffset_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
