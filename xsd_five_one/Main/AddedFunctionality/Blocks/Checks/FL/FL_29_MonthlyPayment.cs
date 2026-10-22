using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_29_MonthlyPayment_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_29_MonthlyPayment;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock, FL_25_26_27_28_Group_Type? fl_25_28_block)
        {
            ErrorData errorData = new(blockName, eventData);
            if (eventData.EventName == refEventNameType.FL_Event_2_5 && sum != "0")
            {
                errorData.AddError("sum", sum, "Величина среднемесячного платежа ФЛ_29.1 не равна «0»");
            }
            if (calcDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("calcDate", calcDate, "Дата расчета величины среднемесячного платежа ФЛ_29.2 позднее даты и времени наступления события eventDateTime");
            }
            if (calcDate.DateNotEqual(fl_25_28_block?.calcDate))
            {
                errorData.AddError("calcDate", calcDate, "Дата расчета величины среднемесячного платежа ФЛ_29.2 не равна дате расчета ФЛ_25.8, 28.13");
            }
            if (calcDate.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("calcDate", calcDate, "Дата расчета величины среднемесячного платежа ФЛ_29.2 раньше даты совершения сделки ФЛ_17.4");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_29_MonthlyPayment_Type block, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null, FL_18_Deal_Type? dealBlock = null, FL_25_26_27_28_Group_Type? fl_25_28_block = null)
        {
            var errorList = (block?.Check(eventData, dealUidBlock, fl_25_28_block)).OrEmptyIfNull().ToList();
            if (dealBlock?.consumerExist == true && dealBlock?.monetarySubjectExist == true && dealBlock?.purposeCode.OrEmptyIfNull().Contains(refLoanGoalType.Item20) == false && block == null)
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_29_MonthlyPayment, "Отсутствует блок 29. Величина среднемесячного платежа по договору займа (кредита) и дата ее расчета"));
            }
            return errorList;
        }
    }
}
