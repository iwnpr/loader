using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_16_Fund_Type : Block_Type
    {
        public UL_16_Fund_Type()
        {
            blockName = refBlockNameType.UL_16_Fund;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IUL_10_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (dateSpecified)
            {
                if (date.DateGreater(eventData?.eventDate))
                {
                    AddError("date", date, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ЮЛ_16.1 позднее даты наступления события eventDate");
                }
                if (date.DateLess(dealUidBlock?.openDate))
                {
                    AddError("date", date, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ЮЛ_16.1 раньше даты совершения сделки ЮЛ_10.4");
                }
            }
            if (num.IsNotNull() && decimal.Parse(num) < 1)
            {
                AddError("num", num, "Порядковый номер транша ЮЛ_16.2 меньше «1»");
            }

            if (!CurrencyDictionary.Contains(currency))
            {
                AddError("currency", currency, "Валюта задолженности на дату передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ЮЛ_16.4 не соответствует Общероссийскому классификатору валют");
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_16_Fund_Type block, EventData eventData, IUL_10_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}
