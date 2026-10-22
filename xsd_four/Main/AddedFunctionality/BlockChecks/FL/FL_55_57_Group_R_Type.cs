using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_55_57_Group_R_Type : Block_Type
    {
        public FL_55_57_Group_R_Type()
        {
            blockName = refBlockNameType.FL_55_57_Group_R;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            #region FL_55_Application_R
            if (FL_55_Application_R.currency != null && !CurrencyDictionary.Contains(FL_55_Application_R.currency))
            {
                AddError("currency", FL_55_Application_R.currency, "Запрошенная валюта обязательства ФЛ_55.3 не соответствует Общероссийскому классификатору валют");
            }

            if (!UidHelper.CheckUid(FL_55_Application_R.uid))
            {
                AddError("uid", FL_55_Application_R.uid, "Контрольный символ УИДа обращения ФЛ_55.4 не верный");
            }

            if (FL_55_Application_R.applicationDate.DateGreater(eventData?.eventDate))
            {
                AddError("applicationDate", FL_55_Application_R.applicationDate, "Дата обращения ФЛ_55.5 позднее даты наступления события eventDate");
            }

            if ((eventData.EventName.In(refEventNameType.FL_Event_1_4, refEventNameType.FL_Event_1_4_1, refEventNameType.FL_Event_2_2, refEventNameType.FL_Event_2_3)
                    || (eventData.EventName == refEventNameType.FL_Event_1_2 && FL_55_Application_R.stageCode == 2))
                && !FL_55_Application_R.approvalEndDateSpecified)
            {
                AddError("approvalEndDate", FL_55_Application_R.approvalEndDate, "Отсутствует дата окончания действия инвестиционного предложения, одобрения обращения (оферты кредитора) ФЛ_55.8");
            }

            if (eventData.EventName.In(refEventNameType.FL_Event_1_1, refEventNameType.FL_Event_1_3)
               && FL_55_Application_R.approvalEndDateSpecified)
            {
                AddError("approvalEndDate", FL_55_Application_R.approvalEndDate, "Дата окончания действия инвестиционного предложения, одобрения обращения (оферты кредитора) ФЛ_55.8 должна отсутствовать");
            }
            if (FL_55_Application_R.approvalEndDateSpecified && FL_55_Application_R.approvalEndDate.DateLess(FL_55_Application_R.applicationDate))
            {
                AddError("approvalEndDate", FL_55_Application_R.approvalEndDate, "Дата окончания действия инвестиционного предложения, одобрения обращения (оферты кредитора) ФЛ_55.8 раньше Даты обращения ФЛ_55.5");
            }

            if ((eventData.EventName == refEventNameType.FL_Event_1_1
               || (eventData.EventName == refEventNameType.FL_Event_1_2 && FL_55_Application_R.stageCode == 1))
                   && !FL_55_Application_R.stageEndDateSpecified)
            {
                AddError("stageEndDate", FL_55_Application_R.stageEndDate, "Отсутствует дата окончания срока рассмотрения обращения ФЛ_55.9");
            }
            if ((eventData.EventName.In(refEventNameType.FL_Event_1_3, refEventNameType.FL_Event_1_4, refEventNameType.FL_Event_1_4_1, refEventNameType.FL_Event_2_2, refEventNameType.FL_Event_2_3)
             || (eventData.EventName == refEventNameType.FL_Event_1_2 && FL_55_Application_R.stageCode == 4))
                 && FL_55_Application_R.stageEndDateSpecified)
            {
                AddError("stageEndDate", FL_55_Application_R.stageEndDate, "Дата окончания срока рассмотрения обращения ФЛ_55.9 должна отсутствовать");
            }
            if (FL_55_Application_R.stageEndDateSpecified && FL_55_Application_R.stageEndDate.DateLess(FL_55_Application_R.applicationDate))
            {
                AddError("stageEndDate", FL_55_Application_R.stageEndDate, "Дата окончания срока рассмотрения обращения ФЛ_55.9 раньше даты обращения ФЛ_55.5");
            }


            if (FL_55_Application_R.role == 1 && FL_55_Application_R.purposeCode == null)
            {
                AddError("purposeCode", null, "Отсутствует код цели запрошенного займа (кредита) ФЛ_55.10");
            }
            if (FL_55_Application_R.role != 1 && FL_55_Application_R.purposeCode != null)
            {
                foreach (var purposeCode in FL_55_Application_R.purposeCode)
                    AddError("purposeCode", purposeCode, "Код цели запрошенного займа (кредита) ФЛ_55.10 должен отсутствовать");
            }

            if (eventData.EventName == refEventNameType.FL_Event_1_1 && FL_55_Application_R.stageCode != 1)
            {
                AddError("stageCode", FL_55_Application_R.stageCode, "Код стадии рассмотрения обращения ФЛ_55.11 должен быть равен «1»");
            }

            if (eventData.EventName == refEventNameType.FL_Event_1_3 && FL_55_Application_R.stageCode != 3)
            {
                AddError("stageCode", FL_55_Application_R.stageCode, "Код стадии рассмотрения обращения ФЛ_55.11 должен быть равен «3»");
            }

            if (eventData.EventName.In(refEventNameType.FL_Event_1_4, refEventNameType.FL_Event_1_4_1, refEventNameType.FL_Event_2_2, refEventNameType.FL_Event_2_3) && FL_55_Application_R.stageCode != 5)
            {
                AddError("stageCode", FL_55_Application_R.stageCode, "Код стадии рассмотрения обращения ФЛ_55.11 должен быть равен «5»");
            }

            if (eventData.EventName == refEventNameType.FL_Event_1_2
                && (FL_55_Application_R.approvalEndDateSpecified || !FL_55_Application_R.stageEndDateSpecified || FL_55_Application_R.stageCode != 1)
                 && (FL_55_Application_R.approvalEndDateSpecified || FL_55_Application_R.stageEndDateSpecified || FL_55_Application_R.stageCode != 4)
                 && (!FL_55_Application_R.approvalEndDateSpecified || FL_55_Application_R.stageCode != 2)
                 && FL_55_Application_R.stageCode != 5)
            {
                AddError("stageCode", FL_55_Application_R.stageCode, "Неверно указано значение кода стадии рассмотрения обращения ФЛ_55.11");
            }


            if (FL_55_Application_R.stageDate.DateGreater(eventData?.eventDate))
            {
                AddError("stageDate", FL_55_Application_R.stageDate, "Дата перехода обращения в текущую стадию рассмотрения ФЛ_55.12 позднее даты наступления события eventDate");
            }

            if (!CurrencyDictionary.Contains(FL_55_Application_R.loanCurrency))
            {
                AddError("loanCurrency", FL_55_Application_R.loanCurrency, "Валюта одобренного займа (кредита), лизинга или обеспечения ФЛ_55.16 не соответствует Общероссийскому классификатору валют");
            }
            #endregion

            #region FL_57_Reject
            if (FL_57_Reject != null)
            {
                if (FL_57_Reject.rejectDate.DateLess(FL_55_Application_R.applicationDate))
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
        public static List<INotificationOfAcceptanceError> Validate(this FL_55_57_Group_R_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
