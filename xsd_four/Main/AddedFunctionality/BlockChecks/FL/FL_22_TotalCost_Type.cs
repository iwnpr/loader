using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_22_TotalCost_Type : Block_Type
    {
        public FL_22_TotalCost_Type()
        {
            blockName = refBlockNameType.FL_22_TotalCost;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IFL_17_DealUid dealUidBlock = null, FL_18_Deal_Type dealBlock = null)
        {
            InitCheck(eventData);

            if (dealBlock?.consumerExist == true)
            {
                if (dealUidBlock?.openDate.DateGreaterEqual("2014-07-01") == true)
                {
                    if (minPercentCost == null)
                    {
                        AddError("minPercentCost", minPercentCost, "Отсутствует минимальная полная стоимость кредита (займа) в процентах годовых ФЛ_22.1");
                    }

                    if (maxPercentCost == null)
                    {
                        AddError("maxPercentCost", maxPercentCost, "Отсутствует максимальная полная стоимость кредита (займа) в процентах годовых ФЛ_22.4");
                    }
                }

                if (dealUidBlock?.openDate.DateGreaterEqual("2018-06-24") == true)
                {
                    if (minCost == null)
                    {
                        AddError("minCost", minCost, "Отсутствует минимальная полная стоимость кредита (займа) в денежном выражении ФЛ_22.2");
                    }

                    if (maxCost == null)
                    {
                        AddError("maxCost", maxCost, "Отсутствует максимальная полная стоимость кредита (займа) в денежном выражении ФЛ_22.5");
                    }
                }

                if (dealUidBlock?.openDate.DateGreaterEqual("2024-01-22") == true)
                {
                    if (maxPercentCash == null)
                    {
                        AddError("maxPercentCash", maxPercentCash, "Отсутствует максимальная полная стоимость кредита (займа) в процентах годовых при его использовании путем снятия наличных денежных средств ФЛ_22.6");
                    }

                    if (maxPercentCashless == null)
                    {
                        AddError("maxPercentCashless", maxPercentCashless, "Отсутствует максимальная полная стоимость кредита (займа) в процентах годовых при его использовании в безналичном порядке ФЛ_22.7");
                    }
                }
            }
            if (dealBlock?.consumerExist == false)
            {
                if (minPercentCost != null)
                {
                    AddError("minPercentCost", minPercentCost, "Минимальная полная стоимость кредита (займа) в процентах годовых ФЛ_22.1 должна отсутствовать");
                }

                if (maxPercentCost != null)
                {
                    AddError("maxPercentCost", maxPercentCost, "Минимальная полная стоимость кредита (займа) в денежном выражении ФЛ_22.2 должна отсутствовать");
                }

                if (minCost != null)
                {
                    AddError("minCost", minCost, "Максимальная полная стоимость кредита (займа) в процентах годовых ФЛ_22.4 должна отсутствовать");
                }

                if (maxCost != null)
                {
                    AddError("maxCost", maxCost, "Максимальная полная стоимость кредита (займа) в денежном выражении ФЛ_22.5 должна отсутствовать");
                }

                if (maxPercentCash != null)
                {
                    AddError("maxPercentCash", maxPercentCash, "Максимальная полная стоимость кредита (займа) в процентах годовых при его использовании путем снятия наличных денежных средств ФЛ_22.6 должна отсутствовать");
                }

                if (maxPercentCashless != null)
                {
                    AddError("maxPercentCashless", maxPercentCashless, "Максимальная полная стоимость кредита (займа) в процентах годовых при его использовании в безналичном порядке ФЛ_22.7 должна отсутствовать");
                }
            }
            if (minPercentCost.IsNotNull() && minCost.IsNotNull()
                 && (decimal.Parse(minPercentCost) != 0 || decimal.Parse(minCost) != 0)
                 && (decimal.Parse(minPercentCost) <= 0 || decimal.Parse(minCost) <= 0))
            {
                AddError("minPercentCost", minPercentCost, "Минимальная полная стоимость кредита (займа) в процентах годовых ФЛ_22.1 и минимальная полная стоимость кредита (займа) в денежном выражении ФЛ_22.2 должны быть одновременно больше 0 либо равны 0");
                AddError("minCost", minCost, "Минимальная полная стоимость кредита (займа) в процентах годовых ФЛ_22.1 и минимальная полная стоимость кредита (займа) в денежном выражении ФЛ_22.2 должны быть одновременно больше 0 либо равны 0");
            }

            if (calcDate.DateGreater(eventData?.eventDate))
            {
                AddError("calcDate", calcDate, "Дата расчета полной стоимости кредита (займа) ФЛ_22.3 позднее даты наступления события eventDate");
            }

            if (maxPercentCost.DecimalLess(minPercentCost))
            {
                AddError("maxPercentCost", maxPercentCost, "Максимальная полная стоимость кредита (займа) в процентах годовых ФЛ_22.4 меньше минимальной полной стоимости кредита (займа) в процентах годовых ФЛ_22.1");
            }

            if (maxCost.DecimalLess(minCost))
            {
                AddError("maxCost", maxCost, "Максимальная полная стоимость кредита (займа) в денежном выражении ФЛ_22.5 меньше минимальной полной стоимости кредита (займа) в денежном выражении ФЛ_22.2");
            }

            if (maxPercentCash.DecimalLess(minPercentCost))
            {
                AddError("maxPercentCash", maxPercentCash, "Максимальная полная стоимость кредита (займа) в процентах годовых при его использовании путем снятия наличных денежных средств ФЛ_22.6 меньше минимальной полной стоимости кредита (займа) в процентах годовых ФЛ_22.1");
            }

            if (maxPercentCashless.DecimalLess(minPercentCost))
            {
                AddError("maxPercentCashless", maxPercentCashless, "Максимальная полная стоимость кредита (займа) в процентах годовых при его использовании в безналичном порядке ФЛ_22.7 меньше минимальной полной стоимости кредита (займа) в процентах годовых ФЛ_22.1");
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_22_TotalCost_Type block, EventData eventData, IFL_17_DealUid dealUidBlock = null, FL_18_Deal_Type dealBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock, dealBlock)).OrEmptyIfNull().ToList();
        }
    }
}
