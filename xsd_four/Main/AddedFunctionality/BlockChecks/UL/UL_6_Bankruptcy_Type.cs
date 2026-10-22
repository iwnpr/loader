using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_6_Bankruptcy_Type : Block_Type
    {
        public UL_6_Bankruptcy_Type()
        {
            blockName = refBlockNameType.UL_6_Bankruptcy;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);
            if (bankruptcyFact == true)
            {
                if (date.DateGreater(eventData?.eventDate))
                {
                    AddError("date", date, "Дата публикации в ЕФРСБ сведений о стадии рассмотрения дела ЮЛ_6.3 позднее даты наступления события eventDate");
                }

                if (illegalActExist == true)
                {
                    foreach (var illegalActDateValue in illegalActDate.OrEmptyIfNull())
                    {
                        if (illegalActDateValue.DateGreater(eventData?.eventDate))
                        {
                            AddError("illegalActDate", illegalActDateValue, "Дата публикации в ЕФРСБ сообщения о неправомерных действиях ЮЛ_6.6 позднее даты наступления события eventDate");
                        }
                    }
                }

                if (fakeActExist == true)
                {
                    foreach (var fakeActDateValue in fakeActDate.OrEmptyIfNull())
                    {
                        if (fakeActDateValue.DateGreater(eventData?.eventDate))
                        {
                            AddError("illegalActDate", fakeActDateValue, "Дата публикации в ЕФРСБ сообщения о признаках преднамеренного или фиктивного банкротства ЮЛ_6.8 позднее даты наступления события eventDate");
                        }
                    }
                }
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_6_Bankruptcy_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
