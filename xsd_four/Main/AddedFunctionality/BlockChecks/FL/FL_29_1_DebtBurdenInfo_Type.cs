using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_29_1_DebtBurdenInfo_Type : Block_Type
    {
        public FL_29_1_DebtBurdenInfo_Type()
        {
            blockName = refBlockNameType.FL_29_1_DebtBurdenInfo;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IFL_17_DealUid dealUidBlock = null, FL_55_Application_Type applicationBlock = null)
        {
            InitCheck(eventData);

            if (loadCalcDate.DateGreater(eventData?.eventDate))
            {
                AddError("loadCalcDate", loadCalcDate, "Дата расчета показателя долговой нагрузки заемщика ФЛ_29(1).2 позднее даты наступления события eventDate");
            }

            if (incomeInfo.In("3", "4", "5") && incomeInfoSource != null)
            {
                foreach (var value in incomeInfoSource.OrEmptyIfNull())
                {
                    AddError("incomeInfoSource", value, "Неверно указано значение источника сведений о величине среднемесячного дохода заемщика ФЛ_29(1).4");
                }
            }
            if (incomeInfo == "1")
            {
                if (incomeInfoSource == null)
                {
                    AddError("incomeInfoSource", null, "Неверно указано значение источника сведений о величине среднемесячного дохода заемщика ФЛ_29(1).4");
                }
                foreach (var value in incomeInfoSource.OrEmptyIfNull().Where(x => !x.In("-", "1", "2", "3", "4", "5", "6", "7", "8")))
                {
                    AddError("incomeInfoSource", value, "Неверно указано значение источника сведений о величине среднемесячного дохода заемщика ФЛ_29(1).4");
                }
            }
            if (incomeInfo == "2")
            {
                if (incomeInfoSource == null)
                {
                    AddError("incomeInfoSource", null, "Неверно указано значение источника сведений о величине среднемесячного дохода заемщика ФЛ_29(1).4");
                }
                foreach (var value in incomeInfoSource.OrEmptyIfNull().Where(x => !x.In("-", "9", "10")))
                {
                    AddError("incomeInfoSource", value, "Неверно указано значение источника сведений о величине среднемесячного дохода заемщика ФЛ_29(1).4");
                }
            }
            if (incomeInfo == "6")
            {
                if (incomeInfoSource == null)
                {
                    AddError("incomeInfoSource", null, "Неверно указано значение источника сведений о величине среднемесячного дохода заемщика ФЛ_29(1).4");
                }
                foreach (var value in incomeInfoSource.OrEmptyIfNull().Where(x => !x.In("-", "11")))
                {
                    AddError("incomeInfoSource", value, "Неверно указано значение источника сведений о величине среднемесячного дохода заемщика ФЛ_29(1).4");
                }
            }

            if (eventData.EventName.In(refEventNameType.FL_Event_1_2, refEventNameType.FL_Event_1_3) && loadCalculationFact != false)
            {
                AddError("loadCalculationFact_0/loadCalculationFact_1", null, "Признак расчета показателя долговой нагрузки заемщика по заключенному потребительскому кредиту (займу) ФЛ_29(1).6 не равен 0");
            }


            if (loadCalculationFact == true && dealUidBlock != null && dealUid != dealUidBlock.uid)
            {
                AddError("dealUid", dealUid, "УИд сделки (УИд обращения) ФЛ_29(1).7 не равен УИду сделки ФЛ_17.1");
            }
            if (loadCalculationFact == false && applicationBlock != null && dealUid != applicationBlock.uid)
            {
                AddError("dealUid", dealUid, "УИд сделки (УИд обращения) ФЛ_29(1).7 не равен УИду обращения ФЛ_55.4");
            }
            if (!UidHelper.CheckUid(dealUid))
            {
                AddError("dealUid", dealUid, "Неверный контрольный символ УИДа сделки (обращения) ФЛ_29(1).7");
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_29_1_DebtBurdenInfo_Type block, EventData eventData, ISource source, IFL_17_DealUid dealUidBlock = null, FL_18_Deal_Type dealBlock = null, FL_19_Amount_Type amountBlock = null, FL_55_Application_Type applicationBlock = null)
        {
            var errorList = (block?.Check(eventData, dealUidBlock, applicationBlock)).OrEmptyIfNull().ToList();
            if (source is FL_46_UL_36_OrgSource_Type orgSource
                && orgSource.sourceCode.In(1, 2) && dealBlock?.consumerExist == true
                && ((dealUidBlock?.openDate.DateGreaterEqual("2019-10-01") == true && dealUidBlock?.openDate.DateLess("2024-01-01") == true
                        && amountBlock?.sum.DecimalGreaterEqual(10000) == true && amountBlock?.currency == "RUB")
                    || dealUidBlock?.openDate.DateGreaterEqual("2024-01-01") == true)
                && block == null)
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_29_1_DebtBurdenInfo, null, null, "Отсутствует блок 29(1). Сведения о долговой нагрузке заемщика"));
            }
            return errorList;
        }
    }
}
