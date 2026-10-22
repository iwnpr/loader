using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_6_Bankruptcy_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_6_Bankruptcy;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, ISource source)
        {
            ErrorData errorData = new(blockName, eventData);
            if (bankruptcyFact == true)
            {
                if (date.DateGreater(source.sourceCreditInfoDate))
                {
                    errorData.AddError("date", date, "Дата публикации в ЕФРСБ сведений о стадии рассмотрения дела ЮЛ_6.3 позднее даты формирования кредитной информации");
                }

                if (stage.In("6", "7", "8"))
                {
                    errorData.AddError("stage", stage, "Код стадии рассмотрения дела ЮЛ_6.2 не должен быть равен значениям от 6 до 8");
                }

                if (illegalActExist == true)
                {
                    foreach (var illegalActDateValue in illegalActDate.OrEmptyIfNull())
                    {
                        if (illegalActDateValue.DateGreater(source.sourceCreditInfoDate))
                        {
                            errorData.AddError("illegalActDate", illegalActDateValue, "Дата публикации в ЕФРСБ сообщения о неправомерных действиях ЮЛ_6.6 позднее даты формирования кредитной информации");
                        }
                    }
                }

                if (fakeActExist == true)
                {
                    foreach (var fakeActDateValue in fakeActDate.OrEmptyIfNull())
                    {
                        if (fakeActDateValue.DateGreater(source.sourceCreditInfoDate))
                        {
                            errorData.AddError("illegalActDate", fakeActDateValue, "Дата публикации в ЕФРСБ сообщения о признаках преднамеренного или фиктивного банкротства ЮЛ_6.8 позднее даты формирования кредитной информации");
                        }
                    }
                }
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_6_Bankruptcy_Type block, IEventData eventData, ISource source)
        {
            return [.. (block?.Check(eventData, source)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<UL_6_Bankruptcy_Type> blocks, IEventData eventData, ISource source)
        {
            List<INotificationOfAcceptanceError> errorList = [];
            foreach (var block in blocks.OrEmptyIfNull())
            {
                errorList.AddRange(block.Check(eventData, source));
            }
            return errorList;
        }
    }
}
