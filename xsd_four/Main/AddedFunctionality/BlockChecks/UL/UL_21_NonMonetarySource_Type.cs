using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_21_NonMonetarySource_Type : Block_Type
    {
        public UL_21_NonMonetarySource_Type()
        {
            blockName = refBlockNameType.UL_21_NonMonetarySource;
        }
        public List<INotificationOfAcceptanceError> Check(EventData eventData, IUL_10_DealUid dealUidBlock = null, UL_16_Fund_Type[] fundBlocks = null)
        {
            InitCheck(eventData);

            if (date.DateGreater(eventData?.eventDate))
            {
                AddError("date", date, "Дата передачи имущества субъекту ЮЛ_21.4 позднее даты наступления события eventDate");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                AddError("date", date, "Дата передачи имущества субъекту ЮЛ_21.4 раньше даты совершения сделки ЮЛ_10.4");
            }
            if (fundBlocks != null && !fundBlocks.OrEmptyIfNull().Any(x => x.date.DateEqual(date)))
            {
                AddError("date", date, "Дата передачи имущества субъекту ЮЛ_21.4 не равно дате передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ЮЛ_16.1");
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_21_NonMonetarySource_Type[] blocks, EventData eventData, IUL_10_DealUid dealUidBlock = null, UL_11_Deal_Type dealBlock = null, UL_16_Fund_Type[] fundBlocks = null)
        {
            var errorList = new List<INotificationOfAcceptanceError>();

            if (dealBlock?.monetarySourceExist == false && blocks.OrEmptyIfNull().Count() == 0)
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_21_NonMonetarySource, null, null, "Отсутствует блок 21. Сведения о неденежном обязательстве источника"));
            }
            if (dealBlock?.monetarySourceExist == true && blocks.OrEmptyIfNull().Count() != 0)
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_21_NonMonetarySource, null, null, "Блок 21. Сведения о неденежном обязательстве источника должен отсутствовать"));
            }
            foreach (var block in blocks.OrEmptyIfNull())
            {
                errorList.AddRange(block.Check(eventData, dealUidBlock, fundBlocks));
            }
            foreach (var propertyId in blocks.OrEmptyIfNull().GroupBy(x => x.propertyId).Where(x => x.Count() > 1).Select(x => x.Key))
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_21_NonMonetarySource, "propertyId", propertyId, "Идентификационный код предоставляемого имущества ЮЛ_21.5 не уникальный"));
            }
            return errorList;
        }
    }
}
