using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_3_Reg_Type : Block_Type
    {
        public UL_3_Reg_Type()
        {
            blockName = refBlockNameType.UL_3_Reg;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, UL_2_Address_Type[] addressBlocks = null, UL_4_Tax_TypeTaxNum_group_UL_4_Tax[] taxBlocks = null)
        {
            InitCheck(eventData);
            if (!taxBlocks.OrEmptyIfNull().Any(x => x.taxNum != null) && regNum == "-")
            {
                AddError("regNum", regNum, "Регистрационный номер ЮЛ_3.1 равен «-»");
            }

            if (addressBlocks.OrEmptyIfNull().Any(x => x.foreignerFact == false) && !regNum.DigitCheck(true, 13))
            {
                AddError("regNum", regNum, "Длина регистрационного номера ЮЛ_3.1 не равна 13 цифрам");
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_3_Reg_Type block, EventData eventData, UL_2_Address_Type[] addressBlocks = null, UL_4_Tax_TypeTaxNum_group_UL_4_Tax[] taxBlocks = null)
        {
            return (block?.Check(eventData, addressBlocks, taxBlocks)).OrEmptyIfNull().ToList();
        }
    }
}
