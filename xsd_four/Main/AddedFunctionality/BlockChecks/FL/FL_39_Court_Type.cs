using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_39_Court_Type : Block_Type
    {
        public FL_39_Court_Type()
        {
            blockName = refBlockNameType.FL_39_Court;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (date.DateGreater(eventData?.eventDate))
            {
                AddError("openDate", date, "Дата принятия судебного акта ФЛ_39.3 позднее даты наступления события eventDate");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                AddError("openDate", date, "Дата принятия судебного акта ФЛ_39.3 раньше даты совершения сделки ФЛ_17.4");
            }

            if (!CurrencyDictionary.Contains(currency))
            {
                AddError("currency", currency, "Валюта суммы требований, подлежащих удовлетворению ФЛ_39.10 не соответствует  Общероссийскому классификатору валют");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_39_Court_Type block, EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}
