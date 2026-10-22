using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_28_WarrantyReturn_Type : Block_Type
    {
        public UL_28_WarrantyReturn_Type()
        {
            blockName = refBlockNameType.UL_28_WarrantyReturn;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, ISource source, UL_11_Deal_Type dealBlock = null, UL_12_Amount_Type amountBlock = null)
        {
            InitCheck(eventData);

            if (exist == true)
            {
                if (sum.DecimalGreater(amountBlock?.sum))
                {
                    AddError("sum", sum, "Сумма, подлежащая возмещению ЮЛ_28.2 больше суммы обязательства ЮЛ_12.1");
                }

                if (calcDate.DateGreater(eventData?.eventDate))
                {
                    AddError("calcDate", calcDate, "Дата расчета ЮЛ_28.5 позднее даты наступления события eventDate");
                }
                if (calcDate.DateLess(dealBlock?.startDate))
                {
                    AddError("calcDate", calcDate, "Дата расчета ЮЛ_28.5 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
                }

                if (!CurrencyDictionary.Contains(currency))
                {
                    AddError("currency", currency, "Валюта возмещения ЮЛ_28.6 не соответствует  Общероссийскому классификатору валют");
                }
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_28_WarrantyReturn_Type block, EventData eventData, ISource source, UL_11_Deal_Type dealBlock = null, UL_12_Amount_Type amountBlock = null)
        {
            var errorList = (block?.Check(eventData, source, dealBlock, amountBlock)).OrEmptyIfNull().ToList();

            if (source is FL_46_UL_36_OrgSource_Type orgSource)
            {
                if (orgSource.sourceCode == 9 && block == null)
                {
                    errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_28_WarrantyReturn, null, null, "Отсутствует блок 28. Сведения о возмещении принципалом гаранту выплаченной суммы"));
                }
                if (orgSource.sourceCode != 9 && block != null)
                {
                    errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_28_WarrantyReturn, null, null, "Блок 28. Сведения о возмещении принципалом гаранту выплаченной суммы должен отсутствовать"));
                }
            }
            else if (block != null)
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_28_WarrantyReturn, null, null, "Блок 28. Сведения о возмещении принципалом гаранту выплаченной суммы должен отсутствовать"));
            }

            return errorList;
        }
    }
}
