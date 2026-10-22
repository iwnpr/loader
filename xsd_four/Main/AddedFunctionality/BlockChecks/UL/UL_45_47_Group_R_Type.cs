using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_45_47_Group_R_Type : Block_Type
    {
        public UL_45_47_Group_R_Type()
        {
            blockName = refBlockNameType.UL_45_47_Group_R;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            #region UL_45_Application_R
            if (UL_45_Application_R.currency != null && !CurrencyDictionary.Contains(UL_45_Application_R.currency))
            {
                AddError("currency", UL_45_Application_R.currency, "Запрошенная валюта обязательства ЮЛ_45.3 не соответствует Общероссийскому классификатору валют");
            }

            if (!UidHelper.CheckUid(UL_45_Application_R.uid))
            {
                AddError("uid", UL_45_Application_R.uid, "Контрольный символ УИДа обращения ЮЛ_45.4 не верный");
            }

            if (UL_45_Application_R.applicationDate.DateGreater(eventData?.eventDate))
            {
                AddError("applicationDate", UL_45_Application_R.applicationDate, "Дата обращения ЮЛ_45.5 позднее даты наступления события eventDate");
            }

            if ((eventData.EventName.In(refEventNameType.UL_Event_1_4, refEventNameType.UL_Event_1_4_1, refEventNameType.UL_Event_2_2, refEventNameType.UL_Event_2_3)
                    || (eventData.EventName == refEventNameType.UL_Event_1_2 && UL_45_Application_R.stageCode == 2))
                && !UL_45_Application_R.approvalEndDateSpecified)
            {
                AddError("approvalEndDate", UL_45_Application_R.approvalEndDate, "Отсутствует дата окончания действия инвестиционного предложения, одобрения обращения (оферты кредитора) ЮЛ_45.8");
            }

            if (eventData.EventName.In(refEventNameType.UL_Event_1_1, refEventNameType.UL_Event_1_3)
               && UL_45_Application_R.approvalEndDateSpecified)
            {
                AddError("approvalEndDate", UL_45_Application_R.approvalEndDate, "Дата окончания действия инвестиционного предложения, одобрения обращения (оферты кредитора) ЮЛ_45.8 должна отсутствовать");
            }
            if (UL_45_Application_R.approvalEndDateSpecified && UL_45_Application_R.approvalEndDate.DateLess(UL_45_Application_R.applicationDate))
            {
                AddError("approvalEndDate", UL_45_Application_R.approvalEndDate, "Дата окончания действия инвестиционного предложения, одобрения обращения (оферты кредитора) ЮЛ_45.8 раньше Даты обращения ЮЛ_45.5");
            }

            if ((eventData.EventName == refEventNameType.UL_Event_1_1
               || (eventData.EventName == refEventNameType.UL_Event_1_2 && UL_45_Application_R.stageCode == 1))
                   && !UL_45_Application_R.stageEndDateSpecified)
            {
                AddError("stageEndDate", UL_45_Application_R.stageEndDate, "Отсутствует дата окончания срока рассмотрения обращения ЮЛ_45.9");
            }
            if ((eventData.EventName.In(refEventNameType.UL_Event_1_3, refEventNameType.UL_Event_1_4, refEventNameType.UL_Event_1_4_1, refEventNameType.UL_Event_2_2, refEventNameType.UL_Event_2_3)
             || (eventData.EventName == refEventNameType.UL_Event_1_2 && UL_45_Application_R.stageCode == 4))
                 && UL_45_Application_R.stageEndDateSpecified)
            {
                AddError("stageEndDate", UL_45_Application_R.stageEndDate, "Дата окончания срока рассмотрения обращения ЮЛ_45.9 должна отсутствовать");
            }
            if (UL_45_Application_R.stageEndDateSpecified && UL_45_Application_R.stageEndDate.DateLess(UL_45_Application_R.applicationDate))
            {
                AddError("stageEndDate", UL_45_Application_R.stageEndDate, "Дата окончания срока рассмотрения обращения ЮЛ_45.9 раньше даты обращения ЮЛ_45.5");
            }


            if (UL_45_Application_R.role == 1 && UL_45_Application_R.purposeCode == null)
            {
                AddError("purposeCode", null, "Отсутствует код цели запрошенного займа (кредита) ЮЛ_45.10");
            }
            if (UL_45_Application_R.role != 1 && UL_45_Application_R.purposeCode != null)
            {
                foreach (var purposeCode in UL_45_Application_R.purposeCode)
                    AddError("purposeCode", purposeCode, "Код цели запрошенного займа (кредита) ЮЛ_45.10 должен отсутствовать");
            }

            if (eventData.EventName == refEventNameType.UL_Event_1_1 && UL_45_Application_R.stageCode != 1)
            {
                AddError("stageCode", UL_45_Application_R.stageCode, "Код стадии рассмотрения обращения ЮЛ_45.11 должен быть равен «1»");
            }

            if (eventData.EventName == refEventNameType.UL_Event_1_3 && UL_45_Application_R.stageCode != 3)
            {
                AddError("stageCode", UL_45_Application_R.stageCode, "Код стадии рассмотрения обращения ЮЛ_45.11 должен быть равен «3»");
            }

            if (eventData.EventName.In(refEventNameType.UL_Event_1_4, refEventNameType.UL_Event_1_4_1, refEventNameType.UL_Event_2_2, refEventNameType.UL_Event_2_3) && UL_45_Application_R.stageCode != 5)
            {
                AddError("stageCode", UL_45_Application_R.stageCode, "Код стадии рассмотрения обращения ЮЛ_45.11 должен быть равен «5»");
            }

            if (eventData.EventName == refEventNameType.UL_Event_1_2
                && (UL_45_Application_R.approvalEndDateSpecified || !UL_45_Application_R.stageEndDateSpecified || UL_45_Application_R.stageCode != 1)
                 && (UL_45_Application_R.approvalEndDateSpecified || UL_45_Application_R.stageEndDateSpecified || UL_45_Application_R.stageCode != 4)
                 && (!UL_45_Application_R.approvalEndDateSpecified || UL_45_Application_R.stageCode != 2)
                 && UL_45_Application_R.stageCode != 5)
            {
                AddError("stageCode", UL_45_Application_R.stageCode, "Неверно указано значение кода стадии рассмотрения обращения ЮЛ_45.11");
            }


            if (UL_45_Application_R.stageDate.DateGreater(eventData?.eventDate))
            {
                AddError("stageDate", UL_45_Application_R.stageDate, "Дата перехода обращения в текущую стадию рассмотрения ЮЛ_45.12 позднее даты наступления события eventDate");
            }

            if (!CurrencyDictionary.Contains(UL_45_Application_R.loanCurrency))
            {
                AddError("loanCurrency", UL_45_Application_R.loanCurrency, "Валюта одобренного займа (кредита), лизинга или обеспечения ЮЛ_45.16 не соответствует Общероссийскому классификатору валют");
            }
            #endregion

            #region UL_47_Reject
            if (UL_47_Reject != null)
            {
                if (UL_47_Reject.rejectDate.DateLess(UL_45_Application_R.applicationDate))
                {
                    AddError("rejectDate", UL_47_Reject.rejectDate, "Дата отказа ЮЛ_47.1 раньше даты обращения ЮЛ_45.5");
                }
                if (UL_47_Reject.rejectDate.DateGreater(eventData?.eventDate))
                {
                    AddError("rejectDate", UL_47_Reject.rejectDate, "Дата отказа ЮЛ_47.1 позднее даты наступления события eventDate");
                }

                foreach (var code in UL_47_Reject.rejectCode.Where(x => x == 7))
                {
                    AddError("rejectDate", code, "Код причины отказа ЮЛ_47.2 равен «7»");
                }
            }
            #endregion

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_45_47_Group_R_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
