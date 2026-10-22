using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_12_1_AmountInfo_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_12_1_AmountInfo;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, UL_11_Deal_Type dealBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (dealBlock.code.In(refContractType.Item1, refContractType.Item2) && securityFact != false)
            {
                errorData.AddError("securityFact_0/securityFact_1", null, "Признак обеспечивающего обязательства ЮЛ_12(1).1 должен быть равен «0»");
            }

            if (!CurrencyDictionary.Contains(securityCurrency))
            {
                errorData.AddError("securityCurrency", securityCurrency, "Валюта обеспечиваемого обязательства ЮЛ_12(1).3 не соответствует Общероссийскому классификатору валют");
            }
            if (securityFact == true)
            {
                if (calcDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("calcDate", calcDate, "Дата расчета ЮЛ_12(1).5 позднее даты и времени наступления события eventDateTime");
                }
                if (calcDate.DateLess(dealBlock.startDate))
                {
                    errorData.AddError("calcDate", calcDate, "Дата расчета ЮЛ_12(1).5 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
                }
                if (!UidHelper.CheckUid(securityUid))
                {
                    errorData.AddError("securityUid", securityUid, "Неверный контрольный символ УИда сделки, в результате которой возникло обеспечиваемое обязательство ЮЛ_12(1).6");
                }
                if (!CurrencyDictionary.Contains(limitCurrency))
                {
                    errorData.AddError("limitCurrency", limitCurrency, "Валюта лимита ответственности по обеспечиваемому обязательству ЮЛ_12(1).8 не соответствует Общероссийскому классификатору валют");
                }
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_12_1_AmountInfo_Type block, IEventData eventData, UL_11_Deal_Type dealBlock)
        {
            return [.. (block?.Check(eventData, dealBlock)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<UL_12_1_AmountInfo_Type> blocks, IEventData eventData, UL_11_Deal_Type dealBlock)
        {
            List<INotificationOfAcceptanceError> errorList = [];
            foreach (var block in blocks.OrEmptyIfNull())
            {
                errorList.AddRange(block.Check(eventData, dealBlock));
            }
            return errorList;
        }
    }
}
