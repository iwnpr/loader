using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public interface I_FL_57_Reject_Type : I_BlockType
    {
        public System.DateTime rejectDate { get; }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_55_Application_Type applicationBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (rejectDate.DateLess(applicationBlock?.applicationDate))
            {
                errorData.AddError("rejectDate", rejectDate, "Дата отказа ФЛ_57.1 раньше даты обращения ФЛ_55.5");
            }
            if (rejectDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("rejectDate", rejectDate, "Дата отказа ФЛ_57.1 позднее даты и времени наступления события eventDateTime");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this I_FL_57_Reject_Type block, IEventData eventData, I_FL_55_Application_Type applicationBlock)
        {
            return [.. (block?.Check(eventData, applicationBlock)).OrEmptyIfNull()];
        }
    }
}

