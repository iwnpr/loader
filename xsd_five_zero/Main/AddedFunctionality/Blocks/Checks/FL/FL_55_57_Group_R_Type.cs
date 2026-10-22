using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_55_57_Group_R_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_55_57_Group_R;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock)
        {
            List<INotificationOfAcceptanceError> errorList = [];

            errorList.AddRange(FL_55_Application_R.Validate(eventData, dealUidBlock));

            if (FL_57_Reject != null)
            {
                errorList.AddRange(FL_57_Reject.Validate(eventData, FL_55_Application_R));
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_55_57_Group_R_Type block, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }
    }
}
