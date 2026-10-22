using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_18_Deal_Type : Block_Type
    {
        public FL_18_Deal_Type()
        {
            blockName = refBlockNameType.FL_18_Deal;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, object sourceBlock = null, FL_6_Tax_Type[] taxBlocks = null, IFL_17_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            foreach (var taxBlock in taxBlocks.OrEmptyIfNull())
            {
                if (creditorCode.In(1, 2) && taxBlock?.TaxNum_group_FL_6_Tax != null && taxBlock.TaxNum_group_FL_6_Tax.taxCode == 1 && taxBlock.TaxNum_group_FL_6_Tax.innChecked == null)
                {
                    errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_6_Tax, "innChecked_0/innChecked_1", null, "Отсутствует признак проверки ИНН ФЛ_6.2(1)"));
                }
            }
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

            if ((eventData.EventName == refEventNameType.FL_Event_2_2_1 && code.In(1, 2))
               || (role == 1 && !code.In(1, 99))
               || (role == 2 && !code.In(9, 10, 11, 12, 13, 99))
               || (role == 3 && !code.In(6, 7, 8, 99))
               || (role == 4 && !code.In(2, 99)))
            {
                AddError("code", code, "Неверное значение кода типа сделки ФЛ_18.3");
            }

            if (code == 1 && !kindCodeSpecified)
            {
                AddError("kindCode", kindCode, "Отсутствует код вида займа (кредита) ФЛ_18.4");
            }
            if (code != 1 && kindCodeSpecified)
            {
                AddError("kindCode", kindCode, "Код вида займа (кредита)  ФЛ_18.4 должен отсутствовать");
            }
            if (code == 1 && purposeCode.OrEmptyIfNull().Count() == 0)
            {
                AddError("purposeCode", purposeCode, "Отсутствует код цели займа (кредита) ФЛ_18.5");
            }
            if (code != 1 && purposeCode.OrEmptyIfNull().Count() != 0)
            {
                foreach (var purposeCodeValue in purposeCode)
                {
                    AddError("purposeCode", purposeCodeValue, "Код цели займа (кредита)  ФЛ_18.5 должен отсутствовать");
                }
            }
            if (endDate.DateLess(dealUidBlock?.openDate))
            {
                AddError("endDate", endDate, "Дата прекращения обязательства субъекта по условиям сделки ФЛ_18.11 раньше даты совершения сделки ФЛ_17.4");
            }
            if (sourceBlock != null && sourceBlock is FL_46_UL_36_OrgSource_Type orgSourceBlock
                && creditorCode != orgSourceBlock.sourceCode)
            {
                AddError("creditorCode", creditorCode, "Код вида кредитора – заимодавца ФЛ_18.12 не равен коду источника ФЛ_46.1");
            }
            if (!UidHelper.CheckUid(transferUid))
            {
                AddError("transferUid", transferUid, "Неверный контрольный символ УИда сделки, по которой права кредитора частично переданы другому лицу ФЛ_18.18");
            }
            if (startDate.DateLess(dealUidBlock?.openDate))
            {
                AddError("startDate", startDate, "Дата возникновения обязательства субъекта ФЛ_18.19 раньше даты совершения сделки ФЛ_17.4");
            }
            if (startDate.DateGreater(eventData?.eventDate))
            {
                AddError("startDate", startDate, "Дата возникновения обязательства субъекта ФЛ_18.19 позднее даты наступления события eventDate");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_18_Deal_Type block, EventData eventData, object sourceBlock = null, FL_6_Tax_Type[] taxBlocks = null, IFL_17_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, sourceBlock, taxBlocks, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}