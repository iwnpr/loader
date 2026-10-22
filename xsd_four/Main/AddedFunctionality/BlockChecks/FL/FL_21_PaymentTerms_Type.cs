using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_21_PaymentTerms_Type : Block_Type
    {
        public FL_21_PaymentTerms_Type()
        {
            blockName = refBlockNameType.FL_21_PaymentTerms;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IFL_17_DealUid dealUidBlock = null, FL_18_Deal_Type dealBlock = null)
        {
            InitCheck(eventData);

            if (mainPaySum.DecimalGreater(0) && !mainPayDateSpecified)
            {
                AddError("mainPayDate", mainPayDate, "Отсутствует дата ближайшего следующего платежа по основному долгу ФЛ_21.2");
            }
            if (percentPaySum.DecimalGreater(0) && !percentPayDateSpecified)
            {
                AddError("percentPayDate", percentPayDate, "Отсутствует дата ближайшего следующего платежа по процентам ФЛ_21.4");
            }

            if (mainPaySum.DecimalEqual(0) && mainPayDateSpecified)
            {
                AddError("mainPayDate", mainPayDate, "Дата ближайшего следующего платежа по основному долгу ФЛ_21.2 должна отсутствовать");
            }
            if (percentPaySum.DecimalEqual(0) && percentPayDateSpecified)
            {
                AddError("percentPayDate", percentPayDate, "Дата ближайшего следующего платежа по процентам ФЛ_21.4 должна отсутствовать");
            }

            if (mainPayDate.DateLess(dealBlock?.startDate))
            {
                AddError("mainPayDate", mainPayDate, "Дата ближайшего следующего платежа по основному долгу ФЛ_21.2 раньше даты возникновения обязательства субъекта ФЛ_18.19");
            }

            if (percentPayDate.DateLess(dealBlock?.startDate))
            {
                AddError("percentPayDate", percentPayDate, "Дата ближайшего следующего платежа по процентам ФЛ_21.4 раньше даты возникновения обязательства субъекта ФЛ_18.19");
            }


            if ((mainPaySum.DecimalGreater(0) || percentPaySum.DecimalGreater(0))
                && !freqCodeSpecified)
            {
                AddError("freqCode", freqCode, "Отсутствует код частоты платежей ФЛ_21.5");
            }


            if (mainPaySum.DecimalEqual(0) && percentPaySum.DecimalEqual(0))
            {
                if (freqCodeSpecified)
                {
                    AddError("freqCode", freqCode, "Код частоты платежей ФЛ_21.5 должен отсутствовать");
                }

                //if (minCardPaySpecified)
                //{
                //    AddError("minCardPay", minCardPay, "Сумма минимального платежа по кредитной карте ФЛ_21.6 должна отсутствовать");
                //}

                if (graceDateSpecified)
                {
                    AddError("graceDate", graceDate, "Дата начала беспроцентного периода ФЛ_21.7 должна отсутствовать");
                }

                if (graceEndDateSpecified)
                {
                    AddError("graceEndDate", graceEndDate, "Дата окончания беспроцентного периода ФЛ_21.8 должна отсутствовать");
                }

                if (percentEndDateSpecified)
                {
                    AddError("percentEndDate", percentEndDate, "Дата окончания срока уплаты процентов ФЛ_21.9 должна отсутствовать");
                }
            }

            if (graceDate.DateGreater(eventData?.eventDate))
            {
                AddError("graceDate", graceDate, "Дата начала беспроцентного периода ФЛ_21.7 позднее даты наступления события eventDate");
            }

            if (graceDate.DateLess(dealUidBlock?.openDate))
            {
                AddError("graceDate", graceDate, "Дата начала беспроцентного периода ФЛ_21.7 раньше даты совершения сделки ФЛ_17.4");
            }

            if (graceEndDate.DateLess(graceDate))
            {
                AddError("graceEndDate", graceEndDate, "Дата окончания беспроцентного периода ФЛ_21.8 раньше даты начала беспроцентного периода ФЛ_21.7");
            }

            if (percentEndDate.DateLess(dealUidBlock?.openDate))
            {
                AddError("percentEndDate", percentEndDate, "Дата окончания срока уплаты процентов ФЛ_21.9 раньше даты совершения сделки ФЛ_17.4");
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_21_PaymentTerms_Type block, EventData eventData, IFL_17_DealUid dealUidBlock = null, FL_18_Deal_Type dealBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock, dealBlock)).OrEmptyIfNull().ToList();
        }
    }
}
