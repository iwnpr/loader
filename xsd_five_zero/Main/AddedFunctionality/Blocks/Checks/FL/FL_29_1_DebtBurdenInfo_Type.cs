using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_29_1_DebtBurdenInfo_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_29_1_DebtBurdenInfo;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock, I_FL_55_Application_Type? applicationBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (loadCalcDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("loadCalcDate", loadCalcDate, "Дата расчета показателя долговой нагрузки заемщика ФЛ_29(1).2 позднее даты и времени наступления события eventDateTime");
            }

            if (incomeInfo.In("3", "4", "5") && incomeInfoSource != null)
            {
                foreach (var value in incomeInfoSource.OrEmptyIfNull())
                {
                    errorData.AddError("incomeInfoSource", value, "Неверно указано значение источника сведений о величине среднемесячного дохода заемщика ФЛ_29(1).4");
                }
            }
            if (incomeInfo == "1")
            {
                if (incomeInfoSource == null)
                {
                    errorData.AddError("incomeInfoSource", null, "Отсутствует источник сведений о величине среднемесячного дохода заемщика ФЛ_29(1).4");
                }
                foreach (var value in incomeInfoSource.OrEmptyIfNull().Where(x => !x.In("-", "1", "2", "3", "4", "5", "6", "7", "8")))
                {
                    errorData.AddError("incomeInfoSource", value, "Неверно указано значение источника сведений о величине среднемесячного дохода заемщика ФЛ_29(1).4");
                }
            }
            if (incomeInfo == "2")
            {
                if (incomeInfoSource == null)
                {
                    errorData.AddError("incomeInfoSource", null, "Отсутствует источник сведений о величине среднемесячного дохода заемщика ФЛ_29(1).4");
                }
                foreach (var value in incomeInfoSource.OrEmptyIfNull().Where(x => !x.In("-", "9", "10")))
                {
                    errorData.AddError("incomeInfoSource", value, "Неверно указано значение источника сведений о величине среднемесячного дохода заемщика ФЛ_29(1).4");
                }
            }
            if (incomeInfo == "6")
            {
                if (incomeInfoSource == null)
                {
                    errorData.AddError("incomeInfoSource", null, "Отсутствует источник сведений о величине среднемесячного дохода заемщика ФЛ_29(1).4");
                }
                foreach (var value in incomeInfoSource.OrEmptyIfNull().Where(x => !x.In("-", "11")))
                {
                    errorData.AddError("incomeInfoSource", value, "Неверно указано значение источника сведений о величине среднемесячного дохода заемщика ФЛ_29(1).4");
                }
            }

            if (eventData.EventName.In(refEventNameType.FL_Event_1_2, refEventNameType.FL_Event_1_3) && loadCalculationFact != false)
            {
                errorData.AddError("loadCalculationFact_0/loadCalculationFact_1", null, "Признак расчета показателя долговой нагрузки заемщика по заключенному потребительскому кредиту (займу) ФЛ_29(1).6 не равен 0");
            }


            if (loadCalculationFact == true && dealUidBlock != null && dealUid != dealUidBlock.uid)
            {
                errorData.AddError("dealUid", dealUid, "УИд сделки (УИд обращения) ФЛ_29(1).7 не равен УИду сделки ФЛ_17.1");
            }
            if (loadCalculationFact == false && applicationBlock != null && dealUid != applicationBlock.uid)
            {
                errorData.AddError("dealUid", dealUid, "УИд сделки (УИд обращения) ФЛ_29(1).7 не равен УИду обращения ФЛ_55.4");
            }
            if (!UidHelper.CheckUid(dealUid))
            {
                errorData.AddError("dealUid", dealUid, "Неверный контрольный символ УИДа сделки (обращения) ФЛ_29(1).7");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_29_1_DebtBurdenInfo_Type block, IEventData eventData, ISource source, I_FL_17_DealUid_Type? dealUidBlock = null, FL_18_Deal_Type? dealBlock = null, FL_19_Amount_Type? amountBlock = null, I_FL_55_Application_Type? applicationBlock = null)
        {
            if (source is FL_46_UL_36_OrgSource_Type orgSource
                && orgSource.sourceCode.In(refSourceType.Item1, refSourceType.Item2) && dealBlock?.consumerExist == true
                && ((dealUidBlock?.openDate.DateGreaterEqual("2019-10-01") == true && dealUidBlock?.openDate.DateLess("2024-01-01") == true
                        && amountBlock?.sum.DecimalGreaterEqual(10000) == true && amountBlock?.currency == "RUB")
                    || dealUidBlock?.openDate.DateGreaterEqual("2024-01-01") == true)
                && block == null)
            {
                return [new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_29_1_DebtBurdenInfo, null, null, "Отсутствует блок 29(1). Сведения о долговой нагрузке заемщика")];
            }
            if ((eventData is FL_Event_3_4_Type || eventData is FL_Event_3_4_Type) && block != null)
            {
                return [new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_29_1_DebtBurdenInfo, null, null, "Блок 29(1). Сведения о долговой нагрузке заемщика должен отсутствовать")];
            }

            return [.. (block?.Check(eventData, dealUidBlock, applicationBlock)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_29_1_DebtBurdenInfo_Type> blocks, IEventData eventData, ISource source, I_FL_17_DealUid_Type? dealUidBlock = null, FL_18_Deal_Type? dealBlock = null, FL_19_Amount_Type? amountBlock = null, I_FL_55_Application_Type? applicationBlock = null)
        {
            if (source is FL_46_UL_36_OrgSource_Type orgSource
                && orgSource.sourceCode.In(refSourceType.Item1, refSourceType.Item2) && dealBlock?.consumerExist == true
                && ((dealUidBlock?.openDate.DateGreaterEqual("2019-10-01") == true && dealUidBlock?.openDate.DateLess("2024-01-01") == true
                        && amountBlock?.sum.DecimalGreaterEqual(10000) == true && amountBlock?.currency == "RUB")
                    || dealUidBlock?.openDate.DateGreaterEqual("2024-01-01") == true)
                && !blocks.OrEmptyIfNull().Any())
            {
                return [new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_29_1_DebtBurdenInfo, null, null, "Отсутствует блок 29(1). Сведения о долговой нагрузке заемщика")];
            }
            List<INotificationOfAcceptanceError> errorList = [];
            if ((eventData is FL_Event_3_4_Type || eventData is FL_Event_3_4_Type) && blocks.OrEmptyIfNull().Any())
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_29_1_DebtBurdenInfo, null, null, "Блок 29(1). Сведения о долговой нагрузке заемщика должен отсутствовать"));
            }
            foreach (var block in blocks.OrEmptyIfNull())
            {
                errorList.AddRange(block.Check(eventData, dealUidBlock, applicationBlock));
            }
            return errorList;
        }
    }
}
