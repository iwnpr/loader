using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_6_Tax_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_6_Tax;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData? eventData, bool isAF)
        {
            ErrorData errorData = new(blockName, eventData);



            if (TaxNum_group_FL_6_Tax != null)
            {
                if (TaxNum_group_FL_6_Tax.taxCode == refTaxpayerType.Item1)
                {
                    if (TaxNum_group_FL_6_Tax.taxNum != null && TaxNum_group_FL_6_Tax.taxNum.Length != 12)
                    {
                        errorData.AddError("taxNum", TaxNum_group_FL_6_Tax.taxNum, "Номер налогоплательщика ФЛ_6.2 не равен 12 цифрам");
                    }

                    if (TaxNum_group_FL_6_Tax.innChecked != null && TaxNum_group_FL_6_Tax.innChecked != true)
                    {
                        errorData.AddError("innChecked_0/innChecked_1", null, "Признак проверки ИНН ФЛ_6.2(1) не равен «1»");
                    }
                }

                if (isAF && TaxNum_group_FL_6_Tax.taxCode != refTaxpayerType.Item1)
                {
                    errorData.AddError("taxCode", TaxNum_group_FL_6_Tax.taxCode, "Код номера налогоплательщика должен быть равен «1»");
                }

                if (TaxNum_group_FL_6_Tax.taxCode == refTaxpayerType.Item2 && TaxNum_group_FL_6_Tax.innChecked != null)
                {
                    errorData.AddError("innChecked_0/innChecked_1", null, "Признак проверки ИНН должен отсутствовать ФЛ_6.2(1)");
                }

                if (isAF && TaxNum_group_FL_6_Tax.innChecked == null)
                {
                    errorData.AddError("innChecked_0/innChecked_1", null, "Отсутствует признак проверки ИНН");
                }
            }
            if (isAF && TaxNum_group_FL_6_Tax == null)
            {
                errorData.AddError(null, null, "Отсутствует код номера налогоплательщика ФЛ_6.1");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_6_Tax_Type>? blocks, IEventData? eventData, FL_1_4_Group_Type? fl_1_4_block = null, bool isAF = false)
        {
            List<INotificationOfAcceptanceError> errorList = [];

            if (fl_1_4_block != null && isAF && !blocks.OrEmptyIfNull().Any())
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_6_Tax, "Отсутствует блок 6. Номер налогоплательщика, регистрационный номер и признак специального налогового режима"));
            }
            foreach (var block in blocks.OrEmptyIfNull())
            {
                errorList.AddRange(block.Check(eventData, isAF));
            }
            if (eventData?.EventName != refEventNameType.FL_Event_1_7 && blocks.OrEmptyIfNull().GroupBy(x => x.regNum).Count() > 1)
            {
                foreach (var regNum in blocks.OrEmptyIfNull().Select(x => x.regNum).Distinct())
                {
                    errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_6_Tax, "regNum", regNum, "Регистрационные номера ФЛ_6.3 во всех блоках ФЛ_6 неидентичны"));
                }
            }
            if (blocks.OrEmptyIfNull().Where(x => x.TaxNum_group_FL_6_Tax?.innChecked == true).GroupBy(x => x.TaxNum_group_FL_6_Tax?.taxNum).Count() > 1)
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_6_Tax, "innChecked_0/innChecked_1", null, "У субъекта кредитной истории несколько проверенных ИНН", false, true));
            }
            foreach (var block in blocks.OrEmptyIfNull().Where(x => x.specialMode != null && x.specialMode != blocks.OrEmptyIfNull().First().specialMode))
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_6_Tax, "specialMode_0/specialMode_1", null, "Признаки специального налогового режима ФЛ_6.4 во всех блоках ФЛ_6 неидентичны"));
            }
            return errorList;
        }
    }
}
