using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_7_BankruptcyEnd_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_7_BankruptcyEnd;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, UL_6_Bankruptcy_Type[]? bankruptcyBlocks)
        {
            ErrorData errorData = new(blockName, eventData);
            if (bankruptcyBlocks != null && bankruptcyBlocks.All(x => x.bankruptcyFact == false) && bankruptcyEndFact != false)
            {
                errorData.AddError("bankruptcyEndFact_0/bankruptcyEndFact_1", null, "Признак завершения расчетов с кредиторами ЮЛ_7.1 не равен «0»");
            }
            if (bankruptcyEndFact == true)
            {
                if (completionDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("completionDate", completionDate, "Дата завершения расчетов с кредиторами ЮЛ_7.2 позднее даты и времени наступления события eventDateTime");
                }
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_7_BankruptcyEnd_Type block, IEventData eventData, UL_6_Bankruptcy_Type[]? bankruptcyBlocks = null)
        {
            return [.. (block?.Check(eventData, bankruptcyBlocks)).OrEmptyIfNull()];
        }
    }
}
