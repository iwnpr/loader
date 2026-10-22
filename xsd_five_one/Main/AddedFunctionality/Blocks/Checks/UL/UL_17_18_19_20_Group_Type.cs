using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_17_18_19_20_Group_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_17_18_19_20_Group;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, UL_11_Deal_Type? dealBlock, UL_44_Accounting_Type? accountingBlock, UL_46_Participation_Type? participationBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (calcDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("calcDate", calcDate, "Дата расчета ЮЛ_17.8, 20.13 позднее даты и времени наступления события eventDateTime");
            }

            if (calcDate.DateNotEqual(accountingBlock?.calcDate))
            {
                errorData.AddError("calcDate", calcDate, "Дата расчета ЮЛ_17.8, 20.13 не равна дате расчета ЮЛ_44.6");
            }

            if (calcDate.DateLess(dealBlock?.startDate))
            {
                errorData.AddError("calcDate", calcDate, "Дата расчета ЮЛ_17.8, 20.13 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
            }

            if (eventData.EventName == refEventNameType.UL_Event_2_5 && exist != false)
            {
                errorData.AddError("calcDate", calcDate, "Признак наличия задолженности ЮЛ_17.1 не равен 0");
            }

            if (exist == true)
            {
                #region UL_17_Debt
                if (UL_17_Debt!.debtSum != UL_17_Debt.debtMainSum + UL_17_Debt.debtPercentSum + UL_17_Debt.debtOtherSum)
                {
                    errorData.AddError("debtSum", UL_17_Debt.debtSum, "Сумма задолженности ЮЛ_17.4 должна быть равна сумме значений показателей ЮЛ_17.5, ЮЛ_17.6 и ЮЛ_17.7");
                }
                if (UL_17_Debt.debtSum != (UL_18_DebtDue!.debtDueSum ?? 0m) + (UL_19_DebtOverdue.debtOverdueSum ?? 0m))
                {
                    errorData.AddError("debtSum", UL_17_Debt.debtSum, "Сумма задолженности ЮЛ_17.4 должна быть равна сумме значений показателей ЮЛ_18.3 и ЮЛ_19.3");
                }

                if (UL_17_Debt.debtMainSum > UL_17_Debt.debtSum)
                {
                    errorData.AddError("debtMainSum", UL_17_Debt.debtMainSum, "Сумма задолженности по основному долгу ЮЛ_17.5 больше суммы задолженности ЮЛ_17.4");
                }
                if (UL_17_Debt.debtMainSum != (UL_18_DebtDue.debtDueMainSum ?? 0m) + (UL_19_DebtOverdue.debtOverdueMainSum ?? 0m))
                {
                    errorData.AddError("debtMainSum", UL_17_Debt.debtMainSum, "Сумма задолженности по основному долгу ЮЛ_17.5 должна быть равна сумме значений показателей ЮЛ_18.4 и ЮЛ_19.4");
                }

                if (UL_17_Debt.debtPercentSum > UL_17_Debt.debtSum)
                {
                    errorData.AddError("debtPercentSum", UL_17_Debt.debtPercentSum, "Сумма задолженности по процентам ЮЛ_17.6 больше суммы задолженности ЮЛ_17.4");
                }
                if (UL_17_Debt.debtPercentSum != (UL_18_DebtDue.debtDuePercentSum ?? 0m) + (UL_19_DebtOverdue.debtOverduePercentSum ?? 0m))
                {
                    errorData.AddError("debtPercentSum", UL_17_Debt.debtPercentSum, "Сумма задолженности по процентам ЮЛ_17.6 должна быть равна сумме значений показателей ЮЛ_18.5 и ЮЛ_19.5");
                }

                if (UL_17_Debt.debtOtherSum > UL_17_Debt.debtSum)
                {
                    errorData.AddError("debtOtherSum", UL_17_Debt.debtOtherSum, "Сумма задолженности по иным требованиям ЮЛ_17.7 больше суммы задолженности ЮЛ_17.4");
                }
                if (UL_17_Debt.debtOtherSum != (UL_18_DebtDue.debtDueOtherSum ?? 0m) + (UL_19_DebtOverdue.debtOverdueOtherSum ?? 0m))
                {
                    errorData.AddError("debtOtherSum", UL_17_Debt.debtOtherSum, "Сумма задолженности по иным требованиям ЮЛ_17.7 должна быть равна сумме значений показателей ЮЛ_18.6 и ЮЛ_19.6");
                }

                if (!CurrencyDictionary.Contains(UL_17_Debt.currency))
                {
                    errorData.AddError("currency", UL_17_Debt.currency, "Валюта задолженности ЮЛ_17.10 не соответствует Общероссийскому классификатору валют");
                }
                #endregion

                #region UL_18_DebtDue
                if (exist == true && UL_18_DebtDue.debtDueExist == true)
                {
                    if (UL_18_DebtDue.debtDueStartDate.DateGreater(calcDate))
                    {
                        errorData.AddError("debtDueStartDate", UL_18_DebtDue.debtDueStartDate, "Дата возникновения срочной задолженности ЮЛ_18.1 позднее даты расчета ЮЛ_17.8, 20.13");
                    }
                    if (UL_18_DebtDue.debtDueStartDate.DateGreater(eventData?.eventDateTimeLocal))
                    {
                        errorData.AddError("debtDueStartDate", UL_18_DebtDue.debtDueStartDate, "Дата возникновения срочной задолженности ЮЛ_18.1 позднее даты и времени наступления события eventDateTime");
                    }
                    if (UL_18_DebtDue.debtDueStartDate.DateLess(dealBlock?.startDate))
                    {
                        errorData.AddError("debtDueStartDate", UL_18_DebtDue.debtDueStartDate, "Дата возникновения срочной задолженности ЮЛ_18.1 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
                    }

                    if (UL_18_DebtDue.debtDueSum != UL_18_DebtDue.debtDueMainSum + UL_18_DebtDue.debtDuePercentSum + UL_18_DebtDue.debtDueOtherSum)
                    {
                        errorData.AddError("debtDueSum", UL_18_DebtDue.debtDueSum, "Сумма срочной задолженности ЮЛ_18.3 должна быть равна сумме значений показателей ЮЛ_18.4, ЮЛ_18.5 и ЮЛ_18.6");
                    }

                    if (UL_18_DebtDue.debtDueMainSum != null && UL_18_DebtDue.debtDueMainSum > UL_18_DebtDue.debtDueSum)
                    {
                        errorData.AddError("debtDueMainSum", UL_18_DebtDue.debtDueMainSum, "Сумма срочной задолженности по основному долгу ЮЛ_18.4 больше суммы срочной задолженности ЮЛ_18.3");
                    }

                    if (UL_18_DebtDue.debtDuePercentSum != null && UL_18_DebtDue.debtDuePercentSum > UL_18_DebtDue.debtDueSum)
                    {
                        errorData.AddError("debtDuePercentSum", UL_18_DebtDue.debtDuePercentSum, "Сумма срочной задолженности по процентам ЮЛ_18.5 больше суммы срочной задолженности ЮЛ_18.3");
                    }

                    if (UL_18_DebtDue.debtDueOtherSum > UL_18_DebtDue.debtDueSum)
                    {
                        errorData.AddError("debtDueOtherSum", UL_18_DebtDue.debtDueOtherSum, "Сумма срочной задолженности по иным требованиям ЮЛ_18.6 больше суммы срочной задолженности ЮЛ_18.3");
                    }
                }
                #endregion
            }

            #region UL_19_DebtOverdue
            if (UL_19_DebtOverdue.missFact == true)
            {
                if (UL_19_DebtOverdue.debtOverdueSum != (UL_19_DebtOverdue.debtOverdueMainSum ?? 0) + (UL_19_DebtOverdue.debtOverduePercentSum ?? 0) + (UL_19_DebtOverdue.debtOverdueOtherSum ?? 0))
                {
                    errorData.AddError("debtOverdueSum", UL_19_DebtOverdue.debtOverdueSum, "Сумма просроченной задолженности ЮЛ_19.3 должна быть равна сумме значений по показателям ЮЛ_19.4, ЮЛ_19.5 и ЮЛ_19.6");
                }
                if (UL_19_DebtOverdue.missDuration.IntGreater(0) && UL_19_DebtOverdue.debtOverdueSum <= 0)
                {
                    errorData.AddError("debtOverdueSum", UL_19_DebtOverdue.debtOverdueSum, "Сумма просроченной задолженности ЮЛ_19.3 должна быть больше «0»");
                }
                if (exist == false && UL_19_DebtOverdue.debtOverdueSum != 0)
                {
                    errorData.AddError("debtOverdueSum", UL_19_DebtOverdue.debtOverdueSum, "Сумма просроченной задолженности ЮЛ_19.3 не равна «0»");
                }

                if (UL_19_DebtOverdue.debtOverdueMainSum.DecimalGreater(UL_19_DebtOverdue.debtOverdueSum))
                {
                    errorData.AddError("debtOverdueMainSum", UL_19_DebtOverdue.debtOverdueMainSum, "Сумма просроченной задолженности по основному долгу ЮЛ_19.4 больше суммы просроченной задолженности ЮЛ_19.3");
                }
                if (exist == false && UL_19_DebtOverdue.debtOverdueMainSum != 0)
                {
                    errorData.AddError("debtOverdueMainSum", UL_19_DebtOverdue.debtOverdueMainSum, "Сумма просроченной задолженности по основному долгу ЮЛ_19.4 не равна «0»");
                }

                if (UL_19_DebtOverdue.debtOverduePercentSum.DecimalGreater(UL_19_DebtOverdue.debtOverdueSum))
                {
                    errorData.AddError("debtOverduePercentSum", UL_19_DebtOverdue.debtOverduePercentSum, "Сумма просроченной задолженности по процентам ЮЛ_19.5 больше суммы просроченной задолженности ЮЛ_19.3");
                }
                if (exist == false && UL_19_DebtOverdue.debtOverduePercentSum != 0)
                {
                    errorData.AddError("debtOverduePercentSum", UL_19_DebtOverdue.debtOverduePercentSum, "Сумма просроченной задолженности по процентам ЮЛ_19.5 не равна «0»");
                }

                if (UL_19_DebtOverdue.debtOverdueOtherSum.DecimalGreater(UL_19_DebtOverdue.debtOverdueSum))
                {
                    errorData.AddError("debtOverdueOtherSum", UL_19_DebtOverdue.debtOverdueOtherSum, "Сумма просроченной задолженности по иным требованиям ЮЛ_19.6 больше суммы просроченной задолженности ЮЛ_19.3");
                }
                if (exist == false && UL_19_DebtOverdue.debtOverdueOtherSum != 0)
                {
                    errorData.AddError("debtOverdueOtherSum", UL_19_DebtOverdue.debtOverdueOtherSum, "Сумма просроченной задолженности по иным требованиям ЮЛ_19.6 не равна «0»");
                }

                if (UL_19_DebtOverdue.debtOverdueStartDate.DateGreater(calcDate))
                {
                    errorData.AddError("debtOverdueStartDate", UL_19_DebtOverdue.debtOverdueStartDate, "Дата возникновения просроченной задолженности ЮЛ_19.1 позднее даты расчета ЮЛ_17.8, 20.13");
                }
                if (UL_19_DebtOverdue.debtOverdueStartDate.DateLess(dealBlock?.startDate))
                {
                    errorData.AddError("debtOverdueStartDate", UL_19_DebtOverdue.debtOverdueOtherSum, "Дата возникновения просроченной задолженности ЮЛ_19.1 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
                }
                if (UL_19_DebtOverdue.debtOverdueStartDate.DateGreater(eventData?.eventDateTimeLocal))
                {
                    errorData.AddError("debtOverdueStartDate", UL_19_DebtOverdue.debtOverdueOtherSum, "Дата возникновения просроченной задолженности ЮЛ_19.1 позднее даты и времени наступления события eventDateTime");
                }

                if (UL_19_DebtOverdue.debtOverdueMainSum > 0 && UL_19_DebtOverdue.mainMissDate == null)
                {
                    errorData.AddError("mainMissDate", null, "Отсутствует дата последнего пропущенного платежа по основному долгу ЮЛ_19.8");
                }
                if (UL_19_DebtOverdue.mainMissDate.DateGreater(eventData?.eventDateTimeLocal))
                {
                    errorData.AddError("mainMissDate", UL_19_DebtOverdue.mainMissDate, "Дата последнего пропущенного платежа по основному долгу ЮЛ_19.8 позднее даты и времени наступления события eventDateTime");
                }
                if (UL_19_DebtOverdue.mainMissDate.DateLess(dealBlock?.startDate))
                {
                    errorData.AddError("mainMissDate", UL_19_DebtOverdue.mainMissDate, "Дата последнего пропущенного платежа по основному долгу ЮЛ_19.8 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
                }

                if (UL_19_DebtOverdue.debtOverduePercentSum > 0 && UL_19_DebtOverdue.percentMissDate == null)
                {
                    errorData.AddError("percentMissDate", null, "Отсутствует дата последнего пропущенного платежа по процентам ЮЛ_19.9");
                }
                if (UL_19_DebtOverdue.percentMissDate.DateGreater(eventData?.eventDateTimeLocal))
                {
                    errorData.AddError("percentMissDate", UL_19_DebtOverdue.percentMissDate, "Дата последнего пропущенного платежа по процентам ЮЛ_19.9 позднее даты и времени наступления события eventDateTime");
                }
                if (UL_19_DebtOverdue.percentMissDate.DateLess(dealBlock?.startDate))
                {
                    errorData.AddError("percentMissDate", UL_19_DebtOverdue.percentMissDate, "Дата последнего пропущенного платежа по процентам ЮЛ_19.9 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
                }

                if (UL_19_DebtOverdue.debtOverdueSum > 0 && UL_19_DebtOverdue.missDuration.IntLessEqual(0))
                {
                    errorData.AddError("missDuration", UL_19_DebtOverdue.missDuration, "Продолжительность просрочки ЮЛ_19.11 должна быть больше «0»");
                }
                if (participationBlock?.overdueExist == true && UL_19_DebtOverdue.missDuration.IntLessEqual(90))
                {
                    errorData.AddError("missDuration", UL_19_DebtOverdue.missDuration, "Продолжительность просрочки ЮЛ_19.11 должна быть больше «90»");
                }
                if (UL_19_DebtOverdue.debtOverdueStartDate.IsNotNull() &&
                     UL_19_DebtOverdue.missDuration.IntGreater(0) && UL_19_DebtOverdue.missDuration.IntGreater((calcDate - DateTime.Parse(UL_19_DebtOverdue.debtOverdueStartDate)).Days + 1))
                {
                    errorData.AddError("missDuration", UL_19_DebtOverdue.missDuration, "Продолжительность просрочки ЮЛ_19.11 должна быть не больше количества дней между датой, указанной по показателю «Дата расчета» ЮЛ_17.8, 20.13 и датой, указанной по показателю «Дата возникновения просроченной задолженности» ЮЛ_19.1, увеличенной на 1 день");
                }
                if (UL_19_DebtOverdue?.debtOverdueSum == 0 && UL_19_DebtOverdue.missDuration != "0")
                {
                    errorData.AddError("missDuration", UL_19_DebtOverdue.missDuration, "Продолжительность просрочки ЮЛ_19.11 не равна «0»");
                }
            }
            #endregion

            #region UL_20_Payment
            if (UL_20_Payment.paymentSum != UL_20_Payment.paymentMainSum + UL_20_Payment.paymentPercentSum + UL_20_Payment.paymentOtherSum)
            {
                errorData.AddError("paymentSum", UL_20_Payment.paymentSum, "Сумма последнего внесенного платежа ЮЛ_20.2 должна быть равна сумме значений показателей ЮЛ_20.3, ЮЛ_20.4, ЮЛ_20.5");
            }

            if (UL_20_Payment.paymentMainSum > UL_20_Payment.paymentSum)
            {
                errorData.AddError("paymentMainSum", UL_20_Payment.paymentMainSum, "Сумма последнего внесенного платежа по основному долгу ЮЛ_20.3 больше суммы последнего внесенного платежа ЮЛ_20.2");
            }

            if (UL_20_Payment.paymentPercentSum > UL_20_Payment.paymentSum)
            {
                errorData.AddError("paymentPercentSum", UL_20_Payment.paymentPercentSum, "Сумма последнего внесенного платежа по процентам ЮЛ_20.4 больше суммы последнего внесенного платежа ЮЛ_20.2");
            }

            if (UL_20_Payment.paymentOtherSum > UL_20_Payment.paymentSum)
            {
                errorData.AddError("paymentOtherSum", UL_20_Payment.paymentOtherSum, "Сумма последнего внесенного платежа по иным требованиям ЮЛ_20.5 больше суммы последнего внесенного платежа ЮЛ_20.2");
            }

            if (UL_20_Payment.totalSum != UL_20_Payment.totalMainSum + UL_20_Payment.totalPercentSum + UL_20_Payment.totalOtherSum)
            {
                errorData.AddError("totalSum", null, "Сумма всех внесенных платежей по обязательству ЮЛ_20.6 должна быть равна сумме значений показателей ЮЛ_20.7, ЮЛ_20.8 и ЮЛ_20.9");
            }
            if (UL_20_Payment.totalSum < UL_20_Payment.paymentSum)
            {
                errorData.AddError("totalSum", UL_20_Payment.totalSum, "Сумма всех внесенных платежей по обязательству ЮЛ_20.6 меньше суммы последнего внесенного платежа ЮЛ_20.2");
            }
            if (UL_20_Payment.paymentSum == 0 && UL_20_Payment.totalSum != 0)
            {
                errorData.AddError("totalSum", UL_20_Payment.totalSum, "Сумма всех внесенных платежей по обязательству ЮЛ_20.6 не равна «0»");
            }

            if (UL_20_Payment.totalMainSum > UL_20_Payment.totalSum)
            {
                errorData.AddError("totalMainSum", null, "Сумма внесенных платежей по основному долгу ЮЛ_20.7 больше суммы всех внесенных платежей по обязательству ЮЛ_20.6");
            }

            if (UL_20_Payment.totalPercentSum > UL_20_Payment.totalSum)
            {
                errorData.AddError("totalPercentSum", null, "Сумма внесенных платежей по процентам ЮЛ_20.8 больше суммы всех внесенных платежей по обязательству ЮЛ_20.6");
            }

            if (UL_20_Payment.totalOtherSum > UL_20_Payment.totalSum)
            {
                errorData.AddError("totalOtherSum", null, "Сумма внесенных платежей по иным требованиям ЮЛ_20.9 больше суммы всех внесенных платежей по обязательству ЮЛ_20.6");
            }

            if (UL_20_Payment.paymentSum > 0 && !UL_20_Payment.dateSpecified)
            {
                errorData.AddError("date", null, "Отсутствует дата последнего внесенного платежа ЮЛ_20.1");
            }
            if (UL_20_Payment.paymentSum == 0 && UL_20_Payment.dateSpecified)
            {
                errorData.AddError("date", UL_20_Payment.paymentOtherSum, "Дата последнего внесенного платежа ЮЛ_20.1 должна отсутствовать");
            }
            if (UL_20_Payment.dateSpecified && UL_20_Payment.date.DateGreater(eventData?.eventDateTimeLocal))
            {
                errorData.AddError("date", UL_20_Payment.date, "Дата последнего внесенного платежа ЮЛ_20.1 позднее даты и времени наступления события eventDateTime");
            }

            if (!CurrencyDictionary.Contains(UL_20_Payment.payCurrency))
            {
                errorData.AddError("payCurrency", UL_20_Payment.payCurrency, "Валюта внесенных платежей ЮЛ_20.16 не соответствует Общероссийскому классификатору валют");
            }
            #endregion

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_17_18_19_20_Group_Type block, IEventData eventData, UL_11_Deal_Type? dealBlock = null, UL_44_Accounting_Type? accountingBlock = null, UL_46_Participation_Type? participationBlock = null)
        {
            return [.. (block?.Check(eventData, dealBlock, accountingBlock, participationBlock)).OrEmptyIfNull()];
        }
    }
}
