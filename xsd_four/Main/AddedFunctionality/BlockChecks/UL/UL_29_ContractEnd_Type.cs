using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_29_ContractEnd_Type : Block_Type
    {
        public UL_29_ContractEnd_Type()
        {
            blockName = refBlockNameType.UL_29_ContractEnd;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IUL_10_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (date.DateGreater(eventData?.eventDate))
            {
                AddError("date", date, "Дата фактического прекращения обязательства ЮЛ_29.2 позднее даты наступления события eventDate");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                AddError("date", date, "Дата фактического прекращения обязательства ЮЛ_29.2 раньше даты совершения сделки ЮЛ_10.4");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_29_ContractEnd_Type block, EventData eventData, IUL_10_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}
