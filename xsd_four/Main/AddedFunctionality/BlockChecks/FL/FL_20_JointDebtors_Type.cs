using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_20_JointDebtors_Type : Block_Type
    {
        public FL_20_JointDebtors_Type()
        {
            blockName = refBlockNameType.FL_20_JointDebtors;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            if (exist == true && num < 2)
            {
                AddError("num", num, "Число солидарных должников ФЛ_20.2 должно быть не меньше двух");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_20_JointDebtors_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
