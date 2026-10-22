using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_24_Warranty_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_24_Warranty;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData)
        {
            ErrorData errorData = new(blockName, eventData);

            if (warrantyFact == true)
            {
                if (!UidHelper.CheckUid(uid))
                {
                    errorData.AddError("uid", uid, "Неверный контрольный символ УИда договора поручительства ЮЛ_24.2");
                }
                if (!CurrencyDictionary.Contains(currency))
                {
                    errorData.AddError("currency", currency, "Валюта поручительства ЮЛ_24.4 не соответствует  Общероссийскому классификатору валют");
                }
                if (openDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("openDate", openDate, "Дата заключения договора поручительства ЮЛ_24.5 позднее даты и времени наступления события eventDateTime");
                }
                if (endDate.DateLess(openDate))
                {
                    errorData.AddError("endDate", endDate, "Дата прекращения поручительства согласно договору ЮЛ_24.6 раньше даты заключения договора поручительства ЮЛ_24.5");
                }

                if (factEndDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("factEndDate", factEndDate, "Дата фактического прекращения поручительства ЮЛ_24.7 позднее даты и времени наступления события eventDateTime");
                }
                if (factEndDate.DateLess(openDate))
                {
                    errorData.AddError("factEndDate", factEndDate, "Дата фактического прекращения поручительства ЮЛ_24.7 раньше даты заключения договора поручительства ЮЛ_24.5");
                }
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_24_Warranty_Type block, IEventData eventData)
        {
            return [.. (block?.Check(eventData)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<UL_24_Warranty_Type> blocks, IEventData eventData)
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
