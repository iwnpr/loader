using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_2_Address_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_2_Address;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData? eventData)
        {
            ErrorData errorData = new(blockName, eventData);

            if (!CountryCodeDictionary.Contains(countryCode))
            {
                errorData.AddError("countryCode", countryCode, "Значение кода страны по ОКСМ ЮЛ_2.1 не соответствует ОКСМ или «999»");
            }
            if (countryCode != "999" && countryOther != null)
            {
                errorData.AddError("countryOther", countryOther, "Наименование иной страны ЮЛ_2.2 должно отсутствовать");
            }
            if (countryCode == "643" && locationCode == null)
            {
                errorData.AddError("locationCode", locationCode, "Отсутствует код населенного пункта по ОКАТО ЮЛ_2.4");
            }
            if (locationCode != "99999999999" && locationOther != null)
            {
                errorData.AddError("locationOther", locationOther, "Иной населенный пункт ЮЛ_2.5 должен отсутствовать");
            }
            if (locationCode == "99999999999" && locationOther == null)
            {
                errorData.AddError("locationOther", locationOther, "Отсутствует иной населенный пункт ЮЛ_2.5");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_2_Address_Type block, IEventData? eventData)
        {
            return [.. (block?.Check(eventData)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<UL_2_Address_Type> blocks, IEventData? eventData)
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
