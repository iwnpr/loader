using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_22_TotalCost_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_22_TotalCost;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock, FL_18_Deal_Type? dealBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (dealBlock?.consumerExist == true)
            {
                if (dealUidBlock?.openDate.DateGreaterEqual("2014-07-01") == true)
                {
                    if (minPercentCost == null)
                    {
                        errorData.AddError("minPercentCost", minPercentCost, "Отсутствует минимальная полная стоимость кредита (займа) в процентах годовых ФЛ_22.1");
                    }

                    if (maxPercentCost == null)
                    {
                        errorData.AddError("maxPercentCost", maxPercentCost, "Отсутствует максимальная полная стоимость кредита (займа) в процентах годовых ФЛ_22.4");
                    }
                }

                if (dealUidBlock?.openDate.DateGreaterEqual("2018-06-24") == true)
                {
                    if (minCost == null)
                    {
                        errorData.AddError("minCost", minCost, "Отсутствует минимальная полная стоимость кредита (займа) в денежном выражении ФЛ_22.2");
                    }

                    if (maxCost == null)
                    {
                        errorData.AddError("maxCost", maxCost, "Отсутствует максимальная полная стоимость кредита (займа) в денежном выражении ФЛ_22.5");
                    }
                }

                if (dealUidBlock?.openDate.DateGreaterEqual("2024-01-22") == true)
                {
                    if (maxPercentCash == null)
                    {
                        errorData.AddError("maxPercentCash", maxPercentCash, "Отсутствует максимальная полная стоимость кредита (займа) в процентах годовых при его использовании путем снятия наличных денежных средств ФЛ_22.6");
                    }

                    if (maxPercentCashless == null)
                    {
                        errorData.AddError("maxPercentCashless", maxPercentCashless, "Отсутствует максимальная полная стоимость кредита (займа) в процентах годовых при его использовании в безналичном порядке ФЛ_22.7");
                    }
                }
            }
            if (dealBlock?.consumerExist == false)
            {
                if (minPercentCost != null)
                {
                    errorData.AddError("minPercentCost", minPercentCost, "Минимальная полная стоимость кредита (займа) в процентах годовых ФЛ_22.1 должна отсутствовать");
                }

                if (maxPercentCost != null)
                {
                    errorData.AddError("maxPercentCost", maxPercentCost, "Минимальная полная стоимость кредита (займа) в денежном выражении ФЛ_22.2 должна отсутствовать");
                }

                if (minCost != null)
                {
                    errorData.AddError("minCost", minCost, "Максимальная полная стоимость кредита (займа) в процентах годовых ФЛ_22.4 должна отсутствовать");
                }

                if (maxCost != null)
                {
                    errorData.AddError("maxCost", maxCost, "Максимальная полная стоимость кредита (займа) в денежном выражении ФЛ_22.5 должна отсутствовать");
                }

                if (maxPercentCash != null)
                {
                    errorData.AddError("maxPercentCash", maxPercentCash, "Максимальная полная стоимость кредита (займа) в процентах годовых при его использовании путем снятия наличных денежных средств ФЛ_22.6 должна отсутствовать");
                }

                if (maxPercentCashless != null)
                {
                    errorData.AddError("maxPercentCashless", maxPercentCashless, "Максимальная полная стоимость кредита (займа) в процентах годовых при его использовании в безналичном порядке ФЛ_22.7 должна отсутствовать");
                }
            }
            if ((minPercentCost.DecimalNotEqual(0) || minCost.DecimalNotEqual(0))
                 && (minPercentCost.DecimalLessEqual(0) || minCost.DecimalLessEqual(0)))
            {
                errorData.AddError("minPercentCost", minPercentCost, "Минимальная полная стоимость кредита (займа) в процентах годовых ФЛ_22.1 и минимальная полная стоимость кредита (займа) в денежном выражении ФЛ_22.2 должны быть одновременно больше 0 либо равны 0");
                errorData.AddError("minCost", minCost, "Минимальная полная стоимость кредита (займа) в процентах годовых ФЛ_22.1 и минимальная полная стоимость кредита (займа) в денежном выражении ФЛ_22.2 должны быть одновременно больше 0 либо равны 0");
            }

            if (calcDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("calcDate", calcDate, "Дата расчета полной стоимости кредита (займа) ФЛ_22.3 позднее даты и времени наступления события eventDateTime");
            }

            if (maxPercentCost.DecimalLess(minPercentCost))
            {
                errorData.AddError("maxPercentCost", maxPercentCost, "Максимальная полная стоимость кредита (займа) в процентах годовых ФЛ_22.4 меньше минимальной полной стоимости кредита (займа) в процентах годовых ФЛ_22.1");
            }

            if (maxCost.DecimalLess(minCost))
            {
                errorData.AddError("maxCost", maxCost, "Максимальная полная стоимость кредита (займа) в денежном выражении ФЛ_22.5 меньше минимальной полной стоимости кредита (займа) в денежном выражении ФЛ_22.2");
            }

            if (maxPercentCash.DecimalLess(minPercentCost))
            {
                errorData.AddError("maxPercentCash", maxPercentCash, "Максимальная полная стоимость кредита (займа) в процентах годовых при его использовании путем снятия наличных денежных средств ФЛ_22.6 меньше минимальной полной стоимости кредита (займа) в процентах годовых ФЛ_22.1");
            }

            if (maxPercentCashless.DecimalLess(minPercentCost))
            {
                errorData.AddError("maxPercentCashless", maxPercentCashless, "Максимальная полная стоимость кредита (займа) в процентах годовых при его использовании в безналичном порядке ФЛ_22.7 меньше минимальной полной стоимости кредита (займа) в процентах годовых ФЛ_22.1");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_22_TotalCost_Type block, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null, FL_18_Deal_Type? dealBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock, dealBlock)).OrEmptyIfNull()];
        }
    }
}
