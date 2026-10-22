using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_8_AddrReg_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_8_AddrReg;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, ISource source, FL_3_Birth_Type? birthBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (code == "3")
            {
                if (postCode != null)
                {
                    errorData.AddError("postCode", postCode, "Почтовый индекс ФЛ_8.2 должен отсутствовать");
                }
                if (countryCode != null)
                {
                    errorData.AddError("countryCode", countryCode, "Код страны по ОКСМ ФЛ_8.3 должен отсутствовать");
                }
                if (countryOther != null)
                {
                    errorData.AddError("countryOther", countryOther, "Наименование иной страны ФЛ_8.4 должно отсутствовать");
                }
                if (regStateNum != null)
                {
                    errorData.AddError("regStateNum", regStateNum, "Номер адреса в ГАР ФЛ_8.5 должен отсутствовать");
                }
                if (locationCode != null)
                {
                    errorData.AddError("locationCode", locationCode, "Код населенного пункта по ОКАТО ФЛ_8.6 должен отсутствовать");
                }
                if (street != null)
                {
                    errorData.AddError("street", street, "Улица ФЛ_8.8 должна отсутствовать");
                }
                if (house != null)
                {
                    errorData.AddError("house", house, "Дом ФЛ_8.9 должен отсутствовать");
                }
                if (estate != null)
                {
                    errorData.AddError("estate", estate, "Владение ФЛ_8.10 должно отсутствовать");
                }
                if (block != null)
                {
                    errorData.AddError("block", block, "Корпус ФЛ_8.11 должен отсутствовать");
                }
                if (build != null)
                {
                    errorData.AddError("build", build, "Строение ФЛ_8.12 должно отсутствовать");
                }
                if (apart != null)
                {
                    errorData.AddError("apart", apart, "Квартира ФЛ_8.13 должна отсутствовать");
                }
                if (dateSpecified)
                {
                    errorData.AddError("date", date, "Дата регистрации ФЛ_8.14 должна отсутствовать");
                }
                if (dept != null)
                {
                    errorData.AddError("dept", dept, "Наименование регистрирующего органа ФЛ_8.15 должно отсутствовать");
                }
                if (deptCode != null)
                {
                    errorData.AddError("deptCode", deptCode, "Код подразделения, осуществившего регистрацию ФЛ_8.16 должен отсутствовать");
                }
            }

            if (postCode != null && countryCode == "643" && postCode.Length != 6)
            {
                errorData.AddError("postCode", postCode, "Длина почтового индекса ФЛ_8.2 не равна 6 цифрам");
            }
            if (code != "3" && countryCode == null)
            {
                errorData.AddError("countryCode", countryCode, "Отсутствует код страны по ОКСМ ФЛ_8.3");
            }
            if (!CountryCodeDictionary.Contains(countryCode))
            {
                errorData.AddError("countryCode", countryCode, "Значение кода страны по ОКСМ ФЛ_8.3 не соответствует ОКСМ или «999»");
            }
            if ((code == "3" || countryCode != "999") && countryOther != null)
            {
                errorData.AddError("countryOther", countryOther, "Наименование иной страны ФЛ_8.4 должно отсутствовать");
            }

            if (code != "3" && countryCode == "643" && locationCode == null)
            {
                errorData.AddError("locationCode", locationCode, "Отсутствует код населенного пункта по ОКАТО ФЛ_8.6");
            }
            if ((code == "3" || (code != "3" && locationCode != "99999999999")) && locationOther != null)
            {
                errorData.AddError("locationOther", locationOther, "Иной населенный пункт ФЛ_8.7 должен отсутствовать");
            }
            if (code != "3" && locationCode == "99999999999" && locationOther == null)
            {
                errorData.AddError("locationOther", locationOther, "Отсутствует иной населенный пункт ФЛ_8.7");
            }

            if (date.DateGreater(source.sourceCreditInfoDate))
            {
                errorData.AddError("date", date, "Дата регистрации ФЛ_8.14 позднее даты формирования кредитной информации");
            }
            if (date.DateLess(birthBlock?.birthDate))
            {
                errorData.AddError("date", date, "Дата регистрации ФЛ_8.14 раньше даты рождения ФЛ_3.1");
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_8_AddrReg_Type block, IEventData eventData, ISource source, FL_3_Birth_Type? birthBlock = null)
        {
            return [.. (block?.Check(eventData, source, birthBlock)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_8_AddrReg_Type> blocks, IEventData eventData, ISource source, FL_3_Birth_Type? birthBlock = null)
        {
            List<INotificationOfAcceptanceError> errorList = [];

            foreach (var block in blocks.OrEmptyIfNull())
            {
                errorList.AddRange(block.Check(eventData, source, birthBlock));
            }
            return errorList;
        }

        public static List<INotificationOfAcceptanceError> Validate(this FL_8_AddrReg_Type blocks, IEventData eventData, ISource source, ISubjectTitleData title)
        {
            return Validate(blocks, eventData, source, title is SubjectTitleDataFL titleFL ? titleFL.FL_3_Birth : null);
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_8_AddrReg_Type> blocks, IEventData eventData, ISource source, ISubjectTitleData title)
        {
            return Validate(blocks, eventData, source, title is SubjectTitleDataFL titleFL ? titleFL.FL_3_Birth : null);
        }
    }
}
