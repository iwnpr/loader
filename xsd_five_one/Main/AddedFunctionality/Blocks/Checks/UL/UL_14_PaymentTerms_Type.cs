using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_14_PaymentTerms_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_14_PaymentTerms;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock, UL_11_Deal_Type? dealBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (mainPaySum.DecimalGreater(0) && !mainPayDateSpecified)
            {
                errorData.AddError("mainPayDate", mainPayDate, "Отсутствует дата ближайшего следующего платежа по основному долгу ЮЛ_14.2");
            }
            if (percentPaySum.DecimalGreater(0) && !percentPayDateSpecified)
            {
                errorData.AddError("percentPayDate", percentPayDate, "Отсутствует дата ближайшего следующего платежа по процентам ЮЛ_14.4");
            }

            if (mainPaySum.DecimalEqual(0) && mainPayDateSpecified)
            {
                errorData.AddError("mainPayDate", mainPayDate, "Дата ближайшего следующего платежа по основному долгу ЮЛ_14.2 должна отсутствовать");
            }
            if (percentPaySum.DecimalEqual(0) && percentPayDateSpecified)
            {
                errorData.AddError("percentPayDate", percentPayDate, "Дата ближайшего следующего платежа по процентам ЮЛ_14.4 должна отсутствовать");
            }

            if (mainPayDate.DateLess(dealBlock?.startDate))
            {
                errorData.AddError("mainPayDate", mainPayDate, "Дата ближайшего следующего платежа по основному долгу ЮЛ_14.2 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
            }
            if (percentPayDate.DateLess(dealBlock?.startDate))
            {
                errorData.AddError("percentPayDate", percentPayDate, "Дата ближайшего следующего платежа по процентам ЮЛ_14.4 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
            }

            if ((mainPaySum.DecimalGreater(0) || percentPaySum.DecimalGreater(0))
                && !freqCodeSpecified)
            {
                errorData.AddError("freqCode", freqCode, "Отсутствует код частоты платежей ЮЛ_14.5");
            }


            //if (mainPaySum.DecimalEqual(0) && percentPaySum.DecimalEqual(0))
            //{
            //    if (freqCodeSpecified)
            //    {
            //        errorData.AddError("freqCode", freqCode, "Код частоты платежей ЮЛ_14.5 должен отсутствовать");
            //    }

            //    if (minPaySpecified)
            //    {
            //        errorData.AddError("minCardPay", minPay, "Сумма минимального платежа по кредитной карте ЮЛ_14.6 должна отсутствовать");
            //    }

            //    if (graceDateSpecified)
            //    {
            //        errorData.AddError("graceDate", graceDate, "Дата начала беспроцентного периода ЮЛ_14.7 должна отсутствовать");
            //    }

            //    if (graceEndDateSpecified)
            //    {
            //        errorData.AddError("graceEndDate", graceEndDate, "Дата окончания беспроцентного периода ЮЛ_14.8 должна отсутствовать");
            //    }

            //    if (percentEndDateSpecified)
            //    {
            //        errorData.AddError("percentEndDate", percentEndDate, "Дата окончания срока уплаты процентов ЮЛ_14.9 должна отсутствовать");
            //    }
            //}

            if (graceDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("graceDate", graceDate, "Дата начала беспроцентного периода ЮЛ_14.7 позднее даты и времени наступления события eventDateTime");
            }

            if (graceDate.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("graceDate", graceDate, "Дата начала беспроцентного периода ЮЛ_14.7 раньше даты совершения сделки ЮЛ_10.4");
            }

            if (graceEndDate.DateLess(graceDate))
            {
                errorData.AddError("graceEndDate", graceEndDate, "Дата окончания беспроцентного периода ЮЛ_14.8 раньше даты начала беспроцентного периода ЮЛ_14.7");
            }

            if (percentEndDate.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("percentEndDate", percentEndDate, "Дата окончания срока уплаты процентов ЮЛ_14.9 раньше даты совершения сделки ЮЛ_10.4");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_14_PaymentTerms_Type block, IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock = null, UL_11_Deal_Type? dealBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock, dealBlock)).OrEmptyIfNull()];
        }
    }
}
