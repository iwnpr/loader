using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_51_1_PrevOrgAcquirer_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_51_1_PrevOrgAcquirer;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData)
        {
            ErrorData errorData = new(blockName, eventData);

            if (fullName.IsEmptyValueDash())
            {
                errorData.AddError("fullName", fullName, $"Полное наименование ФЛ_51(1).3 не может содержать \"-\"");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {

        public static List<INotificationOfAcceptanceError> Validate(this FL_51_1_PrevOrgAcquirer_Type block, IEventData eventData)
        {
            return [.. (block?.Check(eventData)).OrEmptyIfNull()];
        }
    }
}
