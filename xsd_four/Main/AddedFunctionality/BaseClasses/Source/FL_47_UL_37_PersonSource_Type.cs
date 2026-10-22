using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_47_UL_37_PersonSource_Type : Block_Type
    {
        public FL_47_UL_37_PersonSource_Type()
        {
            blockName = refBlockNameType.FL_47_UL_37_PersonSource;
        }

        public List<INotificationOfAcceptanceError> Check(IDocument document)
        {
            InitCheck(null);

            if (sourceDocCode == refIdentifyDocType.Item999 && sourceDocName == null)
            {
                AddError("sourceDocName", sourceDocName, $"Отсутствует наименование иного документа ФЛ_47.6 (ЮЛ_37.6)");
            }

            if (sourceDocCode != refIdentifyDocType.Item999 && sourceDocName != null)
            {
                AddError("sourceDocName", sourceDocName, $"Наименование иного документа ФЛ_47.6 (ЮЛ_37.6) должно отсутствовать");
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
                AddError("sourceDocSeries", sourceDocSeries, "Отсутствует серия документа ФЛ_47.8 (ЮЛ_37.8)");
            }


            if (sourceDocCode == refIdentifyDocType.Item21)
            {
                if (sourceDocSeries.IsNotNull() && sourceDocSeries.Length != 4)
                {
                    AddError("sourceDocSeries", sourceDocSeries, "Длина серии документа ФЛ_47.8(ЮЛ_37.8) не равна 4 цифрам");
                }

                if (sourceDocNum.IsNotNull() && sourceDocNum.Length != 6)
                {
                    AddError("sourceDocNum", sourceDocNum, "Длина номера документа ФЛ_47.9 (ЮЛ_37.9) не равна 6 цифрам");
                }
                if (sourceDocDate.DateLess(birthDate, 13))
                {
                    AddError("sourceDocDate", sourceDocDate, "Дата выдачи документа ФЛ_47.10 (ЮЛ_37.10) раньше даты рождения ФЛ_47.4 (ЮЛ_37.4) плюс 13 лет");
                }

                if (sourceDocDate.DateLess("1997-10-01"))
                {
                    AddError("sourceDocDate", sourceDocDate, "Дата выдачи документа ФЛ_47.10 (ЮЛ_37.10) раньше 01.10.1997");
                }


            }
            if (sourceDocDate.DateLess(birthDate))
            {
                AddError("sourceDocDate", sourceDocDate, "Дата выдачи документа ФЛ_47.10 (ЮЛ_37.10) раньше даты рождения ФЛ_47.4 (ЮЛ_37.4)");
            }

            if (sourceDocDate.DateGreater(sourceCreditInfoDate))
            {
                AddError("sourceDocDate", sourceDocDate, "Дата выдачи документа ФЛ_47.10 (ЮЛ_37.10) позднее даты формирования кредитной информации ФЛ_47.14 (ЮЛ_37.14)");
            }

            //if (sourceRegNum.IsNotNull() && sourceRegNum.Length != 15)
            //{
            //    AddError("sourceRegNum", sourceRegNum, $"Длина регистрационного номера 47.13 (37.13) не равна 15 символов");
            //}

            return errorList;
        }
    }
}
