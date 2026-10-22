using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_37_WarrantyReturn_Type : Block_Type
    {
        public FL_37_WarrantyReturn_Type()
        {
            blockName = refBlockNameType.FL_37_WarrantyReturn;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, ISource source, FL_18_Deal_Type dealBlock = null, FL_19_Amount_Type amountBlock = null)
        {
            InitCheck(eventData);

            if (exist == true)
            {
                if (sum.DecimalGreater(amountBlock?.sum))
                {
                    AddError("sum", sum, "Сумма, подлежащая возмещению ФЛ_37.2 больше суммы обязательства ФЛ_19.1");
                }

                if (calcDate.DateGreater(eventData?.eventDate))
                {
                    AddError("calcDate", calcDate, "Дата расчета ФЛ_37.5 позднее даты наступления события eventDate");
                }
                if (calcDate.DateLess(dealBlock?.startDate))
                {
                    AddError("calcDate", calcDate, "Дата расчета ФЛ_37.5 раньше даты возникновения обязательства субъекта ФЛ_18.19");
                }

                if (!CurrencyDictionary.Contains(currency))
                {
                    AddError("currency", currency, "Валюта возмещения ФЛ_37.6 не соответствует  Общероссийскому классификатору валют");
                }
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_37_WarrantyReturn_Type block, EventData eventData, ISource source, FL_18_Deal_Type dealBlock = null, FL_19_Amount_Type amountBlock = null)
        {
            var errorList = (block?.Check(eventData, source, dealBlock, amountBlock)).OrEmptyIfNull().ToList();

            if (source is FL_46_UL_36_OrgSource_Type orgSource)
            {
                if (orgSource.sourceCode == 9 && block == null)
                {
                    errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_37_WarrantyReturn, null, null, "Отсутствует блок 37. Сведения о возмещении принципалом гаранту выплаченной суммы"));
                }
                if (orgSource.sourceCode != 9 && block != null)
                {
                    errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_37_WarrantyReturn, null, null, "Блок 37. Сведения о возмещении принципалом гаранту выплаченной суммы должен отсутствовать"));
                }
            }
            else if (block != null)
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_37_WarrantyReturn, null, null, "Блок 37. Сведения о возмещении принципалом гаранту выплаченной суммы должен отсутствовать"));
            }

            return errorList;
        }
    }
}
