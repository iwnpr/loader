using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_51_OrgAcquirer_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_51_OrgAcquirer;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (fullName.IsEmptyValueDash())
            {
                errorData.AddError("fullName", fullName, $"Полное наименование ФЛ_51.3 не может содержать \"-\"");
            }

            if (regRusExist == true && !regNum.DigitCheck(true, 13))
            {
                errorData.AddError("regNum", regNum, "Длина регистрационного номера ФЛ_51.7 не равна 13 цифрам");
            }

            foreach (var taxNum in TaxNum_group_FL_51_OrgAcquirer.OrEmptyIfNull())
            {
                if (taxNum.taxCode == refTaxpayerType.Item1 && !taxNum.taxNum.DigitCheck(false, 10))
                {
                    errorData.AddError("taxNum", taxNum.taxNum, "Длина номера налогоплательщика ФЛ_51.9 не равна 10 цифрам");
                }
            }

            if (date.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("date", date, "Дата приобретения прав ФЛ_51.10 позднее даты и времени наступления события eventDateTime");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("date", date, "Дата приобретения прав ФЛ_51.10 раньше даты совершения сделки ФЛ_17.4");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_51_OrgAcquirer_Type block, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }
    }
}
