using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_47_Reject_Type : Block_Type
    {
        public UL_47_Reject_Type()
        {
            blockName = refBlockNameType.UL_47_Reject;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, UL_45_Application_Type applicationBlock)
        {
            InitCheck(eventData);

            if (rejectDate < applicationBlock.applicationDate)
            {
                AddError("rejectDate", rejectDate, "Дата отказа ЮЛ_47.1 раньше даты обращения ЮЛ_45.5");
            }
            if (rejectDate.DateGreater(eventData?.eventDate))
            {
                AddError("rejectDate", rejectDate, "Дата отказа ЮЛ_47.1 позднее даты наступления события eventDate");
            }

            foreach (var code in rejectCode.Where(x => x == 7))
            {
                AddError("rejectDate", code, "Код причины отказа ЮЛ_47.2 равен «7»");
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_47_Reject_Type block, EventData eventData, UL_45_Application_Type applicationBlock)
        {
            return (block?.Check(eventData, applicationBlock)).OrEmptyIfNull().ToList();
        }
    }
}
