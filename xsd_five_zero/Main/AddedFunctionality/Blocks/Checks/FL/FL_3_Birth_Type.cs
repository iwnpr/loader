using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_3_Birth_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_3_Birth;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData? eventData)
        {
            ErrorData errorData = new(blockName, eventData);

            //if (!CountryCodeDictionary.Contains(countryCode))
            //{
            //    errorData.AddError("countryCode", countryCode, "Значение кода страны по ОКСМ ФЛ_3.2 не соответствует ОКСМ или «999»");
            //}
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_3_Birth_Type block, IEventData? eventData)
        {
            return [.. (block?.Check(eventData)).OrEmptyIfNull()];
        }
    }
}
