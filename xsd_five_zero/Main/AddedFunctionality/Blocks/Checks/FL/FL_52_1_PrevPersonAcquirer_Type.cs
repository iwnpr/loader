namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_52_1_PrevPersonAcquirer_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_52_1_PrevPersonAcquirer;
            }
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_52_1_PrevPersonAcquirer_Type block, IEventData eventData)
        {
            if (block != null)
            {
                return [new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_52_1_PrevPersonAcquirer, null, null, "Блок 52(1). Сведения о предыдущем кредиторе - физическом лице должен отсутствовать")];
            }
            else return [];
        }
    }
}