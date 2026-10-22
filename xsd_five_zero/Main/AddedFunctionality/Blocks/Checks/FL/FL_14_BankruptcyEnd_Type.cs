using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_14_BankruptcyEnd_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_14_BankruptcyEnd;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, FL_3_Birth_Type? birthBlock, FL_13_Bankruptcy_Type[]? bankruptcyBlocks)
        {
            ErrorData errorData = new(blockName, eventData);

            if (bankruptcyBlocks != null && bankruptcyBlocks.All(x => x.bankruptcyFact == false) && bankruptcyEndFact != false)
            {
                errorData.AddError("bankruptcyEndFact_0/bankruptcyEndFact_1", null, "Признак завершения расчетов с кредиторами ФЛ_14.1 не равен «0»");
            }
            if (bankruptcyEndFact == false && resumeDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("resumeDate", resumeDate, "Дата восстановления обязательства ФЛ_14.5 позднее даты и времени наступления события eventDateTime");
            }
            if (bankruptcyEndFact == true)
            {
                if (completionDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("completionDate", completionDate, "Дата завершения расчетов с кредиторами ФЛ_14.2 позднее даты и времени наступления события eventDateTime");
                }
                if (completionDate.DateLess(birthBlock?.birthDate))
                {
                    errorData.AddError("completionDate", completionDate, "Дата завершения расчетов с кредиторами ФЛ_14.2 раньше даты рождения ФЛ_3.1");
                }
                if (settlementExist == true && settlementDate.DateLess(birthBlock?.birthDate))
                {
                    errorData.AddError("completionDate", completionDate, "Дата освобождения от исполнения требований кредиторов ФЛ_14.4 раньше даты рождения ФЛ_3.1");
                }
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_14_BankruptcyEnd_Type block, IEventData eventData, FL_3_Birth_Type? birthBlock = null, FL_13_Bankruptcy_Type[]? bankruptcyBlocks = null)
        {
            return [.. (block?.Check(eventData, birthBlock, bankruptcyBlocks)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this FL_14_BankruptcyEnd_Type block, IEventData eventData, ISubjectTitleData title, FL_13_Bankruptcy_Type[]? bankruptcyBlocks = null)
        {
            return Validate(block, eventData, title is SubjectTitleDataFL titleFL ? titleFL.FL_3_Birth : null, bankruptcyBlocks);
        }

    }
}
