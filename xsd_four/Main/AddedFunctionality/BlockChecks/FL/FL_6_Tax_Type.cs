using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_6_Tax_Type : Block_Type
    {
        public FL_6_Tax_Type()
        {
            blockName = refBlockNameType.FL_6_Tax;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            if (TaxNum_group_FL_6_Tax != null)
            {
                if (TaxNum_group_FL_6_Tax.taxCode == 1)
                {
                    if (TaxNum_group_FL_6_Tax.taxNum != null && TaxNum_group_FL_6_Tax.taxNum.Length != 12)
                    {
                        AddError("taxNum", TaxNum_group_FL_6_Tax.taxNum, "Номер налогоплательщика ФЛ_6.2 не равна 12 цифр");
                    }

                    if (TaxNum_group_FL_6_Tax.innChecked != true)
                    {
                        AddError("innChecked_0/innChecked_1", null, "Признак проверки ИНН ФЛ_6.2(1) не равен «1»");
                    }
                    //if (regNum == null)
                    //{
                    //    AddError("regNum", regNum, "Отсутствует регистрационный номер ФЛ_6.3");
                    //}
                    //if (regNum.IsNotNull())
                    //{
                    //    if (regNum.Length == 15)
                    //    {
                    //        AddError("regNum", regNum, "Длина регистрационного номера ФЛ_6.3 не равна 15 символам");
                    //    }
                    //    if (eventName != null && !eventData.EventName.In(refEventNameType.FL_Event_3_1, refEventNameType.FL_Event_3_2, refEventNameType.FL_Event_4_1)
                    //        && individualEntrepreneurBlock != null && regNum != individualEntrepreneurBlock.regNum)
                    //    {
                    //        AddError("regNum", regNum, "Регистрационный номер ФЛ_6.3 не равен регистрационному номеру ФЛ_11.2");
                    //    }
                    //}
                }
                if (TaxNum_group_FL_6_Tax.taxCode == 2 && TaxNum_group_FL_6_Tax.innChecked != null)
                {
                    AddError("innChecked_0/innChecked_1", null, "Признак проверки ИНН должен отсутствовать ФЛ_6.2(1)");
                }
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_6_Tax_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_6_Tax_Type> blocks, EventData eventData)
        {
            List<INotificationOfAcceptanceError> errorList = [];

            if (blocks.OrEmptyIfNull().Where(x => x.TaxNum_group_FL_6_Tax?.innChecked == true).GroupBy(x => x.TaxNum_group_FL_6_Tax?.taxNum).Count() > 1)
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_6_Tax, "innChecked_0/innChecked_1", null, "У субъекта кредитной истории несколько проверенных ИНН", false, true));
            }
            foreach (var block in blocks.OrEmptyIfNull())
            {
                errorList.AddRange(block.Check(eventData));
            }
            return errorList;
        }
    }

}
