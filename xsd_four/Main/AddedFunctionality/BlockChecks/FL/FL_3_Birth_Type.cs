using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_3_Birth_Type : Block_Type
    {
        public FL_3_Birth_Type()
        {
            blockName = refBlockNameType.FL_3_Birth;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            if (!CountryCodeDictionary.Contains(countryCode))
            {
                AddError("countryCode", countryCode, "Значение кода страны по ОКСМ ФЛ_3.2 не соответствует ОКСМ или «999»");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_3_Birth_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
