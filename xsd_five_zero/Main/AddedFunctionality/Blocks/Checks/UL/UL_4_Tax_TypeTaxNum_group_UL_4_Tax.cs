using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_4_Tax_TypeTaxNum_group_UL_4_Tax : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_4_Tax;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData? eventData, UL_3_Reg_Type? regBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (taxCode == refTaxpayerType.Item1 && !taxNum.DigitCheck(false, 10))
            {
                errorData.AddError("taxNum", taxNum, "Длина номера налогоплательщика ЮЛ_4.2 не равна 10 цифрам");
            }
            if (regBlock?.regNum == "-" && taxNum == null)
            {
                errorData.AddError("taxNum", taxNum, "Номер налогоплательщика ЮЛ_4.2 отсутствует");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_4_Tax_TypeTaxNum_group_UL_4_Tax block, IEventData? eventData, UL_3_Reg_Type? regBlock = null)
        {
            return [.. (block?.Check(eventData, regBlock)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<UL_4_Tax_TypeTaxNum_group_UL_4_Tax> blocks, IEventData? eventData, UL_3_Reg_Type? regBlock = null)
        {
            if (regBlock == null && !blocks.OrEmptyIfNull().Any())
            {
                return [new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_4_Tax, null, null, "Отсутствует блок 4. Номер налогоплательщика")];
            }

            List<INotificationOfAcceptanceError> errorList = [];
            foreach (var block in blocks.OrEmptyIfNull())
            {
                errorList.AddRange(block.Check(eventData, regBlock));
            }
            return errorList;
        }
    }
}
