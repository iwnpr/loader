using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_47_UL_37_PersonSource_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_47_UL_37_PersonSource;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IDocument document)
        {
            ErrorData errorData = new(blockName);

            if (sourceDocCode == refIdentifyDocType.Item999 && sourceDocName == null)
            {
                errorData.AddError("sourceDocName", sourceDocName, $"Отсутствует наименование иного документа ФЛ_47.6 (ЮЛ_37.6)");
            }

            if (sourceDocCode != refIdentifyDocType.Item999 && sourceDocName != null)
            {
                errorData.AddError("sourceDocName", sourceDocName, $"Наименование иного документа ФЛ_47.6 (ЮЛ_37.6) должно отсутствовать");
            }

            if (sourceDocCode.In(refIdentifyDocType.Item21,
                                refIdentifyDocType.Item221,
                                refIdentifyDocType.Item222,
                                refIdentifyDocType.Item223,
                                refIdentifyDocType.Item24,
                                refIdentifyDocType.Item25,
                                refIdentifyDocType.Item37)
                && sourceDocSeries == null)
            {
                errorData.AddError("sourceDocSeries", sourceDocSeries, "Отсутствует серия документа ФЛ_47.8 (ЮЛ_37.8)");
            }


            if (sourceDocCode == refIdentifyDocType.Item21)
            {
                if (sourceDocSeries.IsNotNull() && sourceDocSeries.Length != 4)
                {
                    errorData.AddError("sourceDocSeries", sourceDocSeries, "Длина серии документа ФЛ_47.8(ЮЛ_37.8) не равна 4 цифрам");
                }

                if (sourceDocNum.IsNotNull() && sourceDocNum.Length != 6)
                {
                    errorData.AddError("sourceDocNum", sourceDocNum, "Длина номера документа ФЛ_47.9 (ЮЛ_37.9) не равна 6 цифрам");
                }
                if (sourceDocDate.DateLess(birthDate, 13))
                {
                    errorData.AddError("sourceDocDate", sourceDocDate, "Дата выдачи документа ФЛ_47.10 (ЮЛ_37.10) раньше даты рождения ФЛ_47.4 (ЮЛ_37.4) плюс 13 лет");
                }

                if (sourceDocDate.DateLess("1997-10-01"))
                {
                    errorData.AddError("sourceDocDate", sourceDocDate, "Дата выдачи документа ФЛ_47.10 (ЮЛ_37.10) раньше 01.10.1997");
                }


            }
            if (sourceDocDate.DateLess(birthDate))
            {
                errorData.AddError("sourceDocDate", sourceDocDate, "Дата выдачи документа ФЛ_47.10 (ЮЛ_37.10) раньше даты рождения ФЛ_47.4 (ЮЛ_37.4)");
            }

            if (sourceDocDate.DateGreater(sourceCreditInfoDate))
            {
                errorData.AddError("sourceDocDate", sourceDocDate, "Дата выдачи документа ФЛ_47.10 (ЮЛ_37.10) позднее даты формирования кредитной информации ФЛ_47.14 (ЮЛ_37.14)");
            }

            if (sourceCreditInfoDate != document.dateTimeDocLocal.Date)
            {
                errorData.AddError("sourceCreditInfoDate", sourceCreditInfoDate, "Дата формирования кредитной информации ФЛ_47.14 (ЮЛ_37.14) не равна исходящей дате и времени регистрации документа, содержащего кредитную информацию dateTimeDoc");
            }

            return errorData.ErrorList;
        }
    }
}
