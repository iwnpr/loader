using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_41_OrgAcquirer_Type : Block_Type
    {
        public UL_41_OrgAcquirer_Type()
        {
            blockName = refBlockNameType.UL_41_OrgAcquirer;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IUL_10_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (regRusExist == true && !regNum.DigitCheck(true, 13))
            {
                AddError("regNum", regNum, "Длина регистрационного номера ЮЛ_41.7 не равна 13 цифрам");
            }
            foreach (var taxNum in TaxNum_group_UL_41_OrgAcquirer.OrEmptyIfNull())
            {
                if (taxNum.taxCode == 1 && taxNum.taxNum.Length != 10)
                {
                    AddError("taxNum", taxNum.taxNum, "Длина номера налогоплательщика ЮЛ_41.9 не равна 10 символам");
                }
            }

            if (date.DateGreater(eventData?.eventDate))
            {
                AddError("date", date, "Дата приобретения прав ЮЛ_41.10 позднее даты наступления события eventDate");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                AddError("date", date, "Дата приобретения прав ЮЛ_41.10 раньше даты совершения сделки ЮЛ_10.4");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_41_OrgAcquirer_Type block, EventData eventData, IUL_10_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}
