using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_13_Bankruptcy_Type : Block_Type
    {
        public FL_13_Bankruptcy_Type()
        {
            blockName = refBlockNameType.FL_13_Bankruptcy;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, FL_3_Birth_Type? birthBlock = null)
        {
            InitCheck(eventData);
            if (bankruptcyFact == true)
            {
                if (date.DateGreater(eventData?.eventDate))
                {
                    AddError("date", date, "Дата публикации в ЕФРСБ сведений о стадии рассмотрения дела ФЛ_13.3 позднее даты наступления события eventDate");
                }
                if (date.DateLess(birthBlock?.birthDate))
                {
                    AddError("date", date, "Дата публикации в ЕФРСБ сведений о стадии рассмотрения дела ФЛ_13.3 раньше даты рождения ФЛ_3.1");
                }

                if (illegalActExist == true)
                {
                    foreach (var illegalActDateValue in illegalActDate.OrEmptyIfNull())
                    {
                        if (illegalActDateValue.DateGreater(eventData?.eventDate))
                        {
                            AddError("illegalActDate", illegalActDateValue, "Дата публикации в ЕФРСБ сообщения о неправомерных действиях ФЛ_13.6 позднее даты наступления события eventDate");
                        }
                        if (illegalActDateValue.DateLess(birthBlock?.birthDate))
                        {
                            AddError("illegalActDate", illegalActDateValue, "Дата публикации в ЕФРСБ сообщения о неправомерных действиях ФЛ_13.6 раньше даты рождения ФЛ_3.1");
                        }
                    }
                }

                if (fakeActExist == true)
                {
                    foreach (var fakeActDateValue in fakeActDate.OrEmptyIfNull())
                    {
                        if (fakeActDateValue.DateGreater(eventData?.eventDate))
                        {
                            AddError("illegalActDate", fakeActDateValue, "Дата публикации в ЕФРСБ сообщения о признаках преднамеренного или фиктивного банкротства ФЛ_13.8 позднее даты наступления события eventDate");
                        }
                        if (fakeActDateValue.DateLess(birthBlock?.birthDate))
                        {
                            AddError("illegalActDate", fakeActDateValue, "ДДата публикации в ЕФРСБ сообщения о признаках преднамеренного или фиктивного банкротства ФЛ_13.8 раньше даты рождения ФЛ_3.1");
                        }
                    }
                }
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_13_Bankruptcy_Type block, EventData eventData, FL_3_Birth_Type? birthBlock = null)
        {
            return (block?.Check(eventData, birthBlock)).OrEmptyIfNull().ToList();
        }
    }
}
