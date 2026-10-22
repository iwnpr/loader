using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_30_NonMonetarySource_Type : Block_Type
    {
        public FL_30_NonMonetarySource_Type()
        {
            blockName = refBlockNameType.FL_30_NonMonetarySource;
        }
        public List<INotificationOfAcceptanceError> Check(EventData eventData, IFL_17_DealUid dealUidBlock = null, FL_24_Fund_Type[] fundBlocks = null)
        {
            InitCheck(eventData);

            if (date.DateGreater(eventData?.eventDate))
            {
                AddError("date", date, "Дата передачи имущества субъекту ФЛ_30.4 позднее даты наступления события eventDate");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                AddError("date", date, "Дата передачи имущества субъекту ФЛ_30.4 раньше даты совершения сделки ФЛ_17.4");
            }
            if (fundBlocks != null && !fundBlocks.OrEmptyIfNull().Any(x => x.date.DateEqual(date)))
            {
                AddError("date", date, "Дата передачи имущества субъекту ФЛ_30.4 не равно дате передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ФЛ_24.1");
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_30_NonMonetarySource_Type[] blocks, EventData eventData, IFL_17_DealUid dealUidBlock = null, FL_18_Deal_Type dealBlock = null, FL_24_Fund_Type[] fundBlocks = null)
        {
            var errorList = new List<INotificationOfAcceptanceError>();

            if (dealBlock?.monetarySourceExist == false && blocks.OrEmptyIfNull().Count() == 0)
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_30_NonMonetarySource, null, null, "Отсутствует блок 30. Сведения о неденежном обязательстве источника"));
            }
            if (dealBlock?.monetarySourceExist == true && blocks.OrEmptyIfNull().Count() != 0)
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_30_NonMonetarySource, null, null, "Блок 30. Сведения о неденежном обязательстве источника должен отсутствовать"));
            }
            foreach (var block in blocks.OrEmptyIfNull())
            {
                errorList.AddRange(block.Check(eventData, dealUidBlock, fundBlocks));
            }
            foreach (var propertyId in blocks.OrEmptyIfNull().GroupBy(x => x.propertyId).Where(x => x.Count() > 1).Select(x => x.Key))
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_30_NonMonetarySource, "propertyId", propertyId, "Идентификационный код предоставляемого имущества ФЛ_30.5 не уникальный"));
            }
            return errorList;
        }
    }
}
