using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_45_StopSend_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_45_StopSend;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (eventData.EventName == refEventNameType.FL_Event_2_11 && !code.In(refReasonStopTransferInformationType.Item3, refReasonStopTransferInformationType.Item6))
            {
                errorData.AddError("code", code, "Код причины прекращения передачи информации ФЛ_45.1 не равен «3» или «6»");
            }
            if (eventData.EventName == refEventNameType.FL_Event_2_10 && code == refReasonStopTransferInformationType.Item3)
            {
                errorData.AddError("code", code, "Код причины прекращения передачи информации ФЛ_45.1 равен «3»");
            }

            if (date.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("date", date, "Дата прекращения передачи информации ФЛ_45.2 позднее даты и времени наступления события eventDateTime");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("date", date, "Дата прекращения передачи информации ФЛ_45.2 раньше даты совершения сделки ФЛ_17.4");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_45_StopSend_Type block, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }
    }
}
