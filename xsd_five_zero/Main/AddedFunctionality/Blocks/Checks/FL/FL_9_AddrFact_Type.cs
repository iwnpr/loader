using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_9_AddrFact_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_9_AddrFact;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData)
        {
            ErrorData errorData = new(blockName, eventData);

            if (exist == true)
            {
                if (postCode != null && countryCode == "643" && postCode.Length != 6)
                {
                    errorData.AddError("postCode", postCode, "Длина почтового индекса ФЛ_9.2 не равна 6 цифрам");
                }
                if (!CountryCodeDictionary.Contains(countryCode))
                {
                    errorData.AddError("countryCode", countryCode, "Значение кода страны по ОКСМ ФЛ_9.3 не соответствует ОКСМ или «999»");
                }
                if (countryCode != "999" && countryOther != null)
                {
                    errorData.AddError("countryOther", countryOther, "Наименование иной страны ФЛ_9.4 должно отсутствовать");
                }
                if (countryCode == "643" && locationCode == null)
                {
                    errorData.AddError("locationCode", locationCode, "Отсутствует код населенного пункта по ОКАТО ФЛ_9.6");
                }
                if (locationCode == "99999999999" && locationOther == null)
                {
                    errorData.AddError("locationOther", locationOther, "Отсутствует иной населенный пункт ФЛ_9.7");
                }
                if (locationCode != "99999999999" && locationOther != null)
                {
                    errorData.AddError("locationOther", locationOther, "Иной населенный пункт ФЛ_9.7 должен отсутствовать");
                }
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_9_AddrFact_Type block, IEventData eventData)
        {
            return [.. (block?.Check(eventData)).OrEmptyIfNull()];
        }
    }
}
