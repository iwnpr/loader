using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_53_ServiceOrg_Type : Block_Type
    {
        public FL_53_ServiceOrg_Type()
        {
            blockName = refBlockNameType.FL_53_ServiceOrg;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            if (regRusExist == true && !regNum.DigitCheck(false, 13, 15))
            {
                AddError("regNum", regNum, "Длина регистрационного номера обслуживающей организации ФЛ_53.5 не равна 13 или 15 цифрам");
            }

            foreach (var taxNum in TaxNum_group_FL_53_ServiceOrg.OrEmptyIfNull())
            {
                if (taxNum.taxCode == 1 && taxNum.taxNum.Length != 10)
                {
                    AddError("taxNum", taxNum.taxNum, "Длина номера налогоплательщика ФЛ_53.7 не равна 10 символам");
                }
            }

            if (date.DateGreater(eventData?.eventDate))
            {
                AddError("date", date, "Дата начала действия договора обслуживания ФЛ_53.8 позднее даты наступления события eventDate");
            }

            if (endDateSpecified && endDate.DateLess(date))
            {
                AddError("endDate", endDate, "Дата окончания действия договора обслуживания ФЛ_53.9 раньше даты начала действия договора обслуживания ФЛ_53.8");
            }

            if (!issuerRegNum.DigitCheck(false, 13, 15))
            {
                AddError("issuerRegNum", issuerRegNum, "Длина регистрационного номера эмитента ФЛ_53.11 не равна 13 или 15 цифрам");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_53_ServiceOrg_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
