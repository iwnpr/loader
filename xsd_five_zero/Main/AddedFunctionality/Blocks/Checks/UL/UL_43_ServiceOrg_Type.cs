using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_43_ServiceOrg_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_43_ServiceOrg;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData)
        {
            ErrorData errorData = new(blockName, eventData);

            if (regRusExist == true && !regNum.DigitCheck(false, 13, 15))
            {
                errorData.AddError("regNum", regNum, "Длина регистрационного номера обслуживающей организации ЮЛ_43.5 не равна 13 или 15 цифрам");
            }

            foreach (var taxNum in TaxNum_group_UL_43_ServiceOrg.OrEmptyIfNull())
            {
                if (taxNum.taxCode == refTaxpayerType.Item1 && taxNum.taxNum.Length != 10)
                {
                    errorData.AddError("taxNum", taxNum.taxNum, "Длина номера налогоплательщика ЮЛ_43.7 не равна 10 символам");
                }
            }

            if (date.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("date", date, "Дата начала действия договора обслуживания ЮЛ_43.8 позднее даты и времени наступления события eventDateTime");
            }

            if (endDateSpecified && endDate.DateLess(date))
            {
                errorData.AddError("endDate", endDate, "Дата окончания действия договора обслуживания ЮЛ_43.9 раньше даты начала действия договора обслуживания ЮЛ_43.8");
            }

            if (!issuerRegNum.DigitCheck(false, 13, 15))
            {
                errorData.AddError("issuerRegNum", issuerRegNum, "Длина регистрационного номера эмитента ЮЛ_43.11 не равна 13 или 15 цифрам");
            }

            if (terminationDateSpecified)
            {
                errorData.AddError("terminationDate", terminationDate, "Дата прекращения передачи информации ЮЛ_43.12 должна отсутствовать");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_43_ServiceOrg_Type block, IEventData eventData)
        {
            return [.. (block?.Check(eventData)).OrEmptyIfNull()];
        }
    }
}
