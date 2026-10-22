using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_18_Deal_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_18_Deal;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, object? sourceBlock, FL_6_Tax_Type[]? taxBlocks, I_FL_17_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            foreach (var taxBlock in taxBlocks.OrEmptyIfNull())
            {
                if (creditorCode.In(refSourceType.Item1, refSourceType.Item2) && taxBlock?.TaxNum_group_FL_6_Tax != null && taxBlock.TaxNum_group_FL_6_Tax.taxCode == refTaxpayerType.Item1 && taxBlock.TaxNum_group_FL_6_Tax.innChecked == null)
                {
                    errorData.AddError(refBlockNameType.FL_6_Tax, "innChecked_0/innChecked_1", null, "Отсутствует признак проверки ИНН ФЛ_6.2(1)");
                }
            }

            if ((eventData.EventName == refEventNameType.FL_Event_2_2_1 && code.In(refContractType.Item1, refContractType.Item2))
               || (role == refLoanAgreementsType.Item1 && !code.In(refContractType.Item1, refContractType.Item99))
               || (role == refLoanAgreementsType.Item2 && !code.In(refContractType.Item9, refContractType.Item10, refContractType.Item11, refContractType.Item12, refContractType.Item13, refContractType.Item99))
               || (role == refLoanAgreementsType.Item3 && !code.In(refContractType.Item6, refContractType.Item7, refContractType.Item8, refContractType.Item99))
               || (role == refLoanAgreementsType.Item4 && !code.In(refContractType.Item2, refContractType.Item99))
               || (role == refLoanAgreementsType.Item6 && code != refContractType.Item14))
            {
                errorData.AddError("code", code, "Неверное значение кода типа сделки ФЛ_18.3");
            }

            if (code == refContractType.Item1 && !kindCodeSpecified)
            {
                errorData.AddError("kindCode", kindCode, "Отсутствует код вида займа (кредита) ФЛ_18.4");
            }

            if (code != refContractType.Item1 && kindCodeSpecified)
            {
                errorData.AddError("kindCode", kindCode, "Код вида займа (кредита)  ФЛ_18.4 должен отсутствовать");
            }

            //if (kindCode == refLoanFormType.Item18)
            //{
            //    errorData.AddError("kindCode", kindCode, "Код вида займа (кредита)  ФЛ_18.4 равен 18, данный код вводится в действие с 01.07.2026");
            //}

            if (code == refContractType.Item1 && !purposeCode.OrEmptyIfNull().Any())
            {
                errorData.AddError("purposeCode", purposeCode, "Отсутствует код цели займа (кредита) ФЛ_18.5");
            }

            if (code != refContractType.Item1 && purposeCode.OrEmptyIfNull().Any())
            {
                foreach (var purposeCodeValue in purposeCode)
                {
                    errorData.AddError("purposeCode", purposeCodeValue, "Код цели займа (кредита)  ФЛ_18.5 должен отсутствовать");
                }
            }

            if (endDate.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("endDate", endDate, "Дата прекращения обязательства субъекта по условиям сделки ФЛ_18.11 раньше даты совершения сделки ФЛ_17.4");
            }
            if (sourceBlock != null && sourceBlock is FL_46_UL_36_OrgSource_Type orgSourceBlock
                && creditorCode != orgSourceBlock.sourceCode)
            {
                errorData.AddError("creditorCode", creditorCode, "Код вида кредитора – заимодавца ФЛ_18.12 не равен коду источника ФЛ_46.1");
            }

            if (!UidHelper.CheckUid(transferUid))
            {
                errorData.AddError("transferUid", transferUid, "Неверный контрольный символ УИда сделки, по которой права кредитора частично переданы другому лицу ФЛ_18.18");
            }

            if (!(partialExist ?? false) && startDate.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("startDate", startDate, "Дата возникновения обязательства субъекта ФЛ_18.19 раньше даты совершения сделки ФЛ_17.4");
            }

            if (startDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("startDate", startDate, "Дата возникновения обязательства субъекта ФЛ_18.19 позднее даты и времени наступления события eventDateTime");
            }

            if (code == refContractType.Item14 && !installmentCode.OrEmptyIfNull().Any())
            {
                errorData.AddError("installmentCode", installmentCode, "Отсутствует код объекта рассрочки ФЛ_18.23");
            }

            if (code != refContractType.Item14 && installmentCode.OrEmptyIfNull().Any())
            {
                foreach (var installmentCodeValue in installmentCode)
                {
                    errorData.AddError("installmentCode", installmentCodeValue, "Код объекта рассрочки ФЛ_18.23 должен отсутствовать");
                }
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_18_Deal_Type block, IEventData eventData, object? sourceBlock = null, FL_6_Tax_Type[]? taxBlocks = null, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, sourceBlock, taxBlocks, dealUidBlock)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this FL_18_Deal_Type block, IEventData eventData, ISource source, ISubjectTitleData title, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            return Validate(block, eventData, source, title is SubjectTitleDataFL titleFL ? titleFL.FL_6_Tax : null, dealUidBlock);
        }
    }
}