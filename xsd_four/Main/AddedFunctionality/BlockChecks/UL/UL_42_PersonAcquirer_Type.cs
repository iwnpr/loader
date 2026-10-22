using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_42_PersonAcquirer_Type : Block_Type
    {
        public UL_42_PersonAcquirer_Type()
        {
            blockName = refBlockNameType.UL_42_PersonAcquirer;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, ISource source, IUL_10_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (birthDate.DateLess(eventData.eventDate, -150))
            {
                AddError("birthDate", birthDate, "Дата рождения ЮЛ_42.4 раньше, чем дата наступления события eventDate минус 150 лет");
            }
            if (birthDate.DateGreaterEqual(eventData?.eventDate))
            {
                AddError("birthDate", birthDate, "Дата рождения ЮЛ_42.4 должна быть раньше даты наступления события eventDate");
            }

            foreach (var taxNum in TaxNum_group_UL_42_PersonAcquirer.OrEmptyIfNull())
            {
                if (taxNum.taxCode == 1 && taxNum.taxNum.Length != 12)
                {
                    AddError("taxNum", taxNum.taxNum, "Длина номера налогоплательщика ЮЛ_42.7 не равна 12 символов");
                }
            }

            if (!SnilsHelper.CheckSnils(socialNum))
            {
                AddError("socialNum", socialNum, "В СНИЛСе ЮЛ_42.8 неверный контрольный символ");
            }

            if (docCode == refIdentifyDocType.Item999 && docOtherName == null)
            {
                AddError("docOtherName", docOtherName, "Отсутствует наименование иного документа ЮЛ_42.10");
            }

            if (docCode != refIdentifyDocType.Item999 && docOtherName != null)
            {
                AddError("docOtherName", docOtherName, "Наименование иного документа ЮЛ_42.10 должно отсутствовать");
            }

            if (docCode.In(refIdentifyDocType.Item21,
                refIdentifyDocType.Item221,
                refIdentifyDocType.Item222,
                refIdentifyDocType.Item223,
                refIdentifyDocType.Item24,
                refIdentifyDocType.Item25,
                refIdentifyDocType.Item37) && docSeries == null)
            {
                AddError("docSeries", docSeries, "Отсутствует серия документа ЮЛ_42.11");
            }
            else if (docCode == refIdentifyDocType.Item21 && docSeries.Length != 4)
            {
                AddError("docSeries", docSeries, "Длина серии документа ЮЛ_42.11 не равна 4 цифрам");
            }

            if (docCode == refIdentifyDocType.Item21)
            {
                if (docNum.Length != 6)
                {
                    AddError("docNum", docNum, "Длина номера документа ЮЛ_42.12 не равна 6 цифрам");
                }
                if (issueDate.DateLess(birthDate, 13))
                {
                    AddError("issueDate", issueDate, "Дата выдачи документа ЮЛ_42.13 раньше даты рождения ЮЛ_42.4 плюс 13 лет");
                }
                if (issueDate.DateLess("1997-10-01"))
                {
                    AddError("issueDate", issueDate, "Дата выдачи документа ЮЛ_42.13 раньше 01.10.1997");
                }
            }

            if (issueDate.DateLess(birthDate))
            {
                AddError("issueDate", issueDate, "Дата выдачи документа ЮЛ_42.13 раньше даты рождения ЮЛ_42.4");
            }
            if (issueDate.DateGreater(source.sourceCreditInfoDate))
            {
                AddError("issueDate", issueDate, "Дата выдачи документа ЮЛ_42.13 позднее даты формирования кредитной информации");
            }

            if (date.DateGreater(eventData?.eventDate))
            {
                AddError("date", date, "Дата приобретения прав ЮЛ_42.16 позднее даты наступления события eventDate");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                AddError("date", date, "Дата приобретения прав ЮЛ_42.16 раньше даты совершения сделки ЮЛ_10.4");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_42_PersonAcquirer_Type block, EventData eventData, ISource source, IUL_10_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, source, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}
