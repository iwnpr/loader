using loader_utils;
namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_41_OrgAcquirer_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_41_OrgAcquirer;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (fullName.IsEmptyValueDash())
            {
                errorData.AddError("fullName", fullName, $"Полное наименование ЮЛ_41.3 не может содержать \"-\"");
            }

            if (regRusExist == true && !regNum.DigitCheck(true, 13))
            {
                errorData.AddError("regNum", regNum, "Длина регистрационного номера ЮЛ_41.7 не равна 13 цифрам");
            }
            foreach (var taxNum in TaxNum_group_UL_41_OrgAcquirer.OrEmptyIfNull())
            {
                if (taxNum.taxCode == refTaxpayerType.Item1 && taxNum.taxNum.Length != 10)
                {
                    errorData.AddError("taxNum", taxNum.taxNum, "Длина номера налогоплательщика ЮЛ_41.9 не равна 10 символам");
                }
            }

            if (date.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("date", date, "Дата приобретения прав ЮЛ_41.10 позднее даты и времени наступления события eventDateTime");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("date", date, "Дата приобретения прав ЮЛ_41.10 раньше даты совершения сделки ЮЛ_10.4");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_41_OrgAcquirer_Type block, IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }
    }
}
