using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_4_Tax_TypeTaxNum_group_UL_4_Tax : Block_Type
    {
        public UL_4_Tax_TypeTaxNum_group_UL_4_Tax()
        {
            blockName = refBlockNameType.UL_4_Tax;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, UL_3_Reg_Type regBlock = null)
        {
            InitCheck(eventData);

            if (taxCode == 1 && !taxNum.DigitCheck(false, 10))
            {
                AddError("taxNum", taxNum, "Длина номера налогоплательщика ЮЛ_4.2 не равна 10 цифрам");
            }
            if (regBlock?.regNum == "-" && taxNum == null)
            {
                AddError("taxNum", taxNum, "Номер налогоплательщика ЮЛ_4.2 отсутствует");
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_4_Tax_TypeTaxNum_group_UL_4_Tax block, EventData eventData, UL_3_Reg_Type regBlock = null)
        {
            return (block?.Check(eventData, regBlock)).OrEmptyIfNull().ToList();
        }
    }
}
