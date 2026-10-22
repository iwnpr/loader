using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_52_PersonAcquirer_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_52_PersonAcquirer;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, ISource source, I_FL_17_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (birthDate.DateLess(eventData.eventDateTimeLocal, -150))
            {
                errorData.AddError("birthDate", birthDate, "Дата рождения ФЛ_52.4 раньше, чем даты и времемя наступления события eventDateTime минус 150 лет");
            }
            if (birthDate.DateGreaterEqual(eventData.eventDateTimeLocal))
            {
                errorData.AddError("birthDate", birthDate, "Дата рождения ФЛ_52.4 должна быть раньше даты и времени наступления события eventDateTime");
            }

            foreach (var taxNum in TaxNum_group_FL_52_PersonAcquirer.OrEmptyIfNull())
            {
                if (taxNum.taxCode == refTaxpayerType.Item1 && !taxNum.taxNum.DigitCheck(12))
                {
                    errorData.AddError("taxNum", taxNum.taxNum, "Длина номера налогоплательщика ФЛ_52.7 не равна 12 символов");
                }
            }

            if (!SnilsHelper.CheckSnils(socialNum))
            {
                errorData.AddError("socialNum", socialNum, "В СНИЛСе ФЛ_52.8 неверный контрольный символ");
            }

            if (docCode == refIdentifyDocType.Item999 && docOtherName == null)
            {
                errorData.AddError("docOtherName", docOtherName, "Отсутствует наименование иного документа ФЛ_52.10");
            }

            if (docCode != refIdentifyDocType.Item999 && docOtherName != null)
            {
                errorData.AddError("docOtherName", docOtherName, "Наименование иного документа ФЛ_52.10 должно отсутствовать");
            }

            if (docCode.In(refIdentifyDocType.Item21,
                refIdentifyDocType.Item221,
                refIdentifyDocType.Item222,
                refIdentifyDocType.Item223,
                refIdentifyDocType.Item24,
                refIdentifyDocType.Item25,
                refIdentifyDocType.Item37) && docSeries == null)
            {
                errorData.AddError("docSeries", docSeries, "Отсутствует серия документа ФЛ_52.11");
            }
            else if (docCode == refIdentifyDocType.Item21 && !docSeries.DigitCheck(4))
            {
                errorData.AddError("docSeries", docSeries, "Длина серии документа ФЛ_52.11 не равна 4 цифрам");
            }

            if (docCode == refIdentifyDocType.Item21)
            {
                if (!docNum.DigitCheck(6))
                {
                    errorData.AddError("docNum", docNum, "Длина номера документа ФЛ_52.12 не равна 6 цифрам");
                }
                if (issueDate.DateLess(birthDate, 13))
                {
                    errorData.AddError("issueDate", issueDate, "Дата выдачи документа ФЛ_52.13 раньше даты рождения ФЛ_52.4 плюс 13 лет");
                }
                if (issueDate.DateLess("1997-10-01"))
                {
                    errorData.AddError("issueDate", issueDate, "Дата выдачи документа ФЛ_52.13 раньше 01.10.1997");
                }
            }

            if (issueDate.DateLess(birthDate))
            {
                errorData.AddError("issueDate", issueDate, "Дата выдачи документа ФЛ_52.13 раньше даты рождения ФЛ_52.4");
            }
            if (issueDate.DateGreater(source.sourceCreditInfoDate))
            {
                errorData.AddError("issueDate", issueDate, "Дата выдачи документа ФЛ_52.13 позднее даты формирования кредитной информации");
            }

            if (date.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("date", date, "Дата приобретения прав ФЛ_52.16 позднее даты и времени наступления события eventDateTime");
            }
            if (date.DateLess(dealUidBlock?.openDate))
            {
                errorData.AddError("date", date, "Дата приобретения прав ФЛ_52.16 раньше даты совершения сделки ФЛ_17.4");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_52_PersonAcquirer_Type block, IEventData eventData, ISource source, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, source, dealUidBlock)).OrEmptyIfNull()];
        }
    }
}
