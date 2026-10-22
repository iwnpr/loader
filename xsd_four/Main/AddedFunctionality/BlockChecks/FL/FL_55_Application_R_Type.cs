using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_55_Application_R_Type : Block_Type
    {
        public FL_55_Application_R_Type()
        {
            blockName = refBlockNameType.FL_55_Application_R;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            if (currency != null && !CurrencyDictionary.Contains(currency))
            {
                AddError("currency", currency, "Запрошенная валюта обязательства ФЛ_55.3 не соответствует Общероссийскому классификатору валют");
            }

            if (!UidHelper.CheckUid(this.uid))
            {
                AddError("uid", this.uid, "Контрольный символ УИДа обращения ФЛ_55.4 не верный");
            }

            if (applicationDate.DateGreater(eventData?.eventDate))
            {
                AddError("applicationDate", applicationDate, "Дата обращения ФЛ_55.5 позднее даты наступления события eventDate");
            }

            if ((eventData.EventName.In(refEventNameType.FL_Event_1_4, refEventNameType.FL_Event_1_4_1, refEventNameType.FL_Event_2_2, refEventNameType.FL_Event_2_3)
                    || (eventData.EventName == refEventNameType.FL_Event_1_2 && stageCode == 2))
                && !approvalEndDateSpecified)
            {
                AddError("approvalEndDate", approvalEndDate, "Отсутствует дата окончания действия инвестиционного предложения, одобрения обращения (оферты кредитора) ФЛ_55.8");
            }

            if (eventData.EventName.In(refEventNameType.FL_Event_1_1, refEventNameType.FL_Event_1_3)
               && approvalEndDateSpecified)
            {
                AddError("approvalEndDate", approvalEndDate, "Дата окончания действия инвестиционного предложения, одобрения обращения (оферты кредитора) ФЛ_55.8 должна отсутствовать");
            }
            if (approvalEndDateSpecified && approvalEndDate.DateLess(applicationDate))
            {
                AddError("approvalEndDate", approvalEndDate, "Дата окончания действия инвестиционного предложения, одобрения обращения (оферты кредитора) ФЛ_55.8 раньше Даты обращения ФЛ_55.5");
            }

            if ((eventData.EventName == refEventNameType.FL_Event_1_1
               || (eventData.EventName == refEventNameType.FL_Event_1_2 && stageCode == 1))
                   && !stageEndDateSpecified)
            {
                AddError("stageEndDate", stageEndDate, "Отсутствует дата окончания срока рассмотрения обращения ФЛ_55.9");
            }
            if ((eventData.EventName.In(refEventNameType.FL_Event_1_3, refEventNameType.FL_Event_1_4, refEventNameType.FL_Event_1_4_1, refEventNameType.FL_Event_2_2, refEventNameType.FL_Event_2_3)
             || (eventData.EventName == refEventNameType.FL_Event_1_2 && stageCode == 4))
                 && stageEndDateSpecified)
            {
                AddError("stageEndDate", stageEndDate, "Дата окончания срока рассмотрения обращения ФЛ_55.9 должна отсутствовать");
            }
            if (stageEndDateSpecified && stageEndDate.DateLess(applicationDate))
            {
                AddError("stageEndDate", stageEndDate, "Дата окончания срока рассмотрения обращения ФЛ_55.9 раньше даты обращения ФЛ_55.5");
            }


            if (role == 1 && purposeCode == null)
            {
                AddError("purposeCode", null, "Отсутствует код цели запрошенного займа (кредита) ФЛ_55.10");
            }
            if (role != 1 && purposeCode != null)
            {
                foreach (var purposeCode in purposeCode)
                    AddError("purposeCode", purposeCode, "Код цели запрошенного займа (кредита) ФЛ_55.10 должен отсутствовать");
            }

            if (eventData.EventName == refEventNameType.FL_Event_1_1 && stageCode != 1)
            {
                AddError("stageCode", stageCode, "Код стадии рассмотрения обращения ФЛ_55.11 должен быть равен «1»");
            }

            if (eventData.EventName == refEventNameType.FL_Event_1_3 && stageCode != 3)
            {
                AddError("stageCode", stageCode, "Код стадии рассмотрения обращения ФЛ_55.11 должен быть равен «3»");
            }

            if (eventData.EventName.In(refEventNameType.FL_Event_1_4, refEventNameType.FL_Event_1_4_1, refEventNameType.FL_Event_2_2, refEventNameType.FL_Event_2_3) && stageCode != 5)
            {
                AddError("stageCode", stageCode, "Код стадии рассмотрения обращения ФЛ_55.11 должен быть равен «5»");
            }

            if (eventData.EventName == refEventNameType.FL_Event_1_2
                && (approvalEndDateSpecified || !stageEndDateSpecified || stageCode != 1)
                 && (approvalEndDateSpecified || stageEndDateSpecified || stageCode != 4)
                 && (!approvalEndDateSpecified || stageCode != 2)
                 && stageCode != 5)
            {
                AddError("stageCode", stageCode, "Неверно указано значение кода стадии рассмотрения обращения ФЛ_55.11");
            }


            if (stageDate.DateGreater(eventData?.eventDate))
            {
                AddError("stageDate", stageDate, "Дата перехода обращения в текущую стадию рассмотрения ФЛ_55.12 позднее даты наступления события eventDate");
            }

            if (!CurrencyDictionary.Contains(loanCurrency))
            {
                AddError("loanCurrency", loanCurrency, "Валюта одобренного займа (кредита), лизинга или обеспечения ФЛ_55.16 не соответствует Общероссийскому классификатору валют");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_55_Application_R_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
