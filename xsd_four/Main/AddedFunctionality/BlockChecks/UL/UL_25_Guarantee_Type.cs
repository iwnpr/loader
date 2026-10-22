using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_25_Guarantee_Type : Block_Type
    {
        public UL_25_Guarantee_Type()
        {
            blockName = refBlockNameType.UL_25_Guarantee;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            if (exist == true)
            {
                if (!UidHelper.CheckUid(uid))
                {
                    AddError("uid", uid, "Неверный контрольный символ УИда независимой гарантии ЮЛ_25.2");
                }
                if (!CurrencyDictionary.Contains(currency))
                {
                    AddError("currency", currency, "Валюта независимой гарантии ЮЛ_25.4 не соответствует  Общероссийскому классификатору валют");
                }
                if (openDate.DateGreater(eventData?.eventDate))
                {
                    AddError("openDate", openDate, "Дата выдачи независимой гарантии ЮЛ_25.5 позднее даты наступления события eventDate");
                }
                if (endDate.DateLess(openDate))
                {
                    AddError("endDate", endDate, "Дата окончания независимой гарантии согласно ее условиям ЮЛ_25.6 раньше даты выдачи независимой гарантии ЮЛ_25.5");
                }
                if (factEndDate.DateGreater(eventData?.eventDate))
                {
                    AddError("factEndDate", factEndDate, "Дата фактического прекращения независимой гарантии ЮЛ_25.7 позднее даты наступления события eventDate");
                }
                if (factEndDate.DateLess(openDate))
                {
                    AddError("factEndDate", factEndDate, "Дата фактического прекращения независимой гарантии ЮЛ_25.7 раньше даты выдачи независимой гарантии ЮЛ_25.5");
                }
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_25_Guarantee_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
