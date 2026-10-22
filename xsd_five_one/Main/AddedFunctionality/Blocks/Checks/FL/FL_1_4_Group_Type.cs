using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_1_4_Group_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_1_4_Group;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData? eventData, ISource source, FL_2_5_Group_Type[]? blocks_2_5, FL_3_Birth_Type? birthBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (FL_1_Name.firstName == "-" && FL_1_Name.lastName == "-")
            {
                errorData.AddError("lastName", fL_1_NameField.lastName, "Фамилия ФЛ_1.1 и Имя ФЛ_1.2 одновременно содержат знак «-»");
                errorData.AddError("firstName", fL_1_NameField.firstName, "Фамилия ФЛ_1.1 и Имя ФЛ_1.2 одновременно содержат знак «-»");
            }

            foreach (var doc in FL_4_Doc.OrEmptyIfNull())
            {
                if (!CountryCodeDictionary.Contains(doc.countryCode))
                {
                    errorData.AddError("countryCode", doc.countryCode, "Значение кода страны гражданства по ОКСМ ФЛ_4.1 не соответствует ОКСМ или «999»");
                }

                if (doc.docCode.In(
                         refIdentifyDocType.Item21,
                         refIdentifyDocType.Item221,
                         refIdentifyDocType.Item222,
                         refIdentifyDocType.Item223,
                         refIdentifyDocType.Item26,
                         refIdentifyDocType.Item27,
                         refIdentifyDocType.Item28,
                         refIdentifyDocType.Item38) && doc.countryCode != "643")
                {
                    errorData.AddError("countryCode", doc.countryCode, "Код страны гражданства по ОКСМ ФЛ_4.1 не равен «643»");
                }

                if (doc.docCode.In(
                        refIdentifyDocType.Item31,
                        refIdentifyDocType.Item32,
                        refIdentifyDocType.Item35) && doc.countryCode == "643")
                {
                    errorData.AddError("countryCode", doc.countryCode, "Код страны гражданства по ОКСМ ФЛ_4.1 равен «643»");
                }

                if (doc.countryCode != "999" && doc.countryOther != null)
                {
                    errorData.AddError("countryOther", doc.countryOther, "Наименование иной страны ФЛ_4.2 должно отсутствовать");
                }

                if (doc.docCode == refIdentifyDocType.Item999 && doc.docOtherName == null)
                {
                    errorData.AddError("docOtherName", doc.docOtherName, "Отсутствует наименование иного документа ФЛ_4.4");
                }
                if (doc.docCode != refIdentifyDocType.Item999 && doc.docOtherName != null)
                {
                    errorData.AddError("docOtherName", doc.docOtherName, "Наименование иного документа ФЛ_4.4 должно отсутствовать");
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
                    errorData.AddError("docSeries", doc.docSeries, "Отсутствует серия документа ФЛ_4.5");
                }

                if (doc.issueDate.DateLess(birthBlock?.birthDate))
                {
                    errorData.AddError("issueDate", doc.issueDate, "Дата выдачи документа ФЛ_4.7 раньше даты рождения ФЛ_3.1");
                }
                if (doc.issueDate.DateGreater(source.sourceCreditInfoDate))
                {
                    errorData.AddError("issueDate", doc.issueDate, "Дата выдачи документа ФЛ_4.7 позднее даты формирования кредитной информации");
                }
                if (doc.docCode == refIdentifyDocType.Item21)
                {
                    foreach (var block_2_5 in blocks_2_5.OrEmptyIfNull())
                    {
                        foreach (var prevDoc in block_2_5.FL_5_PrevDoc.OrEmptyIfNull())
                        {
                            if (prevDoc.docCode == refIdentifyDocType.Item21 && doc.issueDate.DateLess(prevDoc.issueDate))
                            {
                                errorData.AddError("issueDate", doc.issueDate, "Дата выдачи документа ФЛ_4.7 раньше даты выдачи предыдущего документа ФЛ_5.8");
                            }
                        }
                    }
                    if (doc.docSeries != null && !doc.docSeries.DigitCheck( 4))
                    {
                        errorData.AddError("docSeries", doc.docSeries, "Длина серии документа ФЛ_4.5 не равна 4 цифрам");
                    }

                    if (doc.docNum != null && !doc.docNum.DigitCheck( 6))
                    {
                        errorData.AddError("docNum", doc.docNum, "Длина номера документа ФЛ_4.6 не равна 6 цифрам");
                    }

                    if (doc.issueDate.DateLess(birthBlock?.birthDate, 13))
                    {
                        errorData.AddError("issueDate", doc.issueDate, "Дата выдачи документа ФЛ_4.7 раньше даты рождения ФЛ_3.1 плюс 13 лет");
                    }
                    if (doc.issueDate.DateLess("1997-10-01"))
                    {
                        errorData.AddError("issueDate", doc.issueDate, "Дата выдачи документа ФЛ_4.7 раньше 01.10.1997");
                    }
                    if (doc.foreignerCode == refForeignerCodeType.Item1)
                    {
                        errorData.AddError("endDate", doc.endDate, "Признак иностранного гражданина ФЛ_4.11 равен «1»");
                    }
                }

                if (doc.endDate.DateLess(doc.issueDate))
                {
                    errorData.AddError("endDate", doc.endDate, "Дата окончания срока действия документа ФЛ_4.10 раньше даты выдачи документа ФЛ_4.7");
                }
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_1_4_Group_Type? block, IEventData? eventData, ISource source, FL_2_5_Group_Type[]? blocks_2_5, FL_3_Birth_Type? birthBlock)
        {
            return [.. (block?.Check(eventData, source, blocks_2_5, birthBlock)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this FL_1_4_Group_Type? block, IEventData? eventData, ISource source, FL_2_5_Group_Type[]? blocks_2_5, ISubjectTitleData? title)
        {
            return Validate(block, eventData, source, blocks_2_5, title is SubjectTitleDataFL titleFL ? titleFL.FL_3_Birth : null);
        }
    }
}
