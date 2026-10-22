using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_44_Accounting_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_44_Accounting;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, UL_11_Deal_Type? dealBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (dealBlock != null)
            {
                if (dealBlock.code.In(refContractType.Item6,
                    refContractType.Item7,
                    refContractType.Item8,
                    refContractType.Item9,
                    refContractType.Item10,
                    refContractType.Item11,
                    refContractType.Item12,
                    refContractType.Item13) == true)
                {
                    if (exist != null)
                    {
                        errorData.AddError("exist_0/exist_1", null, "Признак учета задолженности ЮЛ_44.1 должен отсутствовать");
                    }
                }
                else if (exist == null)
                {
                    errorData.AddError("exist_0/exist_1", null, "Отсутствует признак учета задолженности ЮЛ_44.1");
                }
            }

            if (exist == false && !CurrencyDictionary.Contains(currency))
            {
                errorData.AddError("currency", currency, "Валюта суммы задолженности, учтенной на внебалансовых счетах ЮЛ_44.8 не соответствует Общероссийскому классификатору валют");
            }

            if (calcDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("calcDate", calcDate, "Дата расчета ЮЛ_44.7 позднее даты и времени наступления события eventDateTime");
            }
            if (calcDate.DateLess(dealBlock?.startDate))
            {
                errorData.AddError("calcDate", calcDate, "Дата расчета ЮЛ_44.7 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
            }

            if (unaccountedSum != 0 && unaccountedCurrency == null)
            {
                errorData.AddError("unaccountedCurrency", unaccountedCurrency, "Отсутствует валюта суммы задолженности, не учтенной на балансовых и внебалансовых счетах ЮЛ_44.9");
            }
            if (unaccountedSum == 0 && unaccountedCurrency != null)
            {
                errorData.AddError("unaccountedCurrency", unaccountedCurrency, "Валюта суммы задолженности, не учтенной на балансовых и внебалансовых счетах ЮЛ_44.9 должна отсутствовать");
            }
            if (!CurrencyDictionary.Contains(unaccountedCurrency))
            {
                errorData.AddError("unaccountedCurrency", unaccountedCurrency, "Валюта суммы задолженности, не учтенной на балансовых и внебалансовых счетах ЮЛ_44.9 не соответствует Общероссийскому классификатору валют");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_44_Accounting_Type block, IEventData eventData, UL_11_Deal_Type? dealBlock = null)
        {
            return [.. (block?.Check(eventData, dealBlock)).OrEmptyIfNull()];
        }
    }
}
