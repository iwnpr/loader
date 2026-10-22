using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_11_IndividualEntrepreneur_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_11_IndividualEntrepreneur;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, ISource source, FL_6_Tax_Type[]? taxBlocks)
        {
            ErrorData errorData = new(blockName, eventData);

            if (regFact == true)
            {
                foreach (var taxBlock in taxBlocks.OrEmptyIfNull().Where(x => x.regNum == null))
                {
                    errorData.AddError(refBlockNameType.FL_6_Tax, "regNum", taxBlock.regNum, "Отсутствует регистрационный номер ФЛ_6.3", true);
                }

                if (!eventData.EventName.In(refEventNameType.FL_Event_3_1, refEventNameType.FL_Event_4_1)
                    && taxBlocks.OrEmptyIfNull().Any(x => regNum != x.regNum))
                {
                    errorData.AddError("regNum", regNum, "Регистрационный номер ФЛ_11.2 не равен регистрационному номеру ФЛ_6.3", true);
                }
                if (date.DateGreater(source.sourceCreditInfoDate))
                {
                    errorData.AddError("date", date, "Дата регистрации индивидуального предпринимателя ФЛ_11.3 позднее даты формирования кредитной информации");
                }
            }
            if (regFact == false)
            {
                foreach (var taxBlock in taxBlocks.OrEmptyIfNull().Where(x => x.regNum != null))
                {
                    errorData.AddError(refBlockNameType.FL_6_Tax, "regNum", taxBlock.regNum, "Регистрационный номер ФЛ_6.3 должен отсутствовать", true);
                }

                if (endDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("endDate", endDate, "Дата прекращения статуса индивидуального предпринимателя ФЛ_11.4 позднее даты наступления  события eventDateTime");
                }
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_11_IndividualEntrepreneur_Type block, IEventData eventData, ISource source, FL_6_Tax_Type[]? taxBlocks = null)
        {
            return [.. (block?.Check(eventData, source, taxBlocks)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this FL_11_IndividualEntrepreneur_Type block, IEventData eventData, ISource source, ISubjectTitleData title)
        {
            return Validate(block, eventData, source, title is SubjectTitleDataFL titleFL ? titleFL.FL_6_Tax : null);
        }
    }
}
