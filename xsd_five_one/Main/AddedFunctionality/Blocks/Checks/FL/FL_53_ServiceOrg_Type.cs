using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_53_ServiceOrg_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_53_ServiceOrg;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (regRusExist == true && !regNum.DigitCheck(false, 13, 15))
            {
                errorData.AddError("regNum", regNum, "Длина регистрационного номера обслуживающей организации ФЛ_53.5 не равна 13 или 15 цифрам");
            }

            foreach (var taxNum in TaxNum_group_FL_53_ServiceOrg.OrEmptyIfNull())
            {
                if (taxNum.taxCode == refTaxpayerType.Item1 && taxNum.taxNum.Length != 10)
                {
                    errorData.AddError("taxNum", taxNum.taxNum, "Длина номера налогоплательщика ФЛ_53.7 не равна 10 символам");
                }
            }

            if (date.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("date", date, "Дата начала действия договора обслуживания ФЛ_53.8 позднее даты и времени наступления события eventDateTime");
            }

            if (endDateSpecified && endDate.DateLess(date))
            {
                errorData.AddError("endDate", endDate, "Дата окончания действия договора обслуживания ФЛ_53.9 раньше даты начала действия договора обслуживания ФЛ_53.8");
            }

            if (!issuerRegNum.DigitCheck(false, 13, 15))
            {
                errorData.AddError("issuerRegNum", issuerRegNum, "Длина регистрационного номера эмитента ФЛ_53.11 не равна 13 или 15 цифрам");
            }

            if (terminationDateSpecified && terminationDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("terminationDate", terminationDate, "Дата прекращения передачи информации ФЛ_53.12 позднее даты и времени наступления события eventDateTime");
            }
            if (terminationDateSpecified && terminationDate.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("terminationDate", terminationDate, "Дата прекращения передачи информации ФЛ_53.12 раньше даты совершения сделки ФЛ_17.4");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_53_ServiceOrg_Type block, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }
    }
}
