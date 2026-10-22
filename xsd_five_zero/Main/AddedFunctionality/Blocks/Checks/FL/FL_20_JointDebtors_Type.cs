namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_20_JointDebtors_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_20_JointDebtors;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData)
        {
            ErrorData errorData = new(blockName, eventData);

            //if (exist == true && num < 2)
            //{
            //    errorData.AddError("num", num, "Число солидарных должников ФЛ_20.2 должно быть не меньше двух");
            //}
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_20_JointDebtors_Type block, IEventData eventData)
        {
            return [];
            //return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
