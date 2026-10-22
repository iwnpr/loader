using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_44_Accounting_Type : Block_Type
    {
        public UL_44_Accounting_Type()
        {
            blockName = refBlockNameType.UL_44_Accounting;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, UL_11_Deal_Type dealBlock = null)
        {
            InitCheck(eventData);

            if (exist == false && !CurrencyDictionary.Contains(currency))
            {
                AddError("currency", currency, "Валюта суммы задолженности, учтенной на внебалансовых счетах ЮЛ_44.8 не соответствует Общероссийскому классификатору валют");
            }

            if (calcDate.DateGreater(eventData?.eventDate))
            {
                AddError("calcDate", calcDate, "Дата расчета ЮЛ_44.7 позднее даты наступления события eventDate");
            }
            if (calcDate.DateLess(dealBlock?.startDate))
            {
                AddError("calcDate", calcDate, "Дата расчета ЮЛ_44.7 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_44_Accounting_Type block, EventData eventData, UL_11_Deal_Type dealBlock = null)
        {
            return (block?.Check(eventData, dealBlock)).OrEmptyIfNull().ToList();
        }
    }
}
