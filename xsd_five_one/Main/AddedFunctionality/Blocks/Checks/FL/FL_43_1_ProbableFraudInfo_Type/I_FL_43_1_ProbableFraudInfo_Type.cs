using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public interface I_FL_43_1_ProbableFraudInfo_Type : I_BlockType
    {
        public string uid { get; }
        public refCommitmentStatusType statusCode { get; }
        public DateTimeOffset dateTime { get; }
        public decimal sum { get; }
        public string currency { get; }
        public refReasonRefuseContractType[] rejectCode { get; }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData)
        {
            ErrorData errorData = new(blockName, eventData);

            if (!UidHelper.CheckUid(uid))
            {
                errorData.AddError("uid", uid, "Неверный контрольный символ УИд сделки (УИд обращения)  ФЛ_43(1).1");
            }
            if ((eventData is FL_Event_2_13_1_Type && statusCode != refCommitmentStatusType.Item1)
                || (eventData is FL_Event_2_13_2_Type && statusCode != refCommitmentStatusType.Item2)
                || (eventData is FL_Event_2_13_3_Type && statusCode != refCommitmentStatusType.Item3)
                || (eventData is FL_Event_2_13_4_Type && statusCode != refCommitmentStatusType.Item4)
                || (eventData is FL_Event_2_13_5_Type && statusCode != refCommitmentStatusType.Item5))
            {
                errorData.AddError("statusCode", statusCode, "Неверно указан код стадии рассмотрения обращения (статуса обязательства) ФЛ_43(1).3");
            }

            if (dateTime > eventData.eventDateTime)
            {
                errorData.AddError("dateTime", dateTime, "Дата и время перехода обращения в текущую стадию рассмотрения (обязательства в текущий статус) ФЛ_43(1).4 позднее даты и времени наступления события eventDateTime");
            }

            if (!CurrencyDictionary.Contains(currency))
            {
                errorData.AddError("currency", currency, "Валюта суммы займа (кредита) ФЛ_43(1).6 не соответствует Общероссийскому классификатору валют");
            }
            if (eventData is FL_Event_2_13_5_Type && rejectCode.OrEmptyIfNull().Count() == 0)
            {
                errorData.AddError("rejectCode", rejectCode, "Отсутствует код причины отказа ФЛ_43(1).7");
            }
            if ((eventData is FL_Event_2_13_1_Type
                    || eventData is FL_Event_2_13_2_Type
                    || eventData is FL_Event_2_13_3_Type
                    || eventData is FL_Event_2_13_4_Type)
                && rejectCode.OrEmptyIfNull().Count() != 0)
            {
                foreach (var rejectCodeValue in rejectCode)
                {
                    errorData.AddError("rejectCode", rejectCodeValue, "Код причины отказа ФЛ_43(1).7 должен отсутствовать");
                }
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this I_FL_43_1_ProbableFraudInfo_Type block, IEventData eventData)
        {
            return [.. (block?.Check(eventData)).OrEmptyIfNull()];
        }
    }
}
