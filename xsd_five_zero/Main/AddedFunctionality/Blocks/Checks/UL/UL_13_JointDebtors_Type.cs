namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_13_JointDebtors_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_13_JointDebtors;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData)
        {
            ErrorData errorData = new(blockName, eventData);

            //if (exist == true && num < 2)
            //{
            //    errorData.AddError("num", num, "Число солидарных должников ЮЛ_13.2 должно быть не меньше двух");
            //}
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_13_JointDebtors_Type block, IEventData eventData)
        {
            return [];
            //return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
