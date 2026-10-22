namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_42_1_PrevPersonAcquirer_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_42_1_PrevPersonAcquirer;
            }
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_42_1_PrevPersonAcquirer_Type block, IEventData eventData)
        {
            if (block != null)
            {
                return [new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_42_1_PrevPersonAcquirer, null, null, "Блок 42(1). Сведения о предыдущем кредиторе - физическом лице должен отсутствовать")];
            }
            else return [];
        }
    }
}