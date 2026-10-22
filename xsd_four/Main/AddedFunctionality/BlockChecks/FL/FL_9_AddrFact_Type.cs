using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_9_AddrFact_Type : Block_Type
    {
        public FL_9_AddrFact_Type()
        {
            blockName = refBlockNameType.FL_9_AddrFact;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            if (exist == true)
            {
                if (postCode != null && countryCode == "643" && postCode.Length != 6)
                {
                    AddError("postCode", postCode, "Длина почтового индекса ФЛ_9.2 не равна 6 цифрам");
                }
                if (!CountryCodeDictionary.Contains(countryCode))
                {
                    AddError("countryCode", countryCode, "Значение кода страны по ОКСМ ФЛ_9.3 не соответствует ОКСМ или «999»");
                }
                if (countryCode == "643" && locationCode == null)
                {
                    AddError("locationCode", locationCode, "Отсутствует код населенного пункта по ОКАТО ФЛ_9.6");
                }
                if (countryCode != "643" && locationCode != null)
                {
                    AddError("locationCode", locationCode, "Код населенного пункта по ОКАТО ФЛ_9.6 должен отсутствовать");
                }
                if (locationCode == "99999999999" && locationOther == null)
                {
                    AddError("locationOther", locationOther, "Отсутствует иной населенный пункт ФЛ_9.7");
                }
                if (locationCode != "99999999999" && locationOther != null)
                {
                    AddError("locationOther", locationOther, "Иной населенный пункт ФЛ_9.7 должен отсутствовать");
                }
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_9_AddrFact_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
