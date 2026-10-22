using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_2_Address_Type : Block_Type
    {
        public UL_2_Address_Type()
        {
            blockName = refBlockNameType.UL_2_Address;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            if (!CountryCodeDictionary.Contains(countryCode))
            {
                AddError("countryCode", countryCode, "Значение кода страны по ОКСМ ЮЛ_2.1 не соответствует ОКСМ или «999»");
            }
            if (countryCode != "643" && locationCode != null)
            {
                AddError("locationCode", locationCode, "Код населенного пункта по ОКАТО ЮЛ_2.4 должен отсутствовать");
            }
            if (countryCode == "643" && locationCode == null)
            {
                AddError("locationCode", locationCode, "Отсутствует код населенного пункта по ОКАТО ЮЛ_2.4");
            }
            if (locationCode != "99999999999" && locationOther != null)
            {
                AddError("locationOther", locationOther, "Отсутствует иной населенный пункт ЮЛ_2.5");
            }
            if (locationCode == "99999999999" && locationOther == null)
            {
                AddError("locationOther", locationOther, "Иной населенный пункт ЮЛ_2.5 должен отсутствовать");
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_2_Address_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
