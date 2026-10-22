using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_29_MonthlyPayment_Type : Block_Type
    {
        public FL_29_MonthlyPayment_Type()
        {
            blockName = refBlockNameType.FL_29_MonthlyPayment;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IFL_17_DealUid dealUidBlock = null, FL_25_26_27_28_Group_Type fl_25_28_block = null)
        {
            InitCheck(eventData);
            if (calcDate.DateGreater(eventData?.eventDate))
            {
                AddError("calcDate", calcDate, "Дата расчета величины среднемесячного платежа ФЛ_29.2 позднее даты наступления события eventDate");
            }
            if (calcDate.DateNotEqual(fl_25_28_block?.calcDate))
            {
                AddError("calcDate", calcDate, "Дата расчета величины среднемесячного платежа ФЛ_29.2 не равна дате расчета ФЛ_25.8, 28.13");
            }
            if (calcDate.DateLess(dealUidBlock?.openDate))
            {
                AddError("calcDate", calcDate, "Дата расчета величины среднемесячного платежа ФЛ_29.2 раньше даты совершения сделки ФЛ_17.4");
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_29_MonthlyPayment_Type block, EventData eventData, IFL_17_DealUid dealUidBlock = null, FL_18_Deal_Type dealBlock = null, FL_25_26_27_28_Group_Type fl_25_28_block = null)
        {
            var errorList = (block?.Check(eventData, dealUidBlock, fl_25_28_block)).OrEmptyIfNull().ToList();
            if (dealBlock?.consumerExist == true && dealBlock?.monetarySubjectExist == true && dealBlock?.purposeCode.OrEmptyIfNull().Contains(refLoanGoalType.Item20) == false && block == null)
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_29_MonthlyPayment, null, null, "Отсутствует блок 29. Величина среднемесячного платежа по договору займа (кредита) и дата ее расчета"));
            }
            return errorList;
        }
    }
}
