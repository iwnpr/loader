using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_45_47_Group_R_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_45_47_Group_R;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData)
        {
            List<INotificationOfAcceptanceError> errorList = [];

            errorList.AddRange(UL_45_Application_R.Validate(eventData));

            if (UL_47_Reject != null)
            {
                errorList.AddRange(UL_47_Reject.Validate(eventData, UL_45_Application_R));
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_45_47_Group_R_Type block, IEventData eventData)
        {
            return [.. (block?.Check(eventData)).OrEmptyIfNull()];
        }
    }
}
