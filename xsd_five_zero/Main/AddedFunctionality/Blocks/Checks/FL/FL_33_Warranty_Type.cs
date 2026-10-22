using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_33_Warranty_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_33_Warranty;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData)
        {
            ErrorData errorData = new(blockName, eventData);

            if (warrantyFact == true)
            {
                if (!UidHelper.CheckUid(uid))
                {
                    errorData.AddError("uid", uid, "Неверный контрольный символ УИда договора поручительства ФЛ_33.2");
                }
                if (!CurrencyDictionary.Contains(currency))
                {
                    errorData.AddError("currency", currency, "Валюта поручительства ФЛ_33.4 не соответствует  Общероссийскому классификатору валют");
                }
                if (openDate.DateGreater(eventData.eventDateTimeLocal))
                {

                    errorData.AddError("openDate", openDate, "Дата заключения договора поручительства ФЛ_33.5 позднее даты и времени наступления события eventDateTime");
                }
                if (endDate.DateLess(openDate))
                {
                    errorData.AddError("endDate", endDate, "Дата прекращения поручительства согласно договору ФЛ_33.6 раньше даты заключения договора поручительства ФЛ_33.5");
                }

                if (factEndDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("factEndDate", factEndDate, "Дата фактического прекращения поручительства ФЛ_33.7 позднее даты и времени наступления события eventDateTime");
                }
                if (factEndDate.DateLess(openDate))
                {
                    errorData.AddError("factEndDate", factEndDate, "Дата фактического прекращения поручительства ФЛ_33.7 раньше даты заключения договора поручительства ФЛ_33.5");
                }
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_33_Warranty_Type block, IEventData eventData)
        {
            return [.. (block?.Check(eventData)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_33_Warranty_Type> blocks, IEventData eventData)
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
