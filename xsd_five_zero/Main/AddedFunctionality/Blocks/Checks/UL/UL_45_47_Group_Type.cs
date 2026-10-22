using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_45_47_Group_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_45_47_Group;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData)
        {
            List<INotificationOfAcceptanceError> errorList = [];

            errorList.AddRange(UL_45_Application.Validate(eventData));

            if (UL_47_Reject != null)
            {
                errorList.AddRange(UL_47_Reject.Validate(eventData, UL_45_Application));
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_45_47_Group_Type block, IEventData eventData)
        {
            return [.. (block?.Check(eventData)).OrEmptyIfNull()];
        }
    }
}
