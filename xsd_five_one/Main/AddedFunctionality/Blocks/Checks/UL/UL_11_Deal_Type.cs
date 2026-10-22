using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_11_Deal_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_11_Deal;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, UL_10_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if ((eventData.EventName == refEventNameType.UL_Event_2_2_1 && code.In(refContractType.Item1, refContractType.Item2))
               || (role == refLoanAgreementsType.Item1 && !code.In(refContractType.Item1, refContractType.Item99))
               || (role == refLoanAgreementsType.Item2 && !code.In(refContractType.Item9, refContractType.Item10, refContractType.Item11, refContractType.Item12, refContractType.Item13, refContractType.Item99))
               || (role == refLoanAgreementsType.Item3 && !code.In(refContractType.Item6, refContractType.Item7, refContractType.Item8, refContractType.Item99))
               || (role == refLoanAgreementsType.Item4 && !code.In(refContractType.Item2, refContractType.Item99))
               || (role == refLoanAgreementsType.Item6 && !code.In(refContractType.Item14)))
            {
                errorData.AddError("code", code, "Неверное значение кода типа сделки ЮЛ_11.3");
            }

            if (code == refContractType.Item1 && !kindCodeSpecified)
            {
                errorData.AddError("kindCode", kindCode, "Отсутствует код вида займа (кредита) ЮЛ_11.4");
            }

            if (code != refContractType.Item1 && kindCodeSpecified)
            {
                errorData.AddError("kindCode", kindCode, "Код вида займа (кредита) ЮЛ_11.4 должен отсутствовать");
            }

            if (code == refContractType.Item1 && !purposeCode.OrEmptyIfNull().Any())
            {
                errorData.AddError("purposeCode", purposeCode, "Отсутствует код цели займа (кредита) ЮЛ_11.5");
            }

            if (code != refContractType.Item1 && purposeCode.OrEmptyIfNull().Any())
            {
                foreach (var purposeCodeValue in purposeCode)
                {
                    errorData.AddError("purposeCode", purposeCodeValue, "Код цели займа (кредита)  ЮЛ_11.5 должен отсутствовать");
                }
            }

            if (endDate.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("endDate", endDate, "Дата прекращения обязательства субъекта по условиям сделки ЮЛ_11.10 раньше даты совершения сделки ЮЛ_10.4");
            }

            if (!UidHelper.CheckUid(transferUid))
            {
                errorData.AddError("transferUid", transferUid, "Неверный контрольный символ УИда сделки, по которой права кредитора частично переданы другому лицу ЮЛ_11.16");
            }

            if (!(partialExist ?? false) && startDate.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("startDate", startDate, "Дата возникновения обязательства субъекта ЮЛ_11.17 раньше даты совершения сделки ЮЛ_10.4");
            }

            if (startDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("startDate", startDate, "Дата возникновения обязательства субъекта ЮЛ_11.17 позднее даты и времени наступления события eventDateTime");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_11_Deal_Type block, IEventData eventData, UL_10_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }
    }
}