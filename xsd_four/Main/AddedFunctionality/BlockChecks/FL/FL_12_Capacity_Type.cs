using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_12_Capacity_Type : Block_Type
    {
        public FL_12_Capacity_Type()
        {
            blockName = refBlockNameType.FL_12_Capacity;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, FL_3_Birth_Type? birthBlock = null)
        {
            InitCheck(eventData);

            if (code.In("3", "4") && Incapacity_group_FL_12_Capacity == null)
            {
                AddError("Incapacity_group_FL_12_Capacity", null, "Отсутствуют параметры 12.2, 12.3 и 12.4");
            }
            if (!code.In("3", "4") && Incapacity_group_FL_12_Capacity != null)
            {
                AddError("Incapacity_group_FL_12_Capacity", null, "Параметры 12.2, 12.3 и 12.4 должны отсутствовать");
            }
            if (Incapacity_group_FL_12_Capacity != null)
            {
                if (Incapacity_group_FL_12_Capacity.incapacityDate.DateGreater(eventData?.eventDate))
                {
                    AddError("incapacityDate", Incapacity_group_FL_12_Capacity.incapacityDate, "Дата вступления в силу решения суда о признании недееспособным или об ограничении дееспособности ФЛ_12.2 позднее даты наступления события eventDate");
                }
                if (Incapacity_group_FL_12_Capacity.incapacityDate.DateLess(birthBlock?.birthDate))
                {
                    AddError("incapacityDate", Incapacity_group_FL_12_Capacity.incapacityDate, "Дата вступления в силу решения суда о признании недееспособным или об ограничении дееспособности ФЛ_12.2 позднее даты наступления события eventDate");
                }
            }
            if (code == "5" && LegalCapacity_group_FL_12_Capacity == null)
            {
                AddError("LegalCapacity_group_FL_12_Capacity", null, "Отсутствуют параметры 12.5, 12.6 и 12.7");
            }
            if (code != "5" && LegalCapacity_group_FL_12_Capacity != null)
            {
                AddError("LegalCapacity_group_FL_12_Capacity", null, "Параметры 12.5, 12.6 и 12.7 должны отсутствовать");
            }
            if (LegalCapacity_group_FL_12_Capacity != null)
            {

                if (LegalCapacity_group_FL_12_Capacity.legalCapacityDate.DateGreater(eventData?.eventDate))
                {
                    AddError("legalCapacityDate", LegalCapacity_group_FL_12_Capacity.legalCapacityDate, "Дата вступления в силу решения суда о признании дееспособным или об отмене ограничения дееспособности ФЛ_12.5 позднее даты наступления события eventDate");
                }
                if (LegalCapacity_group_FL_12_Capacity.legalCapacityDate.DateLess(birthBlock?.birthDate))
                {
                    AddError("legalCapacityDate", LegalCapacity_group_FL_12_Capacity.legalCapacityDate, "Дата вступления в силу решения суда о признании дееспособным или об отмене ограничения дееспособности ФЛ_12.5 раньше даты рождения ФЛ_3.1");
                }
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_12_Capacity_Type block, EventData eventData, FL_3_Birth_Type? birthBlock = null)
        {
            return (block?.Check(eventData, birthBlock)).OrEmptyIfNull().ToList();
        }
    }
}
