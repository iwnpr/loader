using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_55_57_Group_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_55_57_Group;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock)
        {
            List<INotificationOfAcceptanceError> errorList = [];

            errorList.AddRange(FL_55_Application.Validate(eventData, dealUidBlock));

            if (FL_57_Reject != null)
            {
                errorList.AddRange(FL_57_Reject.Validate(eventData, FL_55_Application));
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_55_57_Group_Type block, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }
    }
}
