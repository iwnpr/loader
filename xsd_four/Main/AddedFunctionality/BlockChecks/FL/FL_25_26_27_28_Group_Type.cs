using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_25_26_27_28_Group_Type : Block_Type
    {
        public FL_25_26_27_28_Group_Type()
        {
            blockName = refBlockNameType.FL_25_26_27_28_Group;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData,
            FL_18_Deal_Type dealBlock = null, FL_29_MonthlyPayment_Type monthlyPaymentBlock = null, FL_56_Participation_Type participationBlock = null)
        {
            InitCheck(eventData);

            if (calcDate.DateGreater(eventData?.eventDate))
            {
                AddError("calcDate", calcDate, "Дата расчета ФЛ_25.8, 28.13 позднее даты наступления события eventDate");
            }

            if (calcDate.DateNotEqual(monthlyPaymentBlock?.calcDate))
            {
                AddError("calcDate", calcDate, "Дата расчета ФЛ_25.8, 28.13 не равна дате расчета величины среднемесячного платежа ФЛ_29.2");
            }

            if (calcDate.DateLess(dealBlock?.startDate))
            {
                AddError("calcDate", calcDate, "Дата расчета ФЛ_25.8, 28.13 раньше даты возникновения обязательства субъекта ФЛ_18.19");
            }

            if (eventData.EventName == refEventNameType.FL_Event_2_5 && exist != false)
            {
                AddError("calcDate", calcDate, "Признак наличия задолженности ФЛ_25.1 не равен 0");
            }

            #region FL_25_Debt
            if (exist == true)
            {
                if (FL_25_Debt.debtSum != FL_25_Debt.debtMainSum + FL_25_Debt.debtPercentSum + FL_25_Debt.debtOtherSum)
                {
                    AddError("debtSum", FL_25_Debt.debtSum, "Сумма задолженности ФЛ_25.4 должна быть равна сумме значений показателей ФЛ_25.5, ФЛ_25.6 и ФЛ_25.7");
                }
                if (FL_25_Debt.debtSum != (FL_26_DebtDue.debtDueSum ?? 0m) + (FL_27_DebtOverdue.debtOverdueSum ?? 0m))
                {
                    AddError("debtSum", FL_25_Debt.debtSum, "Сумма задолженности ФЛ_25.4 должна быть равна сумме значений показателей ФЛ_26.3 и ФЛ_27.3");
                }

                if (FL_25_Debt.debtMainSum > FL_25_Debt.debtSum)
                {
                    AddError("debtMainSum", FL_25_Debt.debtMainSum, "Сумма задолженности по основному долгу ФЛ_25.5 больше суммы задолженности ФЛ_25.4");
                }
                if (FL_25_Debt.debtMainSum != (FL_26_DebtDue.debtDueMainSum ?? 0m) + (FL_27_DebtOverdue.debtOverdueMainSum ?? 0))
                {
                    AddError("debtMainSum", FL_25_Debt.debtMainSum, "Сумма задолженности по основному долгу ФЛ_25.5 должна быть равна сумме значений показателей ФЛ_26.4 и ФЛ_27.4");
                }

                if (FL_25_Debt.debtPercentSum > FL_25_Debt.debtSum)
                {
                    AddError("debtPercentSum", FL_25_Debt.debtPercentSum, "Сумма задолженности по процентам ФЛ_25.6 больше суммы задолженности ФЛ_25.4");
                }
                if (FL_25_Debt.debtPercentSum != (FL_26_DebtDue.debtDuePercentSum ?? 0m) + (FL_27_DebtOverdue.debtOverduePercentSum ?? 0m))
                {
                    AddError("debtPercentSum", FL_25_Debt.debtPercentSum, "Сумма задолженности по процентам ФЛ_25.6 должна быть равна сумме значений показателей ФЛ_26.5 и ФЛ_27.5");
                }

                if (FL_25_Debt.debtOtherSum > FL_25_Debt.debtSum)
                {
                    AddError("debtOtherSum", FL_25_Debt.debtOtherSum, "Сумма задолженности по иным требованиям ФЛ_25.7 больше суммы задолженности ФЛ_25.4");
                }
                if (FL_25_Debt.debtOtherSum != (FL_26_DebtDue.debtDueOtherSum ?? 0m) + (FL_27_DebtOverdue.debtOverdueOtherSum ?? 0m))
                {
                    AddError("debtOtherSum", FL_25_Debt.debtOtherSum, "Сумма задолженности по иным требованиям ФЛ_25.7 должна быть равна сумме значений показателей ФЛ_26.6 и ФЛ_27.6");
                }

                if (!CurrencyDictionary.Contains(FL_25_Debt.currency))
                {
                    AddError("currency", FL_25_Debt.currency, "Валюта задолженности ФЛ_25.10 не соответствует Общероссийскому классификатору валют");
                }
            }
            #endregion

            #region FL_26_DebtDue
            if (exist == true && FL_26_DebtDue.debtDueExist == true)
            {
                if (FL_26_DebtDue.debtDueStartDate.DateGreater(calcDate))
                {
                    AddError("debtDueStartDate", FL_26_DebtDue.debtDueStartDate, "Дата возникновения срочной задолженности ФЛ_26.1 позднее даты расчета ФЛ_25.8, 28.13");
                }
                if (FL_26_DebtDue.debtDueStartDate.DateGreater(eventData?.eventDate))
                {
                    AddError("debtDueStartDate", FL_26_DebtDue.debtDueStartDate, "Дата возникновения срочной задолженности ФЛ_26.1 позднее даты наступления события eventDate");
                }
                if (FL_26_DebtDue.debtDueStartDate.DateLess(dealBlock?.startDate))
                {
                    AddError("debtDueStartDate", FL_26_DebtDue.debtDueStartDate, "Дата возникновения срочной задолженности ФЛ_26.1 раньше даты возникновения обязательства субъекта ФЛ_18.19");
                }

                if (FL_26_DebtDue.debtDueSum != (FL_26_DebtDue.debtDueMainSum ?? 0m) + (FL_26_DebtDue.debtDuePercentSum ?? 0m) + (FL_26_DebtDue.debtDueOtherSum ?? 0m))
                {
                    AddError("debtDueSum", FL_26_DebtDue.debtDueSum, "Сумма срочной задолженности ФЛ_26.3 должна быть равна сумме значений показателей ФЛ_26.4, ФЛ_26.5 и ФЛ_26.6");
                }

                if (FL_26_DebtDue.debtDueMainSum != null && FL_26_DebtDue.debtDueMainSum.DecimalGreater(FL_26_DebtDue.debtDueSum))
                {
                    AddError("debtDueMainSum", FL_26_DebtDue.debtDueMainSum, "Сумма срочной задолженности по основному долгу ФЛ_26.4 больше суммы срочной задолженности ФЛ_26.3");
                }

                if (FL_26_DebtDue.debtDuePercentSum != null && FL_26_DebtDue.debtDuePercentSum.DecimalGreater(FL_26_DebtDue.debtDueSum))
                {
                    AddError("debtDuePercentSum", FL_26_DebtDue.debtDuePercentSum, "Сумма срочной задолженности по процентам ФЛ_26.5 больше суммы срочной задолженности ФЛ_26.3");
                }

                if (FL_26_DebtDue.debtDueOtherSum.DecimalGreater(FL_26_DebtDue.debtDueSum))
                {
                    AddError("debtDueOtherSum", FL_26_DebtDue.debtDueOtherSum, "Сумма срочной задолженности по иным требованиям ФЛ_26.6 больше суммы срочной задолженности ФЛ_26.3");
                }
            }
            #endregion

            #region FL_27_DebtOverdue
            if (FL_27_DebtOverdue.missFact == true)
            {
                if (FL_27_DebtOverdue.debtOverdueSum != (FL_27_DebtOverdue.debtOverdueMainSum ?? 0) + (FL_27_DebtOverdue.debtOverduePercentSum ?? 0) + (FL_27_DebtOverdue.debtOverdueOtherSum ?? 0))
                {
                    AddError("debtOverdueSum", FL_27_DebtOverdue.debtOverdueSum, "Сумма просроченной задолженности ФЛ_27.3 должна быть равна сумме значений по показателям ФЛ_27.4, ФЛ_27.5 и ФЛ_27.6");
                }
                if (FL_27_DebtOverdue.missDuration > 0 && FL_27_DebtOverdue.debtOverdueSum <= 0)
                {
                    AddError("debtOverdueSum", FL_27_DebtOverdue.debtOverdueSum, "Сумма просроченной задолженности ФЛ_27.3 должна быть больше «0»");
                }

                if (FL_27_DebtOverdue.debtOverdueMainSum != null && FL_27_DebtOverdue.debtOverdueMainSum > FL_27_DebtOverdue.debtOverdueSum)
                {
                    AddError("debtOverdueMainSum", FL_27_DebtOverdue.debtOverdueMainSum, "Сумма просроченной задолженности по основному долгу ФЛ_27.4 больше суммы просроченной задолженности ФЛ_27.3");
                }

                if (FL_27_DebtOverdue.debtOverduePercentSum != null && FL_27_DebtOverdue.debtOverduePercentSum > FL_27_DebtOverdue.debtOverdueSum)
                {
                    AddError("debtOverduePercentSum", FL_27_DebtOverdue.debtOverduePercentSum, "Сумма просроченной задолженности по процентам ФЛ_27.5 больше суммы просроченной задолженности ФЛ_27.3");
                }

                if (FL_27_DebtOverdue.debtOverdueOtherSum == null)
                {
                    AddError("debtOverdueOtherSum", null, "Отсутствует сумма просроченной задолженности по иным требованиям ФЛ_27.6");
                }
                if (FL_27_DebtOverdue.debtOverdueOtherSum != null && FL_27_DebtOverdue.debtOverdueOtherSum > FL_27_DebtOverdue.debtOverdueSum)
                {
                    AddError("debtOverdueOtherSum", FL_27_DebtOverdue.debtOverdueOtherSum, "Сумма просроченной задолженности по иным требованиям ФЛ_27.6 больше суммы просроченной задолженности ФЛ_27.3");
                }

                if (FL_27_DebtOverdue.debtOverdueStartDate.DateGreater(calcDate))
                {
                    AddError("debtOverdueStartDate", FL_27_DebtOverdue.debtOverdueStartDate, "Дата возникновения просроченной задолженности ФЛ_27.1 позднее даты расчета ФЛ_25.8, 28.13");
                }
                if (FL_27_DebtOverdue.debtOverdueStartDate.DateLess(dealBlock?.startDate))
                {
                    AddError("debtOverdueStartDate", FL_27_DebtOverdue.debtOverdueOtherSum, "Дата возникновения просроченной задолженности ФЛ_27.1 раньше даты возникновения обязательства субъекта ФЛ_18.19");
                }
                if (FL_27_DebtOverdue.debtOverdueStartDate.DateGreater(eventData?.eventDate))
                {
                    AddError("debtOverdueStartDate", FL_27_DebtOverdue.debtOverdueOtherSum, "Дата возникновения просроченной задолженности ФЛ_27.1 позднее даты наступления события eventDate");
                }

                if (FL_27_DebtOverdue.debtOverdueMainSum > 0 && FL_27_DebtOverdue.mainMissDate == null)
                {
                    AddError("mainMissDate", null, "Отсутствует дата последнего пропущенного платежа по основному долгу ФЛ_27.8");
                }
                if (FL_27_DebtOverdue.mainMissDate.DateGreater(eventData?.eventDate))
                {
                    AddError("mainMissDate", FL_27_DebtOverdue.mainMissDate, "Дата последнего пропущенного платежа по основному долгу ФЛ_27.8 позднее даты наступления события eventDate");
                }
                if (FL_27_DebtOverdue.mainMissDate.DateLess(dealBlock?.startDate))
                {
                    AddError("mainMissDate", FL_27_DebtOverdue.mainMissDate, "Дата последнего пропущенного платежа по основному долгу ФЛ_27.8 раньше даты возникновения обязательства субъекта ФЛ_18.19");
                }

                if (FL_27_DebtOverdue.debtOverduePercentSum > 0 && FL_27_DebtOverdue.percentMissDate == null)
                {
                    AddError("percentMissDate", null, "Отсутствует дата последнего пропущенного платежа по процентам ФЛ_27.9");
                }
                if (FL_27_DebtOverdue.percentMissDate.DateGreater(eventData?.eventDate))
                {
                    AddError("percentMissDate", FL_27_DebtOverdue.percentMissDate, "Дата последнего пропущенного платежа по процентам ФЛ_27.9 позднее даты наступления события eventDate");
                }
                if (FL_27_DebtOverdue.percentMissDate.DateLess(dealBlock?.startDate))
                {
                    AddError("percentMissDate", FL_27_DebtOverdue.percentMissDate, "Дата последнего пропущенного платежа по процентам ФЛ_27.9 раньше даты возникновения обязательства субъекта ФЛ_18.19");
                }

                if (FL_27_DebtOverdue.debtOverdueSum > 0 && FL_27_DebtOverdue.missDuration <= 0)
                {
                    AddError("missDuration", FL_27_DebtOverdue.missDuration, "Продолжительность просрочки ФЛ_27.11 должна быть больше «0»");
                }
                if (participationBlock?.overdueExist == true && FL_27_DebtOverdue.missDuration <= 90)
                {
                    AddError("missDuration", FL_27_DebtOverdue.missDuration, "Продолжительность просрочки ФЛ_27.11 должна быть больше «90»");
                }
                if (FL_27_DebtOverdue.debtOverdueStartDate.IsNotNull() &&
                    FL_27_DebtOverdue.missDuration > 0 && FL_27_DebtOverdue.missDuration > ((calcDate - DateTime.Parse(FL_27_DebtOverdue.debtOverdueStartDate)).TotalDays + 1))
                {
                    AddError("missDuration", FL_27_DebtOverdue.missDuration, "Продолжительность просрочки ФЛ_27.11 должна быть не больше количества дней между датой, указанной по показателю «Дата расчета» ФЛ_25.8, 28.13 и датой, указанной по показателю «Дата возникновения просроченной задолженности» ФЛ_27.1, увеличенной на 1 день");
                }
            }
            #endregion

            #region FL_28_Payment
            if (FL_28_Payment.paymentSum != FL_28_Payment.paymentMainSum + FL_28_Payment.paymentPercentSum + FL_28_Payment.paymentOtherSum)
            {
                AddError("paymentSum", FL_28_Payment.paymentSum, "Сумма последнего внесенного платежа ФЛ_28.2 должна быть равна сумме значений показателей ФЛ_28.3, ФЛ_28.4, ФЛ_28.5");
            }
            if ((FL_28_Payment.lastMissPaySum > 0 || FL_28_Payment.paySum24 > 0) && FL_28_Payment.paymentSum <= 0)
            {
                AddError("paymentSum", FL_28_Payment.paymentSum, "Сумма последнего внесенного платежа ФЛ_28.2 должна быть больше «0»");
            }

            if (FL_28_Payment.paymentMainSum > FL_28_Payment.paymentSum)
            {
                AddError("paymentMainSum", FL_28_Payment.paymentMainSum, "Сумма последнего внесенного платежа по основному долгу ФЛ_28.3 больше суммы последнего внесенного платежа ФЛ_28.2");
            }

            if (FL_28_Payment.paymentPercentSum > FL_28_Payment.paymentSum)
            {
                AddError("paymentPercentSum", FL_28_Payment.paymentPercentSum, "Сумма последнего внесенного платежа по процентам ФЛ_28.4 больше суммы последнего внесенного платежа ФЛ_28.2");
            }

            if (FL_28_Payment.paymentOtherSum > FL_28_Payment.paymentSum)
            {
                AddError("paymentOtherSum", FL_28_Payment.paymentOtherSum, "Сумма последнего внесенного платежа по иным требованиям ФЛ_28.5 больше суммы последнего внесенного платежа ФЛ_28.2");
            }

            if (FL_28_Payment.totalSum != FL_28_Payment.totalMainSum + FL_28_Payment.totalPercentSum + FL_28_Payment.totalOtherSum)
            {
                AddError("totalSum", null, "Сумма всех внесенных платежей по обязательству ФЛ_28.6 должна быть равна сумме значений показателей ФЛ_28.7, ФЛ_28.8, ФЛ_28.9");
            }
            if (FL_28_Payment.totalSum < FL_28_Payment.paymentSum)
            {
                AddError("totalSum", FL_28_Payment.totalSum, "Сумма всех внесенных платежей по обязательству ФЛ_28.6 меньше суммы последнего внесенного платежа ФЛ_28.2");
            }

            if (FL_28_Payment.totalMainSum > FL_28_Payment.totalSum)
            {
                AddError("totalMainSum", null, "Сумма внесенных платежей по основному долгу ФЛ_28.7 больше суммы всех внесенных платежей по обязательству ФЛ_28.6");
            }

            if (FL_28_Payment.totalPercentSum > FL_28_Payment.totalSum)
            {
                AddError("totalPercentSum", null, "Сумма внесенных платежей по процентам ФЛ_28.8 больше суммы всех внесенных платежей по обязательству ФЛ_28.6");
            }

            if (FL_28_Payment.totalOtherSum > FL_28_Payment.totalSum)
            {
                AddError("totalOtherSum", null, "Сумма внесенных платежей по иным требованиям ФЛ_28.9 больше суммы всех внесенных платежей по обязательству ФЛ_28.6");
            }

            if (FL_28_Payment.paymentSum > 0 && !FL_28_Payment.dateSpecified)
            {
                AddError("date", null, "Отсутствует дата последнего внесенного платежа ФЛ_28.1");
            }
            if (FL_28_Payment.paymentSum == 0 && FL_28_Payment.dateSpecified)
            {
                AddError("date", FL_28_Payment.paymentOtherSum, "Дата последнего внесенного платежа ФЛ_28.1 должна отсутствовать");
            }
            if (FL_28_Payment.dateSpecified && FL_28_Payment.date.DateGreater(eventData?.eventDate))
            {
                AddError("date", FL_28_Payment.date, "Дата последнего внесенного платежа ФЛ_28.1 позднее даты наступления события eventDate");
            }

            if (dealBlock?.code != 1 && FL_28_Payment.lastMissPaySumSpecified)
            {
                AddError("lastMissPaySum", null, "Сумма последнего внесенного платежа (части платежа) с просрочкой более 30 календарных дней ФЛ_28.14 должна отсутствовать");
            }
            if (dealBlock?.code != 1 && FL_28_Payment.paySum24Specified)
            {
                AddError("paySum24", null, "Сумма внесенных платежей за 24 календарных месяца, за исключением платежей с просрочкой более 30 календарных дней ФЛ_28.15 должна отсутствовать");
            }

            if (FL_28_Payment.payCurrency == null)
            {
                AddError("payCurrency", FL_28_Payment.payCurrency, "Отсутствует валюта внесенных платежей ФЛ_28.16");
            }
            if (!CurrencyDictionary.Contains(FL_28_Payment.payCurrency))
            {
                AddError("payCurrency", FL_28_Payment.payCurrency, "Валюта внесенных платежей ФЛ_28.16 не соответствует Общероссийскому классификатору валют");
            }
            #endregion

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_25_26_27_28_Group_Type block, EventData eventData,
            FL_18_Deal_Type dealBlock = null, FL_29_MonthlyPayment_Type monthlyPaymentBlock = null, FL_56_Participation_Type participationBlock = null)
        {
            return (block?.Check(eventData, dealBlock, monthlyPaymentBlock, participationBlock)).OrEmptyIfNull().ToList();
        }
    }
}
