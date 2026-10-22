using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_12_Capacity_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_12_Capacity;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, FL_3_Birth_Type? birthBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (code.In("3", "4") && Incapacity_group_FL_12_Capacity == null)
            {
                errorData.AddError("Incapacity_group_FL_12_Capacity", null, "Отсутствуют параметры 12.2, 12.3 и 12.4");
            }
            if (!code.In("3", "4") && Incapacity_group_FL_12_Capacity != null)
            {
                errorData.AddError("Incapacity_group_FL_12_Capacity", null, "Параметры 12.2, 12.3 и 12.4 должны отсутствовать");
            }
            if (Incapacity_group_FL_12_Capacity != null)
            {
                if (Incapacity_group_FL_12_Capacity.incapacityDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("incapacityDate", Incapacity_group_FL_12_Capacity.incapacityDate, "Дата вступления в силу решения суда о признании недееспособным или об ограничении дееспособности ФЛ_12.2 позднее даты и времени наступления события eventDateTime");
                }
                if (Incapacity_group_FL_12_Capacity.incapacityDate.DateLess(birthBlock?.birthDate))
                {
                    errorData.AddError("incapacityDate", Incapacity_group_FL_12_Capacity.incapacityDate, "Дата вступления в силу решения суда о признании недееспособным или об ограничении дееспособности ФЛ_12.2 раньше даты рождения ФЛ_3.1");
                }
            }
            if (code == "5" && LegalCapacity_group_FL_12_Capacity == null)
            {
                errorData.AddError("LegalCapacity_group_FL_12_Capacity", null, "Отсутствуют параметры 12.5, 12.6 и 12.7");
            }
            if (code != "5" && LegalCapacity_group_FL_12_Capacity != null)
            {
                errorData.AddError("LegalCapacity_group_FL_12_Capacity", null, "Параметры 12.5, 12.6 и 12.7 должны отсутствовать");
            }
            if (LegalCapacity_group_FL_12_Capacity != null)
            {

                if (LegalCapacity_group_FL_12_Capacity.legalCapacityDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("legalCapacityDate", LegalCapacity_group_FL_12_Capacity.legalCapacityDate, "Дата вступления в силу решения суда о признании дееспособным или об отмене ограничения дееспособности ФЛ_12.5 позднее даты и времени наступления события eventDateTime");
                }
                if (LegalCapacity_group_FL_12_Capacity.legalCapacityDate.DateLess(birthBlock?.birthDate))
                {
                    errorData.AddError("legalCapacityDate", LegalCapacity_group_FL_12_Capacity.legalCapacityDate, "Дата вступления в силу решения суда о признании дееспособным или об отмене ограничения дееспособности ФЛ_12.5 раньше даты рождения ФЛ_3.1");
                }
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_12_Capacity_Type block, IEventData eventData, FL_3_Birth_Type? birthBlock = null)
        {
            return [.. (block?.Check(eventData, birthBlock)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this FL_12_Capacity_Type block, IEventData eventData, ISubjectTitleData title)
        {
            return Validate(block, eventData, title is SubjectTitleDataFL titleFL ? titleFL.FL_3_Birth : null);
        }
    }
}
