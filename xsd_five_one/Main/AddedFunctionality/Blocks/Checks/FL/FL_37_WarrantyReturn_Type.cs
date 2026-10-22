using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_37_WarrantyReturn_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_37_WarrantyReturn;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, FL_18_Deal_Type? dealBlock, FL_19_Amount_Type? amountBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (exist == true)
            {
                if (sum.DecimalGreater(amountBlock?.sum))
                {
                    errorData.AddError("sum", sum, "Сумма, подлежащая возмещению ФЛ_37.2 больше суммы обязательства ФЛ_19.1");
                }

                if (calcDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("calcDate", calcDate, "Дата расчета ФЛ_37.5 позднее даты и времени наступления события eventDateTime");
                }
                if (calcDate.DateLess(dealBlock?.startDate))
                {
                    errorData.AddError("calcDate", calcDate, "Дата расчета ФЛ_37.5 раньше даты возникновения обязательства субъекта ФЛ_18.19");
                }

                if (!CurrencyDictionary.Contains(currency))
                {
                    errorData.AddError("currency", currency, "Валюта возмещения ФЛ_37.6 не соответствует  Общероссийскому классификатору валют");
                }
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_37_WarrantyReturn_Type block, IEventData eventData, ISource source, FL_18_Deal_Type? dealBlock = null, FL_19_Amount_Type? amountBlock = null)
        {
            var errorList = (block?.Check(eventData, dealBlock, amountBlock)).OrEmptyIfNull().ToList();

            if (source is FL_46_UL_36_OrgSource_Type orgSource)
            {
                if (orgSource.sourceCode == refSourceType.Item9 && block == null)
                {
                    errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_37_WarrantyReturn, "Отсутствует блок 37. Сведения о возмещении принципалом гаранту выплаченной суммы"));
                }
                if (orgSource.sourceCode != refSourceType.Item9 && block != null)
                {
                    errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_37_WarrantyReturn, "Блок 37. Сведения о возмещении принципалом гаранту выплаченной суммы должен отсутствовать"));
                }
            }
            else if (block != null)
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_37_WarrantyReturn, "Блок 37. Сведения о возмещении принципалом гаранту выплаченной суммы должен отсутствовать"));
            }

            return errorList;
        }
    }
}
