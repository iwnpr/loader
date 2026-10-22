using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_8_AddrReg_Type : Block_Type
    {
        public FL_8_AddrReg_Type()
        {
            blockName = refBlockNameType.FL_8_AddrReg;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, ISource source, FL_3_Birth_Type? birthBlock = null)
        {
            InitCheck(eventData);

            if (code == "3")
            {
                if (postCode != null)
                {
                    AddError("postCode", postCode, "Почтовый индекс ФЛ_8.2 должен отсутствовать");
                }
                if (countryCode != null)
                {
                    AddError("countryCode", countryCode, "Код страны по ОКСМ ФЛ_8.3 должен отсутствовать");
                }
                if (countryOther != null)
                {
                    AddError("countryOther", countryOther, "Наименование иной страны ФЛ_8.4 должно отсутствовать");
                }
                if (regStateNum != null)
                {
                    AddError("regStateNum", regStateNum, "Номер адреса в ГАР ФЛ_8.5 должен отсутствовать");
                }
                if (street != null)
                {
                    AddError("street", street, "Улица ФЛ_8.8 должна отсутствовать");
                }
                if (house != null)
                {
                    AddError("house", house, "Дом ФЛ_8.9 должен отсутствовать");
                }
                if (estate != null)
                {
                    AddError("estate", estate, "Владение ФЛ_8.10 должно отсутствовать");
                }
                if (block != null)
                {
                    AddError("block", block, "Корпус ФЛ_8.11 должен отсутствовать");
                }
                if (build != null)
                {
                    AddError("build", build, "Строение ФЛ_8.12 должно отсутствовать");
                }
                if (apart != null)
                {
                    AddError("apart", apart, "Квартира ФЛ_8.13 должна отсутствовать");
                }
                if (dateSpecified)
                {
                    AddError("date", date, "Дата регистрации ФЛ_8.14 должна отсутствовать");
                }
                if (dept != null)
                {
                    AddError("dept", dept, "Наименование регистрирующего органа ФЛ_8.15 должно отсутствовать");
                }
                if (deptCode != null)
                {
                    AddError("deptCode", deptCode, "Код подразделения, осуществившего регистрацию ФЛ_8.16 должен отсутствовать");
                }
            }

            if (postCode != null && countryCode == "643" && postCode.Length != 6)
            {
                AddError("postCode", postCode, "Длина почтового индекса ФЛ_8.2 не равна 6 цифрам");
            }
            if (!CountryCodeDictionary.Contains(countryCode))
            {
                AddError("countryCode", countryCode, "Значение кода страны по ОКСМ ФЛ_8.3 не соответствует ОКСМ или «999»");
            }
            if ((code == "3" || countryCode != "643") && locationCode != null)
            {
                AddError("locationCode", locationCode, "Код населенного пункта по ОКАТО ФЛ_8.6 должен отсутствовать");
            }
            if (code != "3" && countryCode == "643" && locationCode == null)
            {
                AddError("locationCode", locationCode, "Отсутствует код населенного пункта по ОКАТО ФЛ_8.6");
            }
            if ((code == "3" || (code != "3" && locationCode != "99999999999")) && locationOther != null)
            {
                AddError("locationOther", locationOther, "Иной населенный пункт ФЛ_8.7 должен отсутствовать");
            }
            if (code != "3" && locationCode == "99999999999" && locationOther == null)
            {
                AddError("locationOther", locationOther, "Отсутствует иной населенный пункт ФЛ_8.7");
            }

            if (date.DateGreater(source.sourceCreditInfoDate))
            {
                AddError("date", date, "Дата регистрации ФЛ_8.14 позднее даты формирования кредитной информации");
            }
            if (date.DateLess(birthBlock?.birthDate))
            {
                AddError("date", date, "Дата регистрации ФЛ_8.14 раньше даты рождения ФЛ_3.1");
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_8_AddrReg_Type block, EventData eventData, ISource source, FL_3_Birth_Type? birthBlock = null)
        {
            return (block?.Check(eventData, source, birthBlock)).OrEmptyIfNull().ToList();
        }
    }
}
