namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_43_1_ProbableFraudInfo_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_43_1_ProbableFraudInfo;
            }
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_43_1_ProbableFraudInfo_Type block, IEventData eventData)
        {
            if (block != null)
                return [new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_43_1_ProbableFraudInfo, null, null, "Блок 43(1). Сведения для предупреждения возможного мошенничества должен отсутствовать")];
            else
                return [];
        }
    }
}
