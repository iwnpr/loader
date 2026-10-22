using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_21_PaymentTerms_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_21_PaymentTerms;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock, FL_18_Deal_Type? dealBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (mainPaySum.DecimalGreater(0) && !mainPayDateSpecified)
            {
                errorData.AddError("mainPayDate", mainPayDate, "Отсутствует дата ближайшего следующего платежа по основному долгу ФЛ_21.2");
            }
            if (mainPaySum.DecimalEqual(0) && mainPayDateSpecified)
            {
                errorData.AddError("mainPayDate", mainPayDate, "Дата ближайшего следующего платежа по основному долгу ФЛ_21.2 должна отсутствовать");
            }
            if (mainPayDate.DateLess(dealBlock?.startDate))
            {
                errorData.AddError("mainPayDate", mainPayDate, "Дата ближайшего следующего платежа по основному долгу ФЛ_21.2 раньше даты возникновения обязательства субъекта ФЛ_18.19");
            }


            if (percentPaySum.DecimalGreater(0) && !percentPayDateSpecified)
            {
                errorData.AddError("percentPayDate", percentPayDate, "Отсутствует дата ближайшего следующего платежа по процентам ФЛ_21.4");
            }
            if (percentPaySum.DecimalEqual(0) && percentPayDateSpecified)
            {
                errorData.AddError("percentPayDate", percentPayDate, "Дата ближайшего следующего платежа по процентам ФЛ_21.4 должна отсутствовать");
            }
            if (percentPayDate.DateLess(dealBlock?.startDate))
            {
                errorData.AddError("percentPayDate", percentPayDate, "Дата ближайшего следующего платежа по процентам ФЛ_21.4 раньше даты возникновения обязательства субъекта ФЛ_18.19");
            }


            if ((mainPaySum.DecimalGreater(0) || percentPaySum.DecimalGreater(0))
                && !freqCodeSpecified)
            {
                errorData.AddError("freqCode", freqCode, "Отсутствует код частоты платежей ФЛ_21.5");
            }


            //if (mainPaySum.DecimalEqual(0) && percentPaySum.DecimalEqual(0))
            //{
            //    if (freqCodeSpecified)
            //    {
            //        errorData.AddError("freqCode", freqCode, "Код частоты платежей ФЛ_21.5 должен отсутствовать");
            //    }

            //    if (minPaySpecified)
            //    {
            //        errorData.AddError("minPay", minPay, "Сумма минимального платежа по кредитной карте ФЛ_21.6 должна отсутствовать");
            //    }

            //    if (graceDateSpecified)
            //    {
            //        errorData.AddError("graceDate", graceDate, "Дата начала беспроцентного периода ФЛ_21.7 должна отсутствовать");
            //    }

            //    if (graceEndDateSpecified)
            //    {
            //        errorData.AddError("graceEndDate", graceEndDate, "Дата окончания беспроцентного периода ФЛ_21.8 должна отсутствовать");
            //    }

            //    if (percentEndDateSpecified)
            //    {
            //        errorData.AddError("percentEndDate", percentEndDate, "Дата окончания срока уплаты процентов ФЛ_21.9 должна отсутствовать");
            //    }
            //}

            if (graceDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("graceDate", graceDate, "Дата начала беспроцентного периода ФЛ_21.7 позднее даты и времени наступления события eventDateTime");
            }

            if (graceDate.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("graceDate", graceDate, "Дата начала беспроцентного периода ФЛ_21.7 раньше даты совершения сделки ФЛ_17.4");
            }

            if (graceEndDate.DateLess(graceDate))
            {
                errorData.AddError("graceEndDate", graceEndDate, "Дата окончания беспроцентного периода ФЛ_21.8 раньше даты начала беспроцентного периода ФЛ_21.7");
            }

            if (percentEndDate.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("percentEndDate", percentEndDate, "Дата окончания срока уплаты процентов ФЛ_21.9 раньше даты совершения сделки ФЛ_17.4");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_21_PaymentTerms_Type block, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null, FL_18_Deal_Type? dealBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock, dealBlock)).OrEmptyIfNull()];
        }
    }
}
