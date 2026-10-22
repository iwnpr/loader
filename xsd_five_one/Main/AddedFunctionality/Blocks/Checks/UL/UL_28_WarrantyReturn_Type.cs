using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_28_WarrantyReturn_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_28_WarrantyReturn;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, UL_11_Deal_Type? dealBlock, UL_12_Amount_Type? amountBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (exist == true)
            {
                if (sum.DecimalGreater(amountBlock?.sum))
                {
                    errorData.AddError("sum", sum, "Сумма, подлежащая возмещению ЮЛ_28.2 больше суммы обязательства ЮЛ_12.1");
                }

                if (calcDate.DateGreater(eventData?.eventDateTimeLocal))
                {
                    errorData.AddError("calcDate", calcDate, "Дата расчета ЮЛ_28.5 позднее даты и времени наступления события eventDateTime");
                }
                if (calcDate.DateLess(dealBlock?.startDate))
                {
                    errorData.AddError("calcDate", calcDate, "Дата расчета ЮЛ_28.5 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
                }

                if (!CurrencyDictionary.Contains(currency))
                {
                    errorData.AddError("currency", currency, "Валюта возмещения ЮЛ_28.6 не соответствует  Общероссийскому классификатору валют");
                }
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_28_WarrantyReturn_Type block, IEventData eventData, ISource source, UL_11_Deal_Type? dealBlock = null, UL_12_Amount_Type? amountBlock = null)
        {
            var errorList = (block?.Check(eventData, dealBlock, amountBlock)).OrEmptyIfNull().ToList();

            if (source is FL_46_UL_36_OrgSource_Type orgSource)
            {
                if (orgSource.sourceCode == refSourceType.Item9 && block == null)
                {
                    errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_28_WarrantyReturn, "Отсутствует блок 28. Сведения о возмещении принципалом гаранту выплаченной суммы"));
                }
                if (orgSource.sourceCode != refSourceType.Item9 && block != null)
                {
                    errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_28_WarrantyReturn, "Блок 28. Сведения о возмещении принципалом гаранту выплаченной суммы должен отсутствовать"));
                }
            }
            else if (block != null)
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_28_WarrantyReturn, "Блок 28. Сведения о возмещении принципалом гаранту выплаченной суммы должен отсутствовать"));
            }

            return errorList;
        }
    }
}
