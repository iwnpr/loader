using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_38_ContractEnd_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_38_ContractEnd;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (date.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("date", date, "Дата фактического прекращения обязательства ФЛ_38.2 позднее даты и времени наступления события eventDateTime");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("date", date, "Дата фактического прекращения обязательства ФЛ_38.2 раньше даты совершения сделки ФЛ_17.4");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_38_ContractEnd_Type block, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }
    }
}
