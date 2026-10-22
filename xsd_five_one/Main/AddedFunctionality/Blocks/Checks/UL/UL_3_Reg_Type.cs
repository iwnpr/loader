using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_3_Reg_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_3_Reg;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData? eventData, UL_2_Address_Type[]? addressBlocks, UL_4_Tax_TypeTaxNum_group_UL_4_Tax[]? taxBlocks)
        {
            ErrorData errorData = new(blockName, eventData);
            if (!taxBlocks.OrEmptyIfNull().Any(x => x.taxNum != null) && regNum == "-")
            {
                errorData.AddError("regNum", regNum, "Регистрационный номер ЮЛ_3.1 равен «-»");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_3_Reg_Type block, IEventData? eventData, UL_2_Address_Type[]? addressBlocks = null, UL_4_Tax_TypeTaxNum_group_UL_4_Tax[]? taxBlocks = null)
        {
            if (!taxBlocks.OrEmptyIfNull().Any() && block == null)
            {
                return [new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_3_Reg, "Отсутствует блок 3. Регистрационный номер")];
            }
            return [.. (block?.Check(eventData, addressBlocks, taxBlocks)).OrEmptyIfNull()];
        }
    }
}
