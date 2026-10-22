using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_11_Deal_Type : Block_Type
    {
        public UL_11_Deal_Type()
        {
            blockName = refBlockNameType.UL_11_Deal;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, UL_10_DealUid_Type? dealUidBlock = null)
        {
            InitCheck(eventData);

            if ((eventData.EventName == refEventNameType.UL_Event_2_2_1 && code.In(1, 2))
               || (role == 1 && !code.In(1, 99))
               || (role == 2 && !code.In(9, 10, 11, 12, 13, 99))
               || (role == 3 && !code.In(6, 7, 8, 99))
               || (role == 4 && !code.In(2, 99)))
            {
                AddError("code", code, "Неверное значение кода типа сделки ЮЛ_11.3");
            }

            if (code == 1 && !kindCodeSpecified)
            {
                AddError("kindCode", kindCode, "Отсутствует код вида займа (кредита) ЮЛ_11.4");
            }
            if (code != 1 && kindCodeSpecified)
            {
                AddError("kindCode", kindCode, "Код вида займа (кредита) ЮЛ_11.4 должен отсутствовать");
            }
            if (code == 1 && purposeCode.OrEmptyIfNull().Count() == 0)
            {
                AddError("purposeCode", purposeCode, "Отсутствует код цели займа (кредита) ЮЛ_11.5");
            }
            if (code != 1 && purposeCode.OrEmptyIfNull().Count() != 0)
            {
                foreach (var purposeCodeValue in purposeCode)
                {
                    AddError("purposeCode", purposeCodeValue, "Код цели займа (кредита)  ЮЛ_11.5 должен отсутствовать");
                }
            }
            if (endDate.DateLess(dealUidBlock?.openDate))
            {
                AddError("endDate", endDate, "Дата прекращения обязательства субъекта по условиям сделки ЮЛ_11.10 раньше даты совершения сделки ЮЛ_10.4");
            }
            if (!UidHelper.CheckUid(transferUid))
            {
                AddError("transferUid", transferUid, "Неверный контрольный символ УИда сделки, по которой права кредитора частично переданы другому лицу ЮЛ_11.16");
            }
            if (startDate.DateLess(dealUidBlock?.openDate))
            {
                AddError("startDate", startDate, "Дата возникновения обязательства субъекта ЮЛ_11.17 раньше даты совершения сделки ЮЛ_10.4");
            }
            if (startDate.DateGreater(eventData?.eventDate))
            {
                AddError("startDate", startDate, "Дата возникновения обязательства субъекта ЮЛ_11.17 позднее даты наступления события eventDate");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_11_Deal_Type block, EventData eventData, UL_10_DealUid_Type? dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}