using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public interface I_FL_55_Application_Type : I_BlockType
    {
        public string? currency { get; }
        public string? uid { get; }
        public System.DateTime applicationDate { get; }
        public System.DateTime approvalEndDate { get; }
        public bool approvalEndDateSpecified { get; }
        public System.DateTime stageEndDate { get; }
        public bool stageEndDateSpecified { get; }
        public refLoanGoalType[]? purposeCode { get; }
        public System.DateTime stageDate { get; }
        public refLoanAgreementsType role { get; }
        public refAppealReviewStageType stageCode { get; }
        public bool loanSumSpecified { get; }
        public bool loanCurrencySpecified { get; }
        public decimal loanSum { get; }
        public string? loanCurrency { get; }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (currency != null && !CurrencyDictionary.Contains(currency))
            {
                errorData.AddError("currency", currency, "Запрошенная валюта обязательства ФЛ_55.3 не соответствует Общероссийскому классификатору валют");
            }

            if (!UidHelper.CheckUid(uid))
            {
                errorData.AddError("uid", uid, "Контрольный символ УИДа обращения ФЛ_55.4 не верный");
            }

            if (applicationDate.DateGreater(stageDate))
            {
                errorData.AddError("applicationDate", applicationDate, "Дата обращения ФЛ_55.5 позднее даты перехода обращения в текущую стадию рассмотрения ФЛ_55.12");
            }

            if ((eventData.EventName.In(refEventNameType.FL_Event_1_4, refEventNameType.FL_Event_1_4_1, refEventNameType.FL_Event_2_2, refEventNameType.FL_Event_2_3)
                    || (eventData.EventName == refEventNameType.FL_Event_1_2 && stageCode == refAppealReviewStageType.Item2))
                && !approvalEndDateSpecified)
            {
                errorData.AddError("approvalEndDate", approvalEndDate, "Отсутствует дата окончания действия инвестиционного предложения, одобрения обращения (оферты кредитора) ФЛ_55.8");
            }

            if ((eventData.EventName.In(refEventNameType.FL_Event_1_1, refEventNameType.FL_Event_1_3)
                || eventData.EventName == refEventNameType.FL_Event_1_2 && stageCode.In(refAppealReviewStageType.Item1, refAppealReviewStageType.Item4))
               && approvalEndDateSpecified)
            {
                errorData.AddError("approvalEndDate", approvalEndDate, "Дата окончания действия инвестиционного предложения, одобрения обращения (оферты кредитора) ФЛ_55.8 должна отсутствовать");
            }
            if (approvalEndDateSpecified && approvalEndDate.DateLess(applicationDate))
            {
                errorData.AddError("approvalEndDate", approvalEndDate, "Дата окончания действия инвестиционного предложения, одобрения обращения (оферты кредитора) ФЛ_55.8 раньше Даты обращения ФЛ_55.5");
            }

            if ((eventData.EventName == refEventNameType.FL_Event_1_1
               || (eventData.EventName == refEventNameType.FL_Event_1_2 && stageCode == refAppealReviewStageType.Item1))
                   && !stageEndDateSpecified)
            {
                errorData.AddError("stageEndDate", stageEndDate, "Отсутствует дата окончания срока рассмотрения обращения ФЛ_55.9");
            }
            if ((eventData.EventName.In(refEventNameType.FL_Event_1_3, refEventNameType.FL_Event_1_4, refEventNameType.FL_Event_1_4_1, refEventNameType.FL_Event_2_2, refEventNameType.FL_Event_2_3)
             || (eventData.EventName == refEventNameType.FL_Event_1_2 && stageCode == refAppealReviewStageType.Item4))
                 && stageEndDateSpecified)
            {
                errorData.AddError("stageEndDate", stageEndDate, "Дата окончания срока рассмотрения обращения ФЛ_55.9 должна отсутствовать");
            }
            if (stageEndDateSpecified && stageEndDate.DateLess(applicationDate))
            {
                errorData.AddError("stageEndDate", stageEndDate, "Дата окончания срока рассмотрения обращения ФЛ_55.9 раньше даты обращения ФЛ_55.5");
            }

            if (role == refLoanAgreementsType.Item1 && purposeCode == null)
            {
                errorData.AddError("purposeCode", null, "Отсутствует код цели запрошенного займа (кредита) ФЛ_55.10");
            }
            if (role != refLoanAgreementsType.Item1 && purposeCode != null)
            {
                foreach (var purposeCode in purposeCode)
                    errorData.AddError("purposeCode", purposeCode, "Код цели запрошенного займа (кредита) ФЛ_55.10 должен отсутствовать");
            }

            if (eventData.EventName == refEventNameType.FL_Event_1_1 && stageCode != refAppealReviewStageType.Item1)
            {
                errorData.AddError("stageCode", stageCode, "Код стадии рассмотрения обращения ФЛ_55.11 должен быть равен «1»");
            }

            if (eventData.EventName == refEventNameType.FL_Event_1_3 && stageCode != refAppealReviewStageType.Item3)
            {
                errorData.AddError("stageCode", stageCode, "Код стадии рассмотрения обращения ФЛ_55.11 должен быть равен «3»");
            }

            if (eventData.EventName.In(refEventNameType.FL_Event_1_4, refEventNameType.FL_Event_1_4_1, refEventNameType.FL_Event_2_2, refEventNameType.FL_Event_2_3) && stageCode != refAppealReviewStageType.Item5)
            {
                errorData.AddError("stageCode", stageCode, "Код стадии рассмотрения обращения ФЛ_55.11 должен быть равен «5»");
            }

            if (eventData.EventName == refEventNameType.FL_Event_1_2
                && (approvalEndDateSpecified || !stageEndDateSpecified || stageCode != refAppealReviewStageType.Item1)
                 && (approvalEndDateSpecified || stageEndDateSpecified || stageCode != refAppealReviewStageType.Item4)
                 && (!approvalEndDateSpecified || stageCode != refAppealReviewStageType.Item2)
                 && stageCode != refAppealReviewStageType.Item5)
            {
                errorData.AddError("stageCode", stageCode, "Неверно указано значение кода стадии рассмотрения обращения ФЛ_55.11");
            }

            if (stageDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("stageDate", stageDate, "Дата перехода обращения в текущую стадию рассмотрения ФЛ_55.12 позднее даты и времени наступления события eventDateTime");
            }
            if (stageDate.DateGreater(dealUidBlock?.openDate))
            {
                errorData.AddError("stageDate", stageDate, "Дата перехода обращения в текущую стадию рассмотрения ФЛ_55.12 позднее даты совершения сделки ФЛ_17.4");
            }

            if ((eventData.EventName.In(refEventNameType.FL_Event_1_1, refEventNameType.FL_Event_1_3)
                    || stageCode.In(refAppealReviewStageType.Item1, refAppealReviewStageType.Item3, refAppealReviewStageType.Item4))
                && loanSumSpecified)
            {
                errorData.AddError("loanSum", loanSum, "Сумма одобренного займа (кредита), лизинга или обеспечения ФЛ_55.15 должна отсутствовать");
            }

            if (loanCurrencySpecified && !CurrencyDictionary.Contains(loanCurrency))
            {
                errorData.AddError("loanCurrency", loanCurrency, "Валюта одобренного займа (кредита), лизинга или обеспечения ФЛ_55.16 не соответствует Общероссийскому классификатору валют");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this I_FL_55_Application_Type block, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }
    }
}

