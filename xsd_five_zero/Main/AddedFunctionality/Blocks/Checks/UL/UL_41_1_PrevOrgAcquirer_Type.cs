using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_41_1_PrevOrgAcquirer_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_41_1_PrevOrgAcquirer;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData)
        {
            ErrorData errorData = new(blockName, eventData);

            if (fullName.IsEmptyValueDash())
            {
                errorData.AddError("fullName", fullName, $"Полное наименование ЮЛ_41(1).3 не может содержать \"-\"");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_41_1_PrevOrgAcquirer_Type block, IEventData eventData)
        {
            return [.. (block?.Check(eventData)).OrEmptyIfNull()];
        }
    }
}
