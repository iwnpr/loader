using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_13_JointDebtors_Type : Block_Type
    {
        public UL_13_JointDebtors_Type()
        {
            blockName = refBlockNameType.UL_13_JointDebtors;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            if (exist == true && num < 2)
            {
                AddError("num", num, "Число солидарных должников ЮЛ_13.2 должно быть не меньше двух");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_13_JointDebtors_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
