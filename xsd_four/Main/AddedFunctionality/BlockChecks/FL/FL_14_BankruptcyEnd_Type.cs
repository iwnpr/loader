using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_14_BankruptcyEnd_Type : Block_Type
    {
        public FL_14_BankruptcyEnd_Type()
        {
            blockName = refBlockNameType.FL_14_BankruptcyEnd;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, FL_3_Birth_Type? birthBlock = null, FL_13_Bankruptcy_Type[] bankruptcyBlocks = null)
        {
            InitCheck(eventData);
            if (bankruptcyBlocks.OrEmptyIfNull().Any(x => x.bankruptcyFact == false) && bankruptcyEndFact != false)
            {
                AddError("bankruptcyEndFact_0/bankruptcyEndFact_1", null, "Признак завершения расчетов с кредиторами ФЛ_14.1 не равен «0»");
            }
            if (bankruptcyEndFact == false && resumeDate.DateGreater(eventData?.eventDate))
            {
                AddError("resumeDate", resumeDate, "Дата восстановления обязательства ФЛ_14.5 позднее даты наступления события eventDate");
            }
            if (bankruptcyEndFact == true)
            {
                if (completionDate.DateGreater(eventData?.eventDate))
                {
                    AddError("completionDate", completionDate, "Дата завершения расчетов с кредиторами ФЛ_14.2 позднее даты наступления события eventDate");
                }
                if (completionDate.DateLess(birthBlock?.birthDate))
                {
                    AddError("completionDate", completionDate, "Дата завершения расчетов с кредиторами ФЛ_14.2 раньше даты рождения ФЛ_3.1");
                }
                if (settlementExist == true && settlementDate.DateLess(birthBlock?.birthDate))
                {
                    AddError("completionDate", completionDate, "Дата освобождения от исполнения требований кредиторов ФЛ_14.4 раньше даты рождения ФЛ_3.1");
                }
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_14_BankruptcyEnd_Type block, EventData eventData, FL_3_Birth_Type? birthBlock = null, FL_13_Bankruptcy_Type[] bankruptcyBlocks = null)
        {
            return (block?.Check(eventData, birthBlock, bankruptcyBlocks)).OrEmptyIfNull().ToList();
        }
    }
}
