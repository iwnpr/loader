using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_34_Guarantee_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_34_Guarantee;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData)
        {
            ErrorData errorData = new(blockName, eventData);

            if (exist == true)
            {
                if (!UidHelper.CheckUid(uid))
                {
                    errorData.AddError("uid", uid, "Неверный контрольный символ УИда независимой гарантии ФЛ_34.2");
                }
                if (!CurrencyDictionary.Contains(currency))
                {
                    errorData.AddError("currency", currency, "Валюта независимой гарантии ФЛ_34.4 не соответствует  Общероссийскому классификатору валют");
                }
                if (openDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("openDate", openDate, "Дата выдачи независимой гарантии ФЛ_34.5 позднее даты и времени наступления события eventDateTime");
                }
                if (endDate.DateLess(openDate))
                {
                    errorData.AddError("endDate", endDate, "Дата окончания независимой гарантии согласно ее условиям ФЛ_34.6 раньше даты выдачи независимой гарантии ФЛ_34.5");
                }
                if (factEndDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("factEndDate", factEndDate, "Дата фактического прекращения независимой гарантии ФЛ_34.7 позднее даты и времени наступления события eventDateTime");
                }
                if (factEndDate.DateLess(openDate))
                {
                    errorData.AddError("factEndDate", factEndDate, "Дата фактического прекращения независимой гарантии ФЛ_34.7 раньше даты выдачи независимой гарантии ФЛ_34.5");
                }
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_34_Guarantee_Type block, IEventData eventData)
        {
            return [.. (block?.Check(eventData)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_34_Guarantee_Type> blocks, IEventData eventData)
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
