using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_13_Bankruptcy_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_13_Bankruptcy;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, ISource source, FL_3_Birth_Type? birthBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (bankruptcyFact == true)
            {
                if (date.DateGreater(source.sourceCreditInfoDate))
                {
                    errorData.AddError("date", date, "Дата публикации в ЕФРСБ сведений о стадии рассмотрения дела ФЛ_13.3 позднее даты формирования кредитной информации");
                }
                if (date.DateLess(birthBlock?.birthDate))
                {
                    errorData.AddError("date", date, "Дата публикации в ЕФРСБ сведений о стадии рассмотрения дела ФЛ_13.3 раньше даты рождения ФЛ_3.1");
                }

                if (stage.In("9", "10", "11", "12", "13", "14"))
                {
                    errorData.AddError("stage", stage, "Код стадии рассмотрения дела ФЛ_13.2 не должен быть равен значениям от 9 до 14");
                }

                if (illegalActExist == true)
                {
                    foreach (var illegalActDateValue in illegalActDate.OrEmptyIfNull())
                    {
                        if (illegalActDateValue.DateGreater(source.sourceCreditInfoDate))
                        {
                            errorData.AddError("illegalActDate", illegalActDateValue, "Дата публикации в ЕФРСБ сообщения о неправомерных действиях ФЛ_13.6 позднее даты формирования кредитной информации");
                        }
                        if (illegalActDateValue.DateLess(birthBlock?.birthDate))
                        {
                            errorData.AddError("illegalActDate", illegalActDateValue, "Дата публикации в ЕФРСБ сообщения о неправомерных действиях ФЛ_13.6 раньше даты рождения ФЛ_3.1");
                        }
                    }
                }

                if (fakeActExist == true)
                {
                    foreach (var fakeActDateValue in fakeActDate.OrEmptyIfNull())
                    {
                        if (fakeActDateValue.DateGreater(source.sourceCreditInfoDate))
                        {
                            errorData.AddError("illegalActDate", fakeActDateValue, "Дата публикации в ЕФРСБ сообщения о признаках преднамеренного или фиктивного банкротства ФЛ_13.8 позднее даты формирования кредитной информации");
                        }
                        if (fakeActDateValue.DateLess(birthBlock?.birthDate))
                        {
                            errorData.AddError("illegalActDate", fakeActDateValue, "Дата публикации в ЕФРСБ сообщения о признаках преднамеренного или фиктивного банкротства ФЛ_13.8 раньше даты рождения ФЛ_3.1");
                        }
                    }
                }
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_13_Bankruptcy_Type block, IEventData eventData, ISource source, FL_3_Birth_Type? birthBlock = null)
        {
            return [.. (block?.Check(eventData, source, birthBlock)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_13_Bankruptcy_Type> blocks, IEventData eventData, ISource source, FL_3_Birth_Type? birthBlock = null)
        {
            List<INotificationOfAcceptanceError> errorList = [];
            foreach (var block in blocks.OrEmptyIfNull())
            {
                errorList.AddRange(block.Check(eventData, source, birthBlock));
            }
            return errorList;
        }

        public static List<INotificationOfAcceptanceError> Validate(this FL_13_Bankruptcy_Type block, IEventData eventData, ISource source, ISubjectTitleData title)
        {
            return Validate(block, eventData, source, title is SubjectTitleDataFL titleFL ? titleFL.FL_3_Birth : null);
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_13_Bankruptcy_Type> blocks, IEventData eventData, ISource source, ISubjectTitleData title)
        {
            return Validate(blocks, eventData, source, title is SubjectTitleDataFL titleFL ? titleFL.FL_3_Birth : null);
        }
    }
}
