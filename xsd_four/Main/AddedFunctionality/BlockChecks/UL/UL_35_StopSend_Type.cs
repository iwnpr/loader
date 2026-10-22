using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_35_StopSend_Type : Block_Type
    {
        public UL_35_StopSend_Type()
        {
            blockName = refBlockNameType.UL_35_StopSend;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IUL_10_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (eventData.EventName == refEventNameType.UL_Event_2_11 && !code.In(3, 6))
            {
                AddError("code", code, "Код причины прекращения передачи информации ЮЛ_35.1 не равен «3» или «6»");
            }
            if (eventData.EventName == refEventNameType.UL_Event_2_10 && code == 3)
            {
                AddError("code", code, "Код причины прекращения передачи информации ЮЛ_35.1 равен «3»");
            }

            if (date.DateGreater(eventData?.eventDate))
            {
                AddError("date", date, "Дата прекращения передачи информации ЮЛ_35.2 позднее даты наступления события eventDate");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                AddError("date", date, "Дата прекращения передачи информации ЮЛ_35.2 раньше даты совершения сделки ЮЛ_10.4");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_35_StopSend_Type block, EventData eventData, IUL_10_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}
