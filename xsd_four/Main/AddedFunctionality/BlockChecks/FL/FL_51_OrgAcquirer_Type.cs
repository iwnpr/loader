using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_51_OrgAcquirer_Type : Block_Type
    {
        public FL_51_OrgAcquirer_Type()
        {
            blockName = refBlockNameType.FL_51_OrgAcquirer;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (regRusExist == true && !regNum.DigitCheck(true, 13))
            {
                AddError("regNum", regNum, "Длина регистрационного номера ФЛ_51.7 не равна 13 цифрам");
            }
            foreach (var taxNum in TaxNum_group_FL_51_OrgAcquirer.OrEmptyIfNull())
            {
                if (taxNum.taxCode == 1 && taxNum.taxNum.Length != 10)
                {
                    AddError("taxNum", taxNum.taxNum, "Длина номера налогоплательщика ФЛ_51.9 не равна 10 символам");
                }
            }

            if (date.DateGreater(eventData?.eventDate))
            {
                AddError("date", date, "Дата приобретения прав ФЛ_51.10 позднее даты наступления события eventDate");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                AddError("date", date, "Дата приобретения прав ФЛ_51.10 раньше даты совершения сделки ФЛ_17.4");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_51_OrgAcquirer_Type block, EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}
