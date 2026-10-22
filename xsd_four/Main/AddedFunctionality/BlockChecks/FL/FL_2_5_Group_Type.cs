using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_2_5_Group_Type : Block_Type
    {
        public FL_2_5_Group_Type()
        {
            blockName = refBlockNameType.FL_2_5_Group;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, ISource source, FL_3_Birth_Type? birthBlock)
        {
            InitCheck(eventData);

            if (FL_2_PrevName.prevNameFlag == true)
            {
                if (FL_2_PrevName.date.DateLess(birthBlock?.birthDate))
                {
                    AddError("date", FL_2_PrevName.date, "Дата выдачи документа с измененным именем ФЛ_2.5 раньше даты рождения ФЛ_3.1");
                }
                if (FL_2_PrevName.date.DateGreater(source.sourceCreditInfoDate))
                {
                    AddError("date", FL_2_PrevName.date, "Дата выдачи документа с измененным именем ФЛ_2.5  позднее даты формирования кредитной информации");
                }
            }

            foreach (var prevDoc in FL_5_PrevDoc.OrEmptyIfNull())
            {
                if (prevDoc.prevDocFact == true)
                {
                    if (!CountryCodeDictionary.Contains(prevDoc.countryCode))
                    {
                        AddError("countryCode", prevDoc.countryCode, "Значение кода страны гражданства по ОКСМ ФЛ_5.2 не соответствует ОКСМ или «999»");
                    }

                    if (prevDoc.countryCode != "999" && prevDoc.countryOther != null)
                    {
                        AddError("countryOther", prevDoc.countryOther, "Наименование иной страны ФЛ_5.3 должно отсутствовать");
                    }

                    if (prevDoc.docCode.In(refIdentifyDocType.Item21,
                        refIdentifyDocType.Item221,
                        refIdentifyDocType.Item222,
                        refIdentifyDocType.Item223,
                        refIdentifyDocType.Item24,
                        refIdentifyDocType.Item25,
                        refIdentifyDocType.Item37)
                       && prevDoc.docSeries == null)
                    {
                        AddError("docSeries", prevDoc.docSeries, "Отсутствует серия документа ФЛ_5.6");
                    }
                    if (prevDoc.docCode == refIdentifyDocType.Item21)
                    {
                        if (prevDoc.docSeries.IsNotNull() && prevDoc.docSeries.Length != 4)
                        {
                            AddError("docSeries", prevDoc.docSeries, "Длина серии документа ФЛ_5.6 не равна 4 цифрам");
                        }
                        if (prevDoc.docNum.IsNotNull() && prevDoc.docNum.Length != 6)
                        {
                            AddError("docNum", prevDoc.docNum, "Длина номера документа ФЛ_5.7 не равна 6 цифрам");
                        }
                        if (prevDoc.issueDate.DateLess(birthBlock?.birthDate, 13))
                        {
                            AddError("issueDate", prevDoc.issueDate, "Дата выдачи документа ФЛ_5.8 раньше даты рождения ФЛ_3.1 плюс 13 лет");
                        }
                        if (prevDoc.issueDate.DateLess("1997-10-01"))
                        {
                            AddError("issueDate", prevDoc.issueDate, "Дата выдачи документа ФЛ_5.8 раньше 01.10.1997");
                        }
                    }

                    if (prevDoc.docCode == refIdentifyDocType.Item999 && prevDoc.docOtherName == null)
                    {
                        AddError("docOtherName", prevDoc.docOtherName, "Отсутствует наименование иного документа ФЛ_5.5");
                    }
                    if (prevDoc.docCode != refIdentifyDocType.Item999 && prevDoc.docOtherName != null)
                    {
                        AddError("docOtherName", prevDoc.docOtherName, "Наименование иного документа ФЛ_5.5 должно отсутствовать");
                    }

                    if (prevDoc.issueDate.DateLess(birthBlock?.birthDate))
                    {
                        AddError("issueDate", prevDoc.issueDate, "Дата выдачи документа ФЛ_5.8 раньше даты рождения ФЛ_3.1");
                    }
                    if (prevDoc.issueDate.DateGreater(source.sourceCreditInfoDate))
                    {
                        AddError("issueDate", prevDoc.issueDate, "Дата выдачи документа ФЛ_5.8 позднее даты формирования кредитной информации");
                    }

                    if (prevDoc.endDate.DateLess(prevDoc.issueDate))
                    {
                        AddError("issueDate", prevDoc.issueDate, "Дата окончания срока действия документа ФЛ_5.11 раньше даты выдачи документа ФЛ_5.8");
                    }
                }
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_2_5_Group_Type block, EventData eventData, ISource source, FL_3_Birth_Type? birthBlock)
        {
            return (block?.Check(eventData, source, birthBlock)).OrEmptyIfNull().ToList();
        }
    }
}
