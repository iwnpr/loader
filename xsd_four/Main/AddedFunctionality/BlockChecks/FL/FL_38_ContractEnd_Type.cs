using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_38_ContractEnd_Type : Block_Type
    {
        public FL_38_ContractEnd_Type()
        {
            blockName = refBlockNameType.FL_38_ContractEnd;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (date.DateGreater(eventData?.eventDate))
            {
                AddError("date", date, "Дата фактического прекращения обязательства ФЛ_38.2 позднее даты наступления события eventDate");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                AddError("date", date, "Дата фактического прекращения обязательства ФЛ_38.2 раньше даты совершения сделки ФЛ_17.4");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_38_ContractEnd_Type block, EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}
