using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_45_StopSend_Type : Block_Type
    {
        public FL_45_StopSend_Type()
        {
            blockName = refBlockNameType.FL_45_StopSend;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (eventData.EventName == refEventNameType.FL_Event_2_11 && !code.In(3, 6))
            {
                AddError("code", code, "Код причины прекращения передачи информации ФЛ_45.1 не равен «3» или «6»");
            }
            if (eventData.EventName == refEventNameType.FL_Event_2_10 && code == 3)
            {
                AddError("code", code, "Код причины прекращения передачи информации ФЛ_45.1 равен «3»");
            }

            if (date.DateGreater(eventData?.eventDate))
            {
                AddError("date", date, "Дата прекращения передачи информации ФЛ_45.2 позднее даты наступления события eventDate");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                AddError("date", date, "Дата прекращения передачи информации ФЛ_45.2 раньше даты совершения сделки ФЛ_17.4");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_45_StopSend_Type block, EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}
