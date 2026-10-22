using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_24_Fund_Type : Block_Type
    {
        public FL_24_Fund_Type()
        {
            blockName = refBlockNameType.FL_24_Fund;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (date.DateGreater(eventData?.eventDate))
            {
                AddError("date", date, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ФЛ_24.1 позднее даты наступления события eventDate");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                AddError("date", date, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ФЛ_24.1 раньше даты совершения сделки ФЛ_17.4");
            }

            if (num.IsNotNull() && decimal.Parse(num) < 1)
            {
                AddError("num", num, "Порядковый номер транша ФЛ_24.2 меньше «1»");
            }

            if (!CurrencyDictionary.Contains(currency))
            {
                AddError("currency", currency, "Валюта задолженности на дату передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ФЛ_24.4 не соответствует Общероссийскому классификатору валют");
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_24_Fund_Type block, EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}
