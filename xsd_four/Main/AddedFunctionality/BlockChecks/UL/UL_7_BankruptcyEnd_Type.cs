using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_7_BankruptcyEnd_Type : Block_Type
    {
        public UL_7_BankruptcyEnd_Type()
        {
            blockName = refBlockNameType.UL_7_BankruptcyEnd;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, UL_6_Bankruptcy_Type[] bankruptcyBlocks = null)
        {
            InitCheck(eventData);
            if (bankruptcyBlocks.OrEmptyIfNull().Any(x => x.bankruptcyFact == false) && bankruptcyEndFact != false)
            {
                AddError("bankruptcyEndFact_0/bankruptcyEndFact_1", null, "Признак завершения расчетов с кредиторами ЮЛ_7.1 не равен «0»");
            }
            if (bankruptcyEndFact == true)
            {
                if (completionDate.DateGreater(eventData?.eventDate))
                {
                    AddError("completionDate", completionDate, "Дата завершения расчетов с кредиторами ЮЛ_7.2 позднее даты наступления события eventDate");
                }
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_7_BankruptcyEnd_Type block, EventData eventData, UL_6_Bankruptcy_Type[] bankruptcyBlocks = null)
        {
            return (block?.Check(eventData, bankruptcyBlocks)).OrEmptyIfNull().ToList();
        }
    }
}
