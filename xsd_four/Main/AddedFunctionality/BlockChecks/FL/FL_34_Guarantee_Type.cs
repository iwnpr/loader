using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_34_Guarantee_Type : Block_Type
    {
        public FL_34_Guarantee_Type()
        {
            blockName = refBlockNameType.FL_34_Guarantee;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            if (exist == true)
            {
                if (!UidHelper.CheckUid(uid))
                {
                    AddError("uid", uid, "Неверный контрольный символ УИда независимой гарантии ФЛ_34.2");
                }
                if (!CurrencyDictionary.Contains(currency))
                {
                    AddError("currency", currency, "Валюта независимой гарантии ФЛ_34.4 не соответствует  Общероссийскому классификатору валют");
                }
                if (openDate.DateGreater(eventData?.eventDate))
                {
                    AddError("openDate", openDate, "Дата выдачи независимой гарантии ФЛ_34.5 позднее даты наступления события eventDate");
                }
                if (endDate.DateLess(openDate))
                {
                    AddError("endDate", endDate, "Дата окончания независимой гарантии согласно ее условиям ФЛ_34.6 раньше даты выдачи независимой гарантии ФЛ_34.5");
                }
                if (factEndDate.DateGreater(eventData?.eventDate))
                {
                    AddError("factEndDate", factEndDate, "Дата фактического прекращения независимой гарантии ФЛ_34.7 позднее даты наступления события eventDate");
                }
                if (factEndDate.DateLess(openDate))
                {
                    AddError("factEndDate", factEndDate, "Дата фактического прекращения независимой гарантии ФЛ_34.7 раньше даты выдачи независимой гарантии ФЛ_34.5");
                }
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_34_Guarantee_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
