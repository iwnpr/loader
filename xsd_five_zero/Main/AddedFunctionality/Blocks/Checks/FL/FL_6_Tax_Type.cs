using loader_utils;

namespace Xsd.VersionFiveZero.Main
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

        public List<INotificationOfAcceptanceError> Check(IEventData? eventData)
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
                if (TaxNum_group_FL_6_Tax.taxCode == refTaxpayerType.Item2 && TaxNum_group_FL_6_Tax.innChecked != null)
                {
                    errorData.AddError("innChecked_0/innChecked_1", null, "Признак проверки ИНН должен отсутствовать ФЛ_6.2(1)");
                }
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_6_Tax_Type> blocks, IEventData? eventData)
        {
            List<INotificationOfAcceptanceError> errorList = [];

            foreach (var block in blocks.OrEmptyIfNull())
            {
                errorList.AddRange(block.Check(eventData));
            }
            if (eventData?.EventName != refEventNameType.FL_Event_1_7 && blocks.OrEmptyIfNull().GroupBy(x => x.regNum).Where(x => x != null).Count() > 1)
            {
                foreach (var regNum in blocks.OrEmptyIfNull().Where(x => x != null).Select(x => x.regNum).Distinct())
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
