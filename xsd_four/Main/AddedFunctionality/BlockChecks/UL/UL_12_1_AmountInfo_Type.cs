using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_12_1_AmountInfo_Type : Block_Type
    {
        public UL_12_1_AmountInfo_Type()
        {
            blockName = refBlockNameType.UL_12_1_AmountInfo;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, UL_11_Deal_Type dealBlock = null)
        {
            InitCheck(eventData);

            if (dealBlock.code.In(1, 2) && securityFact != false)
            {
                AddError("securityFact_0/securityFact_1", null, "Признак обеспечивающего обязательства ЮЛ_12(1).1 должен быть равен «0»");
            }

            if (!CurrencyDictionary.Contains(securityCurrency))
            {
                AddError("securityCurrency", securityCurrency, "Валюта обеспечиваемого обязательства ЮЛ_12(1).3 не соответствует Общероссийскому классификатору валют");
            }
            if (securityFact == true)
            {
                if (securityTypeCode == null)
                {
                    AddError("securityTypeCode", securityTypeCode, "Отсутствует код типа обеспечиваемого обязательства ЮЛ_12(1).4");
                }
                if (calcDate.DateGreater(eventData?.eventDate))
                {
                    AddError("calcDate", calcDate, "Дата расчета ЮЛ_12(1).5 позднее даты наступления события eventDate");
                }
                if (calcDate.DateLess(dealBlock?.startDate))
                {
                    AddError("calcDate", calcDate, "Дата расчета ЮЛ_12(1).5 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
                }
                if (!UidHelper.CheckUid(securityUid))
                {
                    AddError("securityUid", securityUid, "Неверный контрольный символ УИда сделки, в результате которой возникло обеспечиваемое обязательство ЮЛ_12(1).6");
                }
                if (!CurrencyDictionary.Contains(limitCurrency))
                {
                    AddError("limitCurrency", limitCurrency, "Валюта лимита ответственности по обеспечиваемому обязательству ЮЛ_12(1).8 не соответствует Общероссийскому классификатору валют");
                }
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_12_1_AmountInfo_Type block, EventData eventData, UL_11_Deal_Type dealBlock = null)
        {
            return (block?.Check(eventData, dealBlock)).OrEmptyIfNull().ToList();
        }
    }
}
