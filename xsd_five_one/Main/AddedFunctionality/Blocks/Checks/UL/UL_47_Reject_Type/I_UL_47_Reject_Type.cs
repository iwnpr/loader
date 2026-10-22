using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public interface I_UL_47_Reject_Type : I_BlockType
    {
        public System.DateTime rejectDate { get; }
        public refReasonRefuseContractType[] rejectCode { get; }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_UL_45_Application_Type applicationBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (rejectDate < applicationBlock.applicationDate)
            {
                errorData.AddError("rejectDate", rejectDate, "Дата отказа ЮЛ_47.1 раньше даты обращения ЮЛ_45.5");
            }
            if (rejectDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("rejectDate", rejectDate, "Дата отказа ЮЛ_47.1 позднее даты и времени наступления события eventDateTime");
            }

            foreach (var code in rejectCode.Where(x => x.In(refReasonRefuseContractType.Item7, refReasonRefuseContractType.Item8)))
            {
                errorData.AddError("rejectCode", code, "Код причины отказа ЮЛ_47.2 равен «7» или «8»");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this I_UL_47_Reject_Type block, IEventData eventData, I_UL_45_Application_Type applicationBlock)
        {
            return [.. (block?.Check(eventData, applicationBlock)).OrEmptyIfNull()];
        }
    }
}

