using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_43_ServiceOrg_Type : Block_Type
    {
        public UL_43_ServiceOrg_Type()
        {
            blockName = refBlockNameType.UL_43_ServiceOrg;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            if (regRusExist == true && !regNum.DigitCheck(false, 13, 15))
            {
                AddError("regNum", regNum, "Длина регистрационного номера обслуживающей организации ЮЛ_43.5 не равна 13 или 15 цифрам");
            }

            foreach (var taxNum in TaxNum_group_UL_43_ServiceOrg.OrEmptyIfNull())
            {
                if (taxNum.taxCode == 1 && taxNum.taxNum.Length != 10)
                {
                    AddError("taxNum", taxNum.taxNum, "Длина номера налогоплательщика ЮЛ_43.7 не равна 10 символам");
                }
            }

            if (date.DateGreater(eventData?.eventDate))
            {
                AddError("date", date, "Дата начала действия договора обслуживания ЮЛ_43.8 позднее даты наступления события eventDate");
            }

            if (endDateSpecified)
            {
                if (endDate.DateGreater(eventData?.eventDate))
                {
                    AddError("endDate", endDate, "Дата окончания действия договора обслуживания ЮЛ_43.9 позднее даты наступления события eventDate");
                }
                if (endDate.DateLess(date))
                {
                    AddError("endDate", endDate, "Дата окончания действия договора обслуживания ЮЛ_43.9 раньше даты начала действия договора обслуживания ЮЛ_43.8");
                }
            }

            if (!issuerRegNum.DigitCheck(false, 13, 15))
            {
                AddError("issuerRegNum", issuerRegNum, "Длина регистрационного номера эмитента ЮЛ_43.11 не равна 13 или 15 цифрам");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_43_ServiceOrg_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
