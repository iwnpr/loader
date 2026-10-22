using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_14_PaymentTerms_Type : Block_Type
    {
        public UL_14_PaymentTerms_Type()
        {
            blockName = refBlockNameType.UL_14_PaymentTerms;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IUL_10_DealUid dealUidBlock = null, UL_11_Deal_Type dealBlock = null)
        {
            InitCheck(eventData);

            if (mainPaySum.DecimalGreater(0) && !mainPayDateSpecified)
            {
                AddError("mainPayDate", mainPayDate, "Отсутствует дата ближайшего следующего платежа по основному долгу ЮЛ_14.2");
            }
            if (percentPaySum.DecimalGreater(0) && !percentPayDateSpecified)
            {
                AddError("percentPayDate", percentPayDate, "Отсутствует дата ближайшего следующего платежа по процентам ЮЛ_14.4");
            }

            if (mainPaySum.DecimalEqual(0) && mainPayDateSpecified)
            {
                AddError("mainPayDate", mainPayDate, "Дата ближайшего следующего платежа по основному долгу ЮЛ_14.2 должна отсутствовать");
            }
            if (percentPaySum.DecimalEqual(0) && percentPayDateSpecified)
            {
                AddError("percentPayDate", percentPayDate, "Дата ближайшего следующего платежа по процентам ЮЛ_14.4 должна отсутствовать");
            }

            if (mainPayDate.DateLess(dealBlock?.startDate))
            {
                AddError("mainPayDate", mainPayDate, "Дата ближайшего следующего платежа по основному долгу ЮЛ_14.2 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
            }

            if (percentPayDate.DateLess(dealBlock?.startDate))
            {
                AddError("percentPayDate", percentPayDate, "Дата ближайшего следующего платежа по процентам ЮЛ_14.4 раньше даты возникновения обязательства субъекта ЮЛ_11.17");
            }

            if ((mainPaySum.DecimalGreater(0) || percentPaySum.DecimalGreater(0))
                && !freqCodeSpecified)
            {
                AddError("freqCode", freqCode, "Отсутствует код частоты платежей ЮЛ_14.5");
            }


            if (mainPaySum.DecimalEqual(0) && percentPaySum.DecimalEqual(0))
            {
                if (freqCodeSpecified)
                {
                    AddError("freqCode", freqCode, "Код частоты платежей ЮЛ_14.5 должен отсутствовать");
                }

                //if (minCardPaySpecified)
                //{
                //    AddError("minCardPay", minCardPay, "Сумма минимального платежа по кредитной карте ЮЛ_14.6 должна отсутствовать");
                //}

                if (graceDateSpecified)
                {
                    AddError("graceDate", graceDate, "Дата начала беспроцентного периода ЮЛ_14.7 должна отсутствовать");
                }

                if (graceEndDateSpecified)
                {
                    AddError("graceEndDate", graceEndDate, "Дата окончания беспроцентного периода ЮЛ_14.8 должна отсутствовать");
                }

                if (percentEndDateSpecified)
                {
                    AddError("percentEndDate", percentEndDate, "Дата окончания срока уплаты процентов ЮЛ_14.9 должна отсутствовать");
                }
            }

            if (graceDate.DateGreater(eventData?.eventDate))
            {
                AddError("graceDate", graceDate, "Дата начала беспроцентного периода ЮЛ_14.7 позднее даты наступления события eventDate");
            }

            if (graceDate.DateLess(dealUidBlock?.openDate))
            {
                AddError("graceDate", graceDate, "Дата начала беспроцентного периода ЮЛ_14.7 раньше даты совершения сделки ЮЛ_10.4");
            }

            if (graceEndDate.DateLess(graceDate))
            {
                AddError("graceEndDate", graceEndDate, "Дата окончания беспроцентного периода ЮЛ_14.8 раньше даты начала беспроцентного периода ЮЛ_14.7");
            }

            if (percentEndDate.DateLess(dealUidBlock?.openDate))
            {
                AddError("percentEndDate", percentEndDate, "Дата окончания срока уплаты процентов ЮЛ_14.9 раньше даты совершения сделки ЮЛ_10.4");
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_14_PaymentTerms_Type block, EventData eventData, IUL_10_DealUid dealUidBlock = null, UL_11_Deal_Type dealBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock, dealBlock)).OrEmptyIfNull().ToList();
        }
    }
}
