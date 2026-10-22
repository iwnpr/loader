using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_11_IndividualEntrepreneur_Type : Block_Type
    {
        public FL_11_IndividualEntrepreneur_Type()
        {
            blockName = refBlockNameType.FL_11_IndividualEntrepreneur;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, ISource source, FL_6_Tax_Type[] taxBlocks)
        {
            InitCheck(eventData);

            if (regFact == true)
            {
                if (regNum == null)
                {
                    errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_6_Tax, "regNum", regNum, "Отсутствует регистрационный номер ФЛ_6.3", true));
                }

                if (!regNum.DigitCheck(false, 15))
                {
                    AddError("regNum", regNum, "Длина регистрационного номера ФЛ_11.2 не равна 15 цифр");
                }
                if (!eventData.EventName.In(refEventNameType.FL_Event_3_1, refEventNameType.FL_Event_4_1)
                    && taxBlocks.OrEmptyIfNull().Any(x => regNum != x.regNum))
                {
                    AddError("regNum", regNum, "Регистрационный номер ФЛ_11.2  не равен регистрационному номеру ФЛ_6.3");
                }
                if (date.DateGreater(source.sourceCreditInfoDate))
                {
                    AddError("date", date, "Дата регистрации индивидуального предпринимателя ФЛ_11.3 позднее даты формирования кредитной информации");
                }
            }
            if (regFact == false)
            {
                foreach (var taxBlock in taxBlocks.OrEmptyIfNull().Where(x => x.regNum != null))
                {
                    errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_6_Tax, "regNum", taxBlock.regNum, "Регистрационный номер ФЛ_6.3 должен отсутствовать", true));
                }

                if (endDate.DateGreater(eventData?.eventDate))
                {
                    AddError("endDate", endDate, "Дата прекращения статуса индивидуального предпринимателя ФЛ_11.4 позднее даты наступления  события eventDate");
                }
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_11_IndividualEntrepreneur_Type block, EventData eventData, ISource source, FL_6_Tax_Type[] taxBlocks)
        {
            return (block?.Check(eventData, source, taxBlocks)).OrEmptyIfNull().ToList();
        }
    }
}
