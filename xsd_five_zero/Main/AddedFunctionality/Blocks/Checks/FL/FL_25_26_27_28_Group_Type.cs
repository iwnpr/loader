using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_25_26_27_28_Group_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_25_26_27_28_Group;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, FL_18_Deal_Type? dealBlock, FL_29_MonthlyPayment_Type? monthlyPaymentBlock, FL_54_Accounting_Type? accountingBlock, FL_56_Participation_Type? participationBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (calcDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("calcDate", calcDate, "Дата расчета ФЛ_25.8, 28.13 позднее даты и времени наступления события eventDateTime");
            }

            if (calcDate.DateNotEqual(monthlyPaymentBlock?.calcDate))
            {
                errorData.AddError("calcDate", calcDate, "Дата расчета ФЛ_25.8, 28.13 не равна дате расчета величины среднемесячного платежа ФЛ_29.2");
            }

            if (calcDate.DateNotEqual(accountingBlock?.calcDate))
            {
                errorData.AddError("calcDate", calcDate, "Дата расчета ФЛ_25.8, 28.13 не равна дате расчета ФЛ_54.7");
            }

            if (calcDate.DateLess(dealBlock?.startDate))
            {
                errorData.AddError("calcDate", calcDate, "Дата расчета ФЛ_25.8, 28.13 раньше даты возникновения обязательства субъекта ФЛ_18.19");
            }

            if (eventData.EventName == refEventNameType.FL_Event_2_5 && exist != false)
            {
                errorData.AddError("calcDate", calcDate, "Признак наличия задолженности ФЛ_25.1 не равен 0");
            }

            if (exist == true)
            {
                #region FL_25_Debt
                if (FL_25_Debt!.debtSum != FL_25_Debt.debtMainSum + FL_25_Debt.debtPercentSum + FL_25_Debt.debtOtherSum)
                {
                    errorData.AddError("debtSum", FL_25_Debt.debtSum, "Сумма задолженности ФЛ_25.4 должна быть равна сумме значений показателей ФЛ_25.5, ФЛ_25.6 и ФЛ_25.7");
                }
                if (FL_25_Debt.debtSum != (FL_26_DebtDue!.debtDueSum ?? 0m) + (FL_27_DebtOverdue.debtOverdueSum ?? 0m))
                {
                    errorData.AddError("debtSum", FL_25_Debt.debtSum, "Сумма задолженности ФЛ_25.4 должна быть равна сумме значений показателей ФЛ_26.3 и ФЛ_27.3");
                }

                if (FL_25_Debt.debtMainSum > FL_25_Debt.debtSum)
                {
                    errorData.AddError("debtMainSum", FL_25_Debt.debtMainSum, "Сумма задолженности по основному долгу ФЛ_25.5 больше суммы задолженности ФЛ_25.4");
                }
                if (FL_25_Debt.debtMainSum != (FL_26_DebtDue.debtDueMainSum ?? 0m) + (FL_27_DebtOverdue.debtOverdueMainSum ?? 0))
                {
                    errorData.AddError("debtMainSum", FL_25_Debt.debtMainSum, "Сумма задолженности по основному долгу ФЛ_25.5 должна быть равна сумме значений показателей ФЛ_26.4 и ФЛ_27.4");
                }

                if (FL_25_Debt.debtPercentSum > FL_25_Debt.debtSum)
                {
                    errorData.AddError("debtPercentSum", FL_25_Debt.debtPercentSum, "Сумма задолженности по процентам ФЛ_25.6 больше суммы задолженности ФЛ_25.4");
                }
                if (FL_25_Debt.debtPercentSum != (FL_26_DebtDue.debtDuePercentSum ?? 0m) + (FL_27_DebtOverdue.debtOverduePercentSum ?? 0m))
                {
                    errorData.AddError("debtPercentSum", FL_25_Debt.debtPercentSum, "Сумма задолженности по процентам ФЛ_25.6 должна быть равна сумме значений показателей ФЛ_26.5 и ФЛ_27.5");
                }

                if (FL_25_Debt.debtOtherSum > FL_25_Debt.debtSum)
                {
                    errorData.AddError("debtOtherSum", FL_25_Debt.debtOtherSum, "Сумма задолженности по иным требованиям ФЛ_25.7 больше суммы задолженности ФЛ_25.4");
                }
                if (FL_25_Debt.debtOtherSum != (FL_26_DebtDue.debtDueOtherSum ?? 0m) + (FL_27_DebtOverdue.debtOverdueOtherSum ?? 0m))
                {
                    errorData.AddError("debtOtherSum", FL_25_Debt.debtOtherSum, "Сумма задолженности по иным требованиям ФЛ_25.7 должна быть равна сумме значений показателей ФЛ_26.6 и ФЛ_27.6");
                }

                if (!CurrencyDictionary.Contains(FL_25_Debt.currency))
                {
                    errorData.AddError("currency", FL_25_Debt.currency, "Валюта задолженности ФЛ_25.10 не соответствует Общероссийскому классификатору валют");
                }

                #endregion

                #region FL_26_DebtDue
                if (FL_26_DebtDue.debtDueExist == true)
                {
                    if (FL_26_DebtDue.debtDueStartDate.DateGreater(calcDate))
                    {
                        errorData.AddError("debtDueStartDate", FL_26_DebtDue.debtDueStartDate, "Дата возникновения срочной задолженности ФЛ_26.1 позднее даты расчета ФЛ_25.8, 28.13");
                    }
                    if (FL_26_DebtDue.debtDueStartDate.DateGreater(eventData.eventDateTimeLocal))
                    {
                        errorData.AddError("debtDueStartDate", FL_26_DebtDue.debtDueStartDate, "Дата возникновения срочной задолженности ФЛ_26.1 позднее даты и времени наступления события eventDateTime");
                    }
                    if (FL_26_DebtDue.debtDueStartDate.DateLess(dealBlock?.startDate))
                    {
                        errorData.AddError("debtDueStartDate", FL_26_DebtDue.debtDueStartDate, "Дата возникновения срочной задолженности ФЛ_26.1 раньше даты возникновения обязательства субъекта ФЛ_18.19");
                    }

                    if (FL_26_DebtDue.debtDueSum != (FL_26_DebtDue.debtDueMainSum ?? 0m) + (FL_26_DebtDue.debtDuePercentSum ?? 0m) + (FL_26_DebtDue.debtDueOtherSum ?? 0m))
                    {
                        errorData.AddError("debtDueSum", FL_26_DebtDue.debtDueSum, "Сумма срочной задолженности ФЛ_26.3 должна быть равна сумме значений показателей ФЛ_26.4, ФЛ_26.5 и ФЛ_26.6");
                    }

                    if (FL_26_DebtDue.debtDueMainSum != null && FL_26_DebtDue.debtDueMainSum.DecimalGreater(FL_26_DebtDue.debtDueSum))
                    {
                        errorData.AddError("debtDueMainSum", FL_26_DebtDue.debtDueMainSum, "Сумма срочной задолженности по основному долгу ФЛ_26.4 больше суммы срочной задолженности ФЛ_26.3");
                    }

                    if (FL_26_DebtDue.debtDuePercentSum != null && FL_26_DebtDue.debtDuePercentSum.DecimalGreater(FL_26_DebtDue.debtDueSum))
                    {
                        errorData.AddError("debtDuePercentSum", FL_26_DebtDue.debtDuePercentSum, "Сумма срочной задолженности по процентам ФЛ_26.5 больше суммы срочной задолженности ФЛ_26.3");
                    }

                    if (FL_26_DebtDue.debtDueOtherSum.DecimalGreater(FL_26_DebtDue.debtDueSum))
                    {
                        errorData.AddError("debtDueOtherSum", FL_26_DebtDue.debtDueOtherSum, "Сумма срочной задолженности по иным требованиям ФЛ_26.6 больше суммы срочной задолженности ФЛ_26.3");
                    }
                }
                #endregion
            }

            #region FL_27_DebtOverdue
            if (FL_27_DebtOverdue.missFact == true)
            {
                if (FL_27_DebtOverdue.debtOverdueSum != (FL_27_DebtOverdue.debtOverdueMainSum ?? 0) + (FL_27_DebtOverdue.debtOverduePercentSum ?? 0) + (FL_27_DebtOverdue.debtOverdueOtherSum ?? 0))
                {
                    errorData.AddError("debtOverdueSum", FL_27_DebtOverdue.debtOverdueSum, "Сумма просроченной задолженности ФЛ_27.3 должна быть равна сумме значений по показателям ФЛ_27.4, ФЛ_27.5 и ФЛ_27.6");
                }
                if (FL_27_DebtOverdue.missDuration.DecimalGreater(0) && FL_27_DebtOverdue.debtOverdueSum <= 0)
                {
                    errorData.AddError("debtOverdueSum", FL_27_DebtOverdue.debtOverdueSum, "Сумма просроченной задолженности ФЛ_27.3 должна быть больше «0»");
                }
                if (exist == false && FL_27_DebtOverdue.debtOverdueSum != 0)
                {
                    errorData.AddError("debtOverdueSum", FL_27_DebtOverdue.debtOverdueSum, "Сумма просроченной задолженности ФЛ_27.3 не равна «0»");
                }

                if (FL_27_DebtOverdue.debtOverdueMainSum.DecimalGreater(FL_27_DebtOverdue.debtOverdueSum))
                {
                    errorData.AddError("debtOverdueMainSum", FL_27_DebtOverdue.debtOverdueMainSum, "Сумма просроченной задолженности по основному долгу ФЛ_27.4 больше суммы просроченной задолженности ФЛ_27.3");
                }
                if (exist == false && FL_27_DebtOverdue.debtOverdueMainSum != 0)
                {
                    errorData.AddError("debtOverdueMainSum", FL_27_DebtOverdue.debtOverdueMainSum, "Сумма просроченной задолженности по основному долгу ФЛ_27.4 не равна «0»");
                }

                if (FL_27_DebtOverdue.debtOverduePercentSum.DecimalGreater(FL_27_DebtOverdue.debtOverdueSum))
                {
                    errorData.AddError("debtOverduePercentSum", FL_27_DebtOverdue.debtOverduePercentSum, "Сумма просроченной задолженности по процентам ФЛ_27.5 больше суммы просроченной задолженности ФЛ_27.3");
                }
                if (exist == false && FL_27_DebtOverdue.debtOverduePercentSum != 0)
                {
                    errorData.AddError("debtOverduePercentSum", FL_27_DebtOverdue.debtOverduePercentSum, "Сумма просроченной задолженности по процентам ФЛ_27.5 не равна «0»");
                }

                if (FL_27_DebtOverdue.debtOverdueOtherSum.DecimalGreater(FL_27_DebtOverdue.debtOverdueSum))
                {
                    errorData.AddError("debtOverdueOtherSum", FL_27_DebtOverdue.debtOverdueOtherSum, "Сумма просроченной задолженности по иным требованиям ФЛ_27.6 больше суммы просроченной задолженности ФЛ_27.3");
                }
                if (exist == false && FL_27_DebtOverdue.debtOverdueOtherSum != 0)
                {
                    errorData.AddError("debtOverdueOtherSum", FL_27_DebtOverdue.debtOverdueOtherSum, "Сумма просроченной задолженности по иным требованиям ФЛ_27.6 не равна «0»");
                }

                if (FL_27_DebtOverdue.debtOverdueStartDate.DateGreater(calcDate))
                {
                    errorData.AddError("debtOverdueStartDate", FL_27_DebtOverdue.debtOverdueStartDate, "Дата возникновения просроченной задолженности ФЛ_27.1 позднее даты расчета ФЛ_25.8, 28.13");
                }
                if (FL_27_DebtOverdue.debtOverdueStartDate.DateLess(dealBlock?.startDate))
                {
                    errorData.AddError("debtOverdueStartDate", FL_27_DebtOverdue.debtOverdueOtherSum, "Дата возникновения просроченной задолженности ФЛ_27.1 раньше даты возникновения обязательства субъекта ФЛ_18.19");
                }
                if (FL_27_DebtOverdue.debtOverdueStartDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("debtOverdueStartDate", FL_27_DebtOverdue.debtOverdueOtherSum, "Дата возникновения просроченной задолженности ФЛ_27.1 позднее даты и времени наступления события eventDateTime");
                }

                if (FL_27_DebtOverdue.debtOverdueMainSum > 0 && FL_27_DebtOverdue.mainMissDate == null)
                {
                    errorData.AddError("mainMissDate", null, "Отсутствует дата последнего пропущенного платежа по основному долгу ФЛ_27.8");
                }
                if (FL_27_DebtOverdue.mainMissDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("mainMissDate", FL_27_DebtOverdue.mainMissDate, "Дата последнего пропущенного платежа по основному долгу ФЛ_27.8 позднее даты и времени наступления события eventDateTime");
                }
                if (FL_27_DebtOverdue.mainMissDate.DateLess(dealBlock?.startDate))
                {
                    errorData.AddError("mainMissDate", FL_27_DebtOverdue.mainMissDate, "Дата последнего пропущенного платежа по основному долгу ФЛ_27.8 раньше даты возникновения обязательства субъекта ФЛ_18.19");
                }

                if (FL_27_DebtOverdue.debtOverduePercentSum > 0 && FL_27_DebtOverdue.percentMissDate == null)
                {
                    errorData.AddError("percentMissDate", null, "Отсутствует дата последнего пропущенного платежа по процентам ФЛ_27.9");
                }
                if (FL_27_DebtOverdue.percentMissDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("percentMissDate", FL_27_DebtOverdue.percentMissDate, "Дата последнего пропущенного платежа по процентам ФЛ_27.9 позднее даты и времени наступления события eventDateTime");
                }
                if (FL_27_DebtOverdue.percentMissDate.DateLess(dealBlock?.startDate))
                {
                    errorData.AddError("percentMissDate", FL_27_DebtOverdue.percentMissDate, "Дата последнего пропущенного платежа по процентам ФЛ_27.9 раньше даты возникновения обязательства субъекта ФЛ_18.19");
                }

                if (FL_27_DebtOverdue.debtOverdueSum > 0 && FL_27_DebtOverdue.missDuration.IntLessEqual(0))
                {
                    errorData.AddError("missDuration", FL_27_DebtOverdue.missDuration, "Продолжительность просрочки ФЛ_27.11 должна быть больше «0»");
                }
                if (participationBlock?.overdueExist == true && FL_27_DebtOverdue.missDuration.IntLessEqual(90))
                {
                    errorData.AddError("missDuration", FL_27_DebtOverdue.missDuration, "Продолжительность просрочки ФЛ_27.11 должна быть больше «90»");
                }
                if (FL_27_DebtOverdue.debtOverdueStartDate.IsNotNull() &&
                    FL_27_DebtOverdue.missDuration.IntGreater(0) && FL_27_DebtOverdue.missDuration.IntGreater((calcDate - DateTime.Parse(FL_27_DebtOverdue.debtOverdueStartDate)).Days + 1))
                {
                    errorData.AddError("missDuration", FL_27_DebtOverdue.missDuration, "Продолжительность просрочки ФЛ_27.11 должна быть не больше количества дней между датой, указанной по показателю «Дата расчета» ФЛ_25.8, 28.13 и датой, указанной по показателю «Дата возникновения просроченной задолженности» ФЛ_27.1, увеличенной на 1 день");
                }
                if (FL_27_DebtOverdue?.debtOverdueSum == 0 && FL_27_DebtOverdue.missDuration != "0")
                {
                    errorData.AddError("missDuration", FL_27_DebtOverdue.missDuration, "Продолжительность просрочки ФЛ_27.11 не равна «0»");
                }
            }
            #endregion

            #region FL_28_Payment
            if (FL_28_Payment.paymentSum != FL_28_Payment.paymentMainSum + FL_28_Payment.paymentPercentSum + FL_28_Payment.paymentOtherSum)
            {
                errorData.AddError("paymentSum", FL_28_Payment.paymentSum, "Сумма последнего внесенного платежа ФЛ_28.2 должна быть равна сумме значений показателей ФЛ_28.3, ФЛ_28.4, ФЛ_28.5");
            }
            if ((FL_28_Payment.lastMissPaySum > 0 || FL_28_Payment.paySum24 > 0) && FL_28_Payment.paymentSum <= 0)
            {
                errorData.AddError("paymentSum", FL_28_Payment.paymentSum, "Сумма последнего внесенного платежа ФЛ_28.2 должна быть больше «0»");
            }

            if (FL_28_Payment.paymentMainSum > FL_28_Payment.paymentSum)
            {
                errorData.AddError("paymentMainSum", FL_28_Payment.paymentMainSum, "Сумма последнего внесенного платежа по основному долгу ФЛ_28.3 больше суммы последнего внесенного платежа ФЛ_28.2");
            }

            if (FL_28_Payment.paymentPercentSum > FL_28_Payment.paymentSum)
            {
                errorData.AddError("paymentPercentSum", FL_28_Payment.paymentPercentSum, "Сумма последнего внесенного платежа по процентам ФЛ_28.4 больше суммы последнего внесенного платежа ФЛ_28.2");
            }

            if (FL_28_Payment.paymentOtherSum > FL_28_Payment.paymentSum)
            {
                errorData.AddError("paymentOtherSum", FL_28_Payment.paymentOtherSum, "Сумма последнего внесенного платежа по иным требованиям ФЛ_28.5 больше суммы последнего внесенного платежа ФЛ_28.2");
            }

            if (FL_28_Payment.totalSum != FL_28_Payment.totalMainSum + FL_28_Payment.totalPercentSum + FL_28_Payment.totalOtherSum)
            {
                errorData.AddError("totalSum", null, "Сумма всех внесенных платежей по обязательству ФЛ_28.6 должна быть равна сумме значений показателей ФЛ_28.7, ФЛ_28.8, ФЛ_28.9");
            }
            if (FL_28_Payment.totalSum < FL_28_Payment.paymentSum)
            {
                errorData.AddError("totalSum", FL_28_Payment.totalSum, "Сумма всех внесенных платежей по обязательству ФЛ_28.6 меньше суммы последнего внесенного платежа ФЛ_28.2");
            }
            if (FL_28_Payment.paymentSum == 0 && FL_28_Payment.totalSum != 0)
            {
                errorData.AddError("totalSum", FL_28_Payment.totalSum, "Сумма всех внесенных платежей по обязательству ФЛ_28.6 не равна «0»");
            }

            if (FL_28_Payment.totalMainSum > FL_28_Payment.totalSum)
            {
                errorData.AddError("totalMainSum", null, "Сумма внесенных платежей по основному долгу ФЛ_28.7 больше суммы всех внесенных платежей по обязательству ФЛ_28.6");
            }

            if (FL_28_Payment.totalPercentSum > FL_28_Payment.totalSum)
            {
                errorData.AddError("totalPercentSum", null, "Сумма внесенных платежей по процентам ФЛ_28.8 больше суммы всех внесенных платежей по обязательству ФЛ_28.6");
            }

            if (FL_28_Payment.totalOtherSum > FL_28_Payment.totalSum)
            {
                errorData.AddError("totalOtherSum", null, "Сумма внесенных платежей по иным требованиям ФЛ_28.9 больше суммы всех внесенных платежей по обязательству ФЛ_28.6");
            }

            if (FL_28_Payment.paymentSum > 0 && !FL_28_Payment.dateSpecified)
            {
                errorData.AddError("date", null, "Отсутствует дата последнего внесенного платежа ФЛ_28.1");
            }
            if (FL_28_Payment.paymentSum == 0 && FL_28_Payment.dateSpecified)
            {
                errorData.AddError("date", FL_28_Payment.paymentOtherSum, "Дата последнего внесенного платежа ФЛ_28.1 должна отсутствовать");
            }
            if (FL_28_Payment.dateSpecified && FL_28_Payment.date.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("date", FL_28_Payment.date, "Дата последнего внесенного платежа ФЛ_28.1 позднее даты и времени наступления события eventDateTime");
            }

            if (dealBlock?.code != refContractType.Item1 && FL_28_Payment.lastMissPaySumSpecified)
            {
                errorData.AddError("lastMissPaySum", null, "Сумма последнего внесенного платежа (части платежа) с просрочкой более 30 календарных дней ФЛ_28.14 должна отсутствовать");
            }
            if (dealBlock?.code != refContractType.Item1 && FL_28_Payment.paySum24Specified)
            {
                errorData.AddError("paySum24", null, "Сумма внесенных платежей за 24 календарных месяца, за исключением платежей с просрочкой более 30 календарных дней ФЛ_28.15 должна отсутствовать");
            }

            if (!CurrencyDictionary.Contains(FL_28_Payment.payCurrency))
            {
                errorData.AddError("payCurrency", FL_28_Payment.payCurrency, "Валюта внесенных платежей ФЛ_28.16 не соответствует Общероссийскому классификатору валют");
            }
            #endregion

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_25_26_27_28_Group_Type block, IEventData eventData, FL_18_Deal_Type? dealBlock = null, FL_29_MonthlyPayment_Type? monthlyPaymentBlock = null, FL_54_Accounting_Type? accountingBlock = null, FL_56_Participation_Type? participationBlock = null)
        {
            return [.. (block?.Check(eventData, dealBlock, monthlyPaymentBlock, accountingBlock, participationBlock)).OrEmptyIfNull()];
        }
    }
}
