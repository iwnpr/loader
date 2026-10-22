using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_17_18_19_20_Group_Type : Block_Type
    {
        public UL_17_18_19_20_Group_Type()
        {
            blockName = refBlockNameType.UL_17_18_19_20_Group;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData,
            UL_11_Deal_Type dealBlock = null, UL_46_Participation_Type participationBlock = null)
        {
            InitCheck(eventData);

            if (calcDate.DateGreater(eventData?.eventDate))
            {
                AddError("calcDate", calcDate, "Дата расчета ЮЛ_17.8, 20.13 позднее даты наступления события eventDate");
            }

            if (calcDate.DateLess(dealBlock?.startDate))
            {
                AddError("calcDate", calcDate, "Дата расчета ЮЛ_17.8, 20.13 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
            }

            if (eventData.EventName == refEventNameType.UL_Event_2_5 && exist != false)
            {
                AddError("calcDate", calcDate, "Признак наличия задолженности ЮЛ_17.1 не равен 0");
            }

            #region UL_17_Debt
            if (exist == true)
            {
                if (UL_17_Debt.debtSum != UL_17_Debt.debtMainSum + UL_17_Debt.debtPercentSum + UL_17_Debt.debtOtherSum)
                {
                    AddError("debtSum", UL_17_Debt.debtSum, "Сумма задолженности ЮЛ_17.4 должна быть равна сумме значений показателей ЮЛ_17.5, ЮЛ_17.6 и ЮЛ_17.7");
                }
                if (UL_17_Debt.debtSum != (UL_18_DebtDue.debtDueSum ?? 0m) + (UL_19_DebtOverdue.debtOverdueSum ?? 0m))
                {
                    AddError("debtSum", UL_17_Debt.debtSum, "Сумма задолженности ЮЛ_17.4 должна быть равна сумме значений показателей ЮЛ_18.3 и ЮЛ_19.3");
                }

                if (UL_17_Debt.debtMainSum > UL_17_Debt.debtSum)
                {
                    AddError("debtMainSum", UL_17_Debt.debtMainSum, "Сумма задолженности по основному долгу ЮЛ_17.5 больше суммы задолженности ЮЛ_17.4");
                }
                if (UL_17_Debt.debtMainSum != (UL_18_DebtDue.debtDueMainSum ?? 0m) + (UL_19_DebtOverdue.debtOverdueMainSum ?? 0m))
                {
                    AddError("debtMainSum", UL_17_Debt.debtMainSum, "Сумма задолженности по основному долгу ЮЛ_17.5 должна быть равна сумме значений показателей ЮЛ_18.4 и ЮЛ_19.4");
                }

                if (UL_17_Debt.debtPercentSum > UL_17_Debt.debtSum)
                {
                    AddError("debtPercentSum", UL_17_Debt.debtPercentSum, "Сумма задолженности по процентам ЮЛ_17.6 больше суммы задолженности ЮЛ_17.4");
                }
                if (UL_17_Debt.debtPercentSum != (UL_18_DebtDue.debtDuePercentSum ?? 0m) + (UL_19_DebtOverdue.debtOverduePercentSum ?? 0m))
                {
                    AddError("debtPercentSum", UL_17_Debt.debtPercentSum, "Сумма задолженности по процентам ЮЛ_17.6 должна быть равна сумме значений показателей ЮЛ_18.5 и ЮЛ_19.5");
                }

                if (UL_17_Debt.debtOtherSum > UL_17_Debt.debtSum)
                {
                    AddError("debtOtherSum", UL_17_Debt.debtOtherSum, "Сумма задолженности по иным требованиям ЮЛ_17.7 больше суммы задолженности ЮЛ_17.4");
                }
                if (UL_17_Debt.debtOtherSum != (UL_18_DebtDue.debtDueOtherSum ?? 0m) + (UL_19_DebtOverdue.debtOverdueOtherSum ?? 0m))
                {
                    AddError("debtOtherSum", UL_17_Debt.debtOtherSum, "Сумма задолженности по иным требованиям ЮЛ_17.7 должна быть равна сумме значений показателей ЮЛ_18.6 и ЮЛ_19.6");
                }

                if (!CurrencyDictionary.Contains(UL_17_Debt.currency))
                {
                    AddError("currency", UL_17_Debt.currency, "Валюта задолженности ЮЛ_17.10 не соответствует Общероссийскому классификатору валют");
                }
            }
            #endregion

            #region UL_18_DebtDue
            if (exist == true && UL_18_DebtDue.debtDueExist == true)
            {
                if (UL_18_DebtDue.debtDueStartDate.DateGreater(calcDate))
                {
                    AddError("debtDueStartDate", UL_18_DebtDue.debtDueStartDate, "Дата возникновения срочной задолженности ЮЛ_18.1 позднее даты расчета ЮЛ_17.8, 20.13");
                }
                if (UL_18_DebtDue.debtDueStartDate.DateGreater(eventData?.eventDate))
                {
                    AddError("debtDueStartDate", UL_18_DebtDue.debtDueStartDate, "Дата возникновения срочной задолженности ЮЛ_18.1 позднее даты наступления события eventDate");
                }
                if (UL_18_DebtDue.debtDueStartDate.DateLess(dealBlock?.startDate))
                {
                    AddError("debtDueStartDate", UL_18_DebtDue.debtDueStartDate, "Дата возникновения срочной задолженности ЮЛ_18.1 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
                }

                if (UL_18_DebtDue.debtDueSum != UL_18_DebtDue.debtDueMainSum + UL_18_DebtDue.debtDuePercentSum + UL_18_DebtDue.debtDueOtherSum)
                {
                    AddError("debtDueSum", UL_18_DebtDue.debtDueSum, "Сумма срочной задолженности ЮЛ_18.3 должна быть равна сумме значений показателей ЮЛ_18.4, ЮЛ_18.5 и ЮЛ_18.6");
                }

                if (UL_18_DebtDue.debtDueMainSum != null && UL_18_DebtDue.debtDueMainSum > UL_18_DebtDue.debtDueSum)
                {
                    AddError("debtDueMainSum", UL_18_DebtDue.debtDueMainSum, "Сумма срочной задолженности по основному долгу ЮЛ_18.4 больше суммы срочной задолженности ЮЛ_18.3");
                }

                if (UL_18_DebtDue.debtDuePercentSum != null && UL_18_DebtDue.debtDuePercentSum > UL_18_DebtDue.debtDueSum)
                {
                    AddError("debtDuePercentSum", UL_18_DebtDue.debtDuePercentSum, "Сумма срочной задолженности по процентам ЮЛ_18.5 больше суммы срочной задолженности ЮЛ_18.3");
                }

                if (UL_18_DebtDue.debtDueOtherSum > UL_18_DebtDue.debtDueSum)
                {
                    AddError("debtDueOtherSum", UL_18_DebtDue.debtDueOtherSum, "Сумма срочной задолженности по иным требованиям ЮЛ_18.6 больше суммы срочной задолженности ЮЛ_18.3");
                }
            }
            #endregion

            #region UL_19_DebtOverdue
            if (UL_19_DebtOverdue.missFact == true)
            {
                if (UL_19_DebtOverdue.debtOverdueSum != (UL_19_DebtOverdue.debtOverdueMainSum ?? 0) + (UL_19_DebtOverdue.debtOverduePercentSum ?? 0) + (UL_19_DebtOverdue.debtOverdueOtherSum ?? 0))
                {
                    AddError("debtOverdueSum", UL_19_DebtOverdue.debtOverdueSum, "Сумма просроченной задолженности ЮЛ_19.3 должна быть равна сумме значений по показателям ЮЛ_19.4, ЮЛ_19.5 и ЮЛ_19.6");
                }
                if (UL_19_DebtOverdue.missDuration > 0 && UL_19_DebtOverdue.debtOverdueSum <= 0)
                {
                    AddError("debtOverdueSum", UL_19_DebtOverdue.debtOverdueSum, "Сумма просроченной задолженности ЮЛ_19.3 должна быть больше «0»");
                }

                if (UL_19_DebtOverdue.debtOverdueMainSum.DecimalGreater(UL_19_DebtOverdue.debtOverdueSum))
                {
                    AddError("debtOverdueMainSum", UL_19_DebtOverdue.debtOverdueMainSum, "Сумма просроченной задолженности по основному долгу ЮЛ_19.4 больше суммы просроченной задолженности ЮЛ_19.3");
                }

                if (UL_19_DebtOverdue.debtOverduePercentSum.DecimalGreater(UL_19_DebtOverdue.debtOverdueSum))
                {
                    AddError("debtOverduePercentSum", UL_19_DebtOverdue.debtOverduePercentSum, "Сумма просроченной задолженности по процентам ЮЛ_19.5 больше суммы просроченной задолженности ЮЛ_19.3");
                }

                if (UL_19_DebtOverdue.debtOverdueOtherSum == null)
                {
                    AddError("debtOverdueOtherSum", null, "Отсутствует сумма просроченной задолженности по иным требованиям ЮЛ_19.6");
                }
                if (UL_19_DebtOverdue.debtOverdueOtherSum.DecimalGreater(UL_19_DebtOverdue.debtOverdueSum))
                {
                    AddError("debtOverdueOtherSum", UL_19_DebtOverdue.debtOverdueOtherSum, "Сумма просроченной задолженности по иным требованиям ЮЛ_19.6 больше суммы просроченной задолженности ЮЛ_19.3");
                }

                if (UL_19_DebtOverdue.debtOverdueStartDate.DateGreater(calcDate))
                {
                    AddError("debtOverdueStartDate", UL_19_DebtOverdue.debtOverdueStartDate, "Дата возникновения просроченной задолженности ЮЛ_19.1 позднее даты расчета ЮЛ_17.8, 20.13");
                }
                if (UL_19_DebtOverdue.debtOverdueStartDate.DateLess(dealBlock?.startDate))
                {
                    AddError("debtOverdueStartDate", UL_19_DebtOverdue.debtOverdueOtherSum, "Дата возникновения просроченной задолженности ЮЛ_19.1 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
                }
                if (UL_19_DebtOverdue.debtOverdueStartDate.DateGreater(eventData?.eventDate))
                {
                    AddError("debtOverdueStartDate", UL_19_DebtOverdue.debtOverdueOtherSum, "Дата возникновения просроченной задолженности ЮЛ_19.1 позднее даты наступления события eventDate");
                }

                if (UL_19_DebtOverdue.debtOverdueMainSum > 0 && UL_19_DebtOverdue.mainMissDate == null)
                {
                    AddError("mainMissDate", null, "Отсутствует дата последнего пропущенного платежа по основному долгу ЮЛ_19.8");
                }
                if (UL_19_DebtOverdue.mainMissDate.DateGreater(eventData?.eventDate))
                {
                    AddError("mainMissDate", UL_19_DebtOverdue.mainMissDate, "Дата последнего пропущенного платежа по основному долгу ЮЛ_19.8 позднее даты наступления события eventDate");
                }
                if (UL_19_DebtOverdue.mainMissDate.DateLess(dealBlock?.startDate))
                {
                    AddError("mainMissDate", UL_19_DebtOverdue.mainMissDate, "Дата последнего пропущенного платежа по основному долгу ЮЛ_19.8 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
                }

                if (UL_19_DebtOverdue.debtOverduePercentSum > 0 && UL_19_DebtOverdue.percentMissDate == null)
                {
                    AddError("percentMissDate", null, "Отсутствует дата последнего пропущенного платежа по процентам ЮЛ_19.9");
                }
                if (UL_19_DebtOverdue.percentMissDate.DateGreater(eventData?.eventDate))
                {
                    AddError("percentMissDate", UL_19_DebtOverdue.percentMissDate, "Дата последнего пропущенного платежа по процентам ЮЛ_19.9 позднее даты наступления события eventDate");
                }
                if (UL_19_DebtOverdue.percentMissDate.DateLess(dealBlock?.startDate))
                {
                    AddError("percentMissDate", UL_19_DebtOverdue.percentMissDate, "Дата последнего пропущенного платежа по процентам ЮЛ_19.9 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
                }

                if (UL_19_DebtOverdue.debtOverdueSum > 0 && UL_19_DebtOverdue.missDuration <= 0)
                {
                    AddError("missDuration", UL_19_DebtOverdue.missDuration, "Продолжительность просрочки ЮЛ_19.11 должна быть больше «0»");
                }
                if (participationBlock?.overdueExist == true && UL_19_DebtOverdue.missDuration <= 90)
                {
                    AddError("missDuration", UL_19_DebtOverdue.missDuration, "Продолжительность просрочки ЮЛ_19.11 должна быть больше «90»");
                }
                if (UL_19_DebtOverdue.debtOverdueStartDate.IsNotNull() &&
                    UL_19_DebtOverdue.missDuration > 0 && UL_19_DebtOverdue.missDuration > ((calcDate - DateTime.Parse(UL_19_DebtOverdue.debtOverdueStartDate)).TotalDays + 1))
                {
                    AddError("missDuration", UL_19_DebtOverdue.missDuration, "Продолжительность просрочки ЮЛ_19.11 должна быть не больше количества дней между датой, указанной по показателю «Дата расчета» ЮЛ_17.8, 20.13 и датой, указанной по показателю «Дата возникновения просроченной задолженности» ЮЛ_19.1, увеличенной на 1 день");
                }
            }
            #endregion

            #region UL_20_Payment
            if (UL_20_Payment.paymentSum != UL_20_Payment.paymentMainSum + UL_20_Payment.paymentPercentSum + UL_20_Payment.paymentOtherSum)
            {
                AddError("paymentSum", UL_20_Payment.paymentSum, "Сумма последнего внесенного платежа ЮЛ_20.2 должна быть равна сумме значений показателей ЮЛ_20.3, ЮЛ_20.4, ЮЛ_20.5");
            }

            if (UL_20_Payment.paymentMainSum > UL_20_Payment.paymentSum)
            {
                AddError("paymentMainSum", UL_20_Payment.paymentMainSum, "Сумма последнего внесенного платежа по основному долгу ЮЛ_20.3 больше суммы последнего внесенного платежа ЮЛ_20.2");
            }

            if (UL_20_Payment.paymentPercentSum > UL_20_Payment.paymentSum)
            {
                AddError("paymentPercentSum", UL_20_Payment.paymentPercentSum, "Сумма последнего внесенного платежа по процентам ЮЛ_20.4 больше суммы последнего внесенного платежа ЮЛ_20.2");
            }

            if (UL_20_Payment.paymentOtherSum > UL_20_Payment.paymentSum)
            {
                AddError("paymentOtherSum", UL_20_Payment.paymentOtherSum, "Сумма последнего внесенного платежа по иным требованиям ЮЛ_20.5 больше суммы последнего внесенного платежа ЮЛ_20.2");
            }

            if (UL_20_Payment.totalSum != UL_20_Payment.totalMainSum + UL_20_Payment.totalPercentSum + UL_20_Payment.totalOtherSum)
            {
                AddError("totalSum", null, "Сумма всех внесенных платежей по обязательству ЮЛ_20.6 должна быть равна сумме значений показателей ЮЛ_20.7, ЮЛ_20.8 и ЮЛ_20.9");
            }
            if (UL_20_Payment.totalSum < UL_20_Payment.paymentSum)
            {
                AddError("totalSum", UL_20_Payment.totalSum, "Сумма всех внесенных платежей по обязательству ЮЛ_20.6 меньше суммы последнего внесенного платежа ЮЛ_20.2");
            }

            if (UL_20_Payment.totalMainSum > UL_20_Payment.totalSum)
            {
                AddError("totalMainSum", null, "Сумма внесенных платежей по основному долгу ЮЛ_20.7 больше суммы всех внесенных платежей по обязательству ЮЛ_20.6");
            }

            if (UL_20_Payment.totalPercentSum > UL_20_Payment.totalSum)
            {
                AddError("totalPercentSum", null, "Сумма внесенных платежей по процентам ЮЛ_20.8 больше суммы всех внесенных платежей по обязательству ЮЛ_20.6");
            }

            if (UL_20_Payment.totalOtherSum > UL_20_Payment.totalSum)
            {
                AddError("totalOtherSum", null, "Сумма внесенных платежей по иным требованиям ЮЛ_20.9 больше суммы всех внесенных платежей по обязательству ЮЛ_20.6");
            }

            if (UL_20_Payment.paymentSum > 0 && !UL_20_Payment.dateSpecified)
            {
                AddError("date", null, "Отсутствует дата последнего внесенного платежа ЮЛ_20.1");
            }
            if (UL_20_Payment.paymentSum == 0 && UL_20_Payment.dateSpecified)
            {
                AddError("date", UL_20_Payment.paymentOtherSum, "Дата последнего внесенного платежа ЮЛ_20.1 должна отсутствовать");
            }
            if (UL_20_Payment.dateSpecified && UL_20_Payment.date.DateGreater(eventData?.eventDate))
            {
                AddError("date", UL_20_Payment.date, "Дата последнего внесенного платежа ЮЛ_20.1 позднее даты наступления события eventDate");
            }

            if (UL_20_Payment.payCurrency == null)
            {
                AddError("payCurrency", UL_20_Payment.payCurrency, "Отсутствует валюта внесенных платежей ЮЛ_20.16");
            }
            if (!CurrencyDictionary.Contains(UL_20_Payment.payCurrency))
            {
                AddError("payCurrency", UL_20_Payment.payCurrency, "Валюта внесенных платежей ЮЛ_20.16 не соответствует Общероссийскому классификатору валют");
            }
            #endregion

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_17_18_19_20_Group_Type block, EventData eventData,
            UL_11_Deal_Type dealBlock = null, UL_46_Participation_Type participationBlock = null)
        {
            return (block?.Check(eventData, dealBlock, participationBlock)).OrEmptyIfNull().ToList();
        }
    }
}
