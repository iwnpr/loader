using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_55_57_Group_Type : Block_Type
    {
        public FL_55_57_Group_Type()
        {
            blockName = refBlockNameType.FL_55_57_Group;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            #region FL_55_Application
            if (FL_55_Application.currency != null && !CurrencyDictionary.Contains(FL_55_Application.currency))
            {
                AddError("currency", FL_55_Application.currency, "Запрошенная валюта обязательства ФЛ_55.3 не соответствует Общероссийскому классификатору валют");
            }

            if (!UidHelper.CheckUid(FL_55_Application.uid))
            {
                AddError("uid", FL_55_Application.uid, "Контрольный символ УИДа обращения ФЛ_55.4 не верный");
            }

            if (FL_55_Application.applicationDate.DateGreater(eventData?.eventDate))
            {
                AddError("applicationDate", FL_55_Application.applicationDate, "Дата обращения ФЛ_55.5 позднее даты наступления события eventDate");
            }

            if ((eventData.EventName.In(refEventNameType.FL_Event_1_4, refEventNameType.FL_Event_1_4_1, refEventNameType.FL_Event_2_2, refEventNameType.FL_Event_2_3)
                    || (eventData.EventName == refEventNameType.FL_Event_1_2 && FL_55_Application.stageCode == 2))
                && !FL_55_Application.approvalEndDateSpecified)
            {
                AddError("approvalEndDate", FL_55_Application.approvalEndDate, "Отсутствует дата окончания действия инвестиционного предложения, одобрения обращения (оферты кредитора) ФЛ_55.8");
            }

            if (eventData.EventName.In(refEventNameType.FL_Event_1_1, refEventNameType.FL_Event_1_3)
               && FL_55_Application.approvalEndDateSpecified)
            {
                AddError("approvalEndDate", FL_55_Application.approvalEndDate, "Дата окончания действия инвестиционного предложения, одобрения обращения (оферты кредитора) ФЛ_55.8 должна отсутствовать");
            }
            if (FL_55_Application.approvalEndDateSpecified && FL_55_Application.approvalEndDate.DateLess(FL_55_Application.applicationDate))
            {
                AddError("approvalEndDate", FL_55_Application.approvalEndDate, "Дата окончания действия инвестиционного предложения, одобрения обращения (оферты кредитора) ФЛ_55.8 раньше Даты обращения ФЛ_55.5");
            }

            if ((eventData.EventName == refEventNameType.FL_Event_1_1
               || (eventData.EventName == refEventNameType.FL_Event_1_2 && FL_55_Application.stageCode == 1))
                   && !FL_55_Application.stageEndDateSpecified)
            {
                AddError("stageEndDate", FL_55_Application.stageEndDate, "Отсутствует дата окончания срока рассмотрения обращения ФЛ_55.9");
            }
            if ((eventData.EventName.In(refEventNameType.FL_Event_1_3, refEventNameType.FL_Event_1_4, refEventNameType.FL_Event_1_4_1, refEventNameType.FL_Event_2_2, refEventNameType.FL_Event_2_3)
             || (eventData.EventName == refEventNameType.FL_Event_1_2 && FL_55_Application.stageCode == 4))
                 && FL_55_Application.stageEndDateSpecified)
            {
                AddError("stageEndDate", FL_55_Application.stageEndDate, "Дата окончания срока рассмотрения обращения ФЛ_55.9 должна отсутствовать");
            }
            if (FL_55_Application.stageEndDateSpecified && FL_55_Application.stageEndDate.DateLess(FL_55_Application.applicationDate))
            {
                AddError("stageEndDate", FL_55_Application.stageEndDate, "Дата окончания срока рассмотрения обращения ФЛ_55.9 раньше даты обращения ФЛ_55.5");
            }


            if (FL_55_Application.role == 1 && FL_55_Application.purposeCode == null)
            {
                AddError("purposeCode", null, "Отсутствует код цели запрошенного займа (кредита) ФЛ_55.10");
            }
            if (FL_55_Application.role != 1 && FL_55_Application.purposeCode != null)
            {
                foreach (var purposeCode in FL_55_Application.purposeCode)
                    AddError("purposeCode", purposeCode, "Код цели запрошенного займа (кредита) ФЛ_55.10 должен отсутствовать");
            }

            if (eventData.EventName == refEventNameType.FL_Event_1_1 && FL_55_Application.stageCode != 1)
            {
                AddError("stageCode", FL_55_Application.stageCode, "Код стадии рассмотрения обращения ФЛ_55.11 должен быть равен «1»");
            }

            if (eventData.EventName == refEventNameType.FL_Event_1_3 && FL_55_Application.stageCode != 3)
            {
                AddError("stageCode", FL_55_Application.stageCode, "Код стадии рассмотрения обращения ФЛ_55.11 должен быть равен «3»");
            }

            if (eventData.EventName.In(refEventNameType.FL_Event_1_4, refEventNameType.FL_Event_1_4_1, refEventNameType.FL_Event_2_2, refEventNameType.FL_Event_2_3) && FL_55_Application.stageCode != 5)
            {
                AddError("stageCode", FL_55_Application.stageCode, "Код стадии рассмотрения обращения ФЛ_55.11 должен быть равен «5»");
            }

            if (eventData.EventName == refEventNameType.FL_Event_1_2
                && (FL_55_Application.approvalEndDateSpecified || !FL_55_Application.stageEndDateSpecified || FL_55_Application.stageCode != 1)
                 && (FL_55_Application.approvalEndDateSpecified || FL_55_Application.stageEndDateSpecified || FL_55_Application.stageCode != 4)
                 && (!FL_55_Application.approvalEndDateSpecified || FL_55_Application.stageCode != 2)
                 && FL_55_Application.stageCode != 5)
            {
                AddError("stageCode", FL_55_Application.stageCode, "Неверно указано значение кода стадии рассмотрения обращения ФЛ_55.11");
            }


            if (FL_55_Application.stageDate.DateGreater(eventData?.eventDate))
            {
                AddError("stageDate", FL_55_Application.stageDate, "Дата перехода обращения в текущую стадию рассмотрения ФЛ_55.12 позднее даты наступления события eventDate");
            }

            if (!CurrencyDictionary.Contains(FL_55_Application.loanCurrency))
            {
                AddError("loanCurrency", FL_55_Application.loanCurrency, "Валюта одобренного займа (кредита), лизинга или обеспечения ФЛ_55.16 не соответствует Общероссийскому классификатору валют");
            }
            #endregion

            #region FL_57_Reject
            if (FL_57_Reject != null)
            {
                if (FL_57_Reject.rejectDate < FL_55_Application.applicationDate)
                {
                    AddError("rejectDate", FL_57_Reject.rejectDate, "Дата отказа ФЛ_57.1 раньше даты обращения ФЛ_55.5");
                }
                if (FL_57_Reject.rejectDate.DateGreater(eventData?.eventDate))
                {
                    AddError("rejectDate", FL_57_Reject.rejectDate, "Дата отказа ФЛ_57.1 позднее даты наступления события eventDate");
                }
            }
            #endregion

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_55_57_Group_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
