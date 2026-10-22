using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_30_Court_Type : Block_Type
    {
        public UL_30_Court_Type()
        {
            blockName = refBlockNameType.UL_30_Court;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IUL_10_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (date.DateGreater(eventData?.eventDate))
            {
                AddError("openDate", date, "Дата принятия судебного акта ЮЛ_30.3 позднее даты наступления события eventDate");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                AddError("openDate", date, "Дата принятия судебного акта ЮЛ_30.3 раньше даты совершения сделки ЮЛ_10.4");
            }

            if (!CurrencyDictionary.Contains(currency))
            {
                AddError("currency", currency, "Валюта суммы требований, подлежащих удовлетворению ЮЛ_30.10 не соответствует  Общероссийскому классификатору валют");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_30_Court_Type block, EventData eventData, IUL_10_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}

