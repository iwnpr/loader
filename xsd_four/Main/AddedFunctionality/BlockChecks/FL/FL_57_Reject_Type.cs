using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_57_Reject_Type : Block_Type
    {
        public FL_57_Reject_Type()
        {
            blockName = refBlockNameType.FL_57_Reject;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, FL_55_Application_Type applicationBlock = null)
        {
            InitCheck(eventData);

            if (rejectDate.DateLess(applicationBlock.applicationDate))
            {
                AddError("rejectDate", rejectDate, "Дата отказа ФЛ_57.1 раньше даты обращения ФЛ_55.5");
            }
            if (rejectDate.DateGreater(eventData?.eventDate))
            {
                AddError("rejectDate", rejectDate, "Дата отказа ФЛ_57.1 позднее даты наступления события eventDate");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_57_Reject_Type block, EventData eventData, FL_55_Application_Type applicationBlock = null)
        {
            return (block?.Check(eventData, applicationBlock)).OrEmptyIfNull().ToList();
        }
    }
}
