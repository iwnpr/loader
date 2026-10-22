using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_30_NonMonetarySource_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_30_NonMonetarySource;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock, FL_24_Fund_Type[]? fundBlocks)
        {
            ErrorData errorData = new(blockName, eventData);

            if (date.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("date", date, "ата передачи имущества субъекту ФЛ_30.4 позднее даты и времени наступления события eventDateTime");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("date", date, "Дата передачи имущества субъекту ФЛ_30.4 раньше даты совершения сделки ФЛ_17.4");
            }
            if (fundBlocks != null && fundBlocks.Any(x => x.dateSpecified && x.date.DateNotEqual(date)))
            {
                errorData.AddError("date", date, "Дата передачи имущества субъекту ФЛ_30.4 не равно дате передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ФЛ_24.1");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_30_NonMonetarySource_Type> blocks, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null, FL_18_Deal_Type? dealBlock = null, FL_24_Fund_Type[]? fundBlocks = null)
        {
            List<INotificationOfAcceptanceError> errorList = [];

            if (dealBlock?.monetarySourceExist == false && !blocks.OrEmptyIfNull().Any())
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_30_NonMonetarySource, "Отсутствует блок 30. Сведения о неденежном обязательстве источника"));
            }
            if (dealBlock?.monetarySourceExist == true && blocks.OrEmptyIfNull().Any())
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_30_NonMonetarySource, "Блок 30. Сведения о неденежном обязательстве источника должен отсутствовать"));
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
