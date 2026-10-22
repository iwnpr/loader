using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_1_4_Group_Type : Block_Type
    {
        public FL_1_4_Group_Type()
        {
            blockName = refBlockNameType.FL_1_4_Group;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, ISource source, FL_2_5_Group_Type[]? blocks_2_5 = null, FL_3_Birth_Type? birthBlock = null)
        {
            InitCheck(eventData);

            if (FL_1_Name.firstName == "-" && FL_1_Name.lastName == "-")
            {
                AddError("firstName", fL_1_NameField.firstName, "Фамилия ФЛ_1.1 и Имя ФЛ_1.2 одновременно содержат знак «-»");
                AddError("lastName", fL_1_NameField.lastName, "Фамилия ФЛ_1.1 и Имя ФЛ_1.2 одновременно содержат знак «-»");
            }

            foreach (var doc in FL_4_Doc.OrEmptyIfNull())
            {
                if (!CountryCodeDictionary.Contains(doc.countryCode))
                {
                    AddError("countryCode", doc.countryCode, "Значение кода страны гражданства по ОКСМ ФЛ_4.1 не соответствует ОКСМ или «999»");
                }

                if (doc.countryCode != "999" && doc.countryOther != null)
                {
                    AddError("countryOther", doc.countryOther, "Наименование иной страны ФЛ_4.2 должно отсутствовать");
                }

                if (doc.issueDate.DateLess(birthBlock?.birthDate))
                {
                    AddError("issueDate", doc.issueDate, "Дата выдачи документа ФЛ_4.7 раньше даты рождения ФЛ_3.1");
                }
                if (doc.issueDate.DateGreater(source.sourceCreditInfoDate))
                {
                    AddError("issueDate", doc.issueDate, "Дата выдачи документа ФЛ_4.7 позднее даты формирования кредитной информации");
                }
                if (doc.docCode == refIdentifyDocType.Item21)
                {
                    foreach (var block_2_5 in blocks_2_5.OrEmptyIfNull())
                    {
                        foreach (var prevDoc in block_2_5.FL_5_PrevDoc.OrEmptyIfNull())
                        {
                            if (prevDoc.docCode == refIdentifyDocType.Item21 && doc.issueDate.DateLess(prevDoc.issueDate))
                            {
                                AddError("issueDate", doc.issueDate, "Дата выдачи документа ФЛ_4.7 раньше даты выдачи предыдущего документа ФЛ_5.8");
                            }
                        }
                    }
                    if (doc.docSeries != null && !doc.docSeries.DigitCheck(true, 4))
                    {
                        AddError("docSeries", doc.docSeries, "Длина серии документа ФЛ_4.5 не равна 4 цифрам");
                    }

                    if (doc.docNum.Length != 6)
                    {
                        AddError("docNum", doc.docNum, "Длина номера документа ФЛ_4.6 не равна 6 цифрам");
                    }


                    if (doc.issueDate.DateLess(birthBlock?.birthDate, 13))
                    {
                        AddError("issueDate", doc.issueDate, "Дата выдачи документа ФЛ_4.7 раньше даты рождения ФЛ_3.1 плюс 13 лет");
                    }
                    if (doc.issueDate.DateLess("1997-10-01"))
                    {
                        AddError("issueDate", doc.issueDate, "Дата выдачи документа ФЛ_4.7 раньше 01.10.1997");
                    }
                    if (doc.foreignerCode == 1)
                    {
                        AddError("endDate", doc.endDate, "Признак иностранного гражданина ФЛ_4.11 равен «1»");
                    }
                }
                if (doc.docCode == refIdentifyDocType.Item999 && doc.docOtherName == null)
                {
                    AddError("docOtherName", doc.docOtherName, "Отсутствует наименование иного документа ФЛ_4.4");
                }
                if (doc.docCode != refIdentifyDocType.Item999 && doc.docOtherName != null)
                {
                    AddError("docOtherName", doc.docOtherName, "Наименование иного документа ФЛ_4.4 должно отсутствовать");
                }

                if (doc.docCode.In(
                            refIdentifyDocType.Item21,
                            refIdentifyDocType.Item221,
                            refIdentifyDocType.Item222,
                            refIdentifyDocType.Item223,
                            refIdentifyDocType.Item24,
                            refIdentifyDocType.Item25,
                            refIdentifyDocType.Item37) && doc.docSeries == null)
                {
                    AddError("docSeries", doc.docSeries, "Отсутствует серия документа ФЛ_4.5");
                }

                if (doc.endDate.DateLess(doc.issueDate))
                {
                    AddError("endDate", doc.endDate, "Дата окончания срока действия документа ФЛ_4.10 раньше даты выдачи документа ФЛ_4.7");
                }
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_1_4_Group_Type block, EventData eventData, ISource source, FL_2_5_Group_Type[]? blocks_2_5 = null, FL_3_Birth_Type? birthBlock = null)
        {
            return (block?.Check(eventData, source, blocks_2_5, birthBlock)).OrEmptyIfNull().ToList();
        }
    }
}
