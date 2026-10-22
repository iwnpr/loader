using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_46_Participation_Type : Block_Type
    {
        public UL_46_Participation_Type()
        {
            blockName = refBlockNameType.UL_46_Participation;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IUL_10_DealUid dealUidBlock = null, UL_11_Deal_Type dealBlock = null, UL_17_18_19_20_Group_Type ul_17_20_block = null)
        {
            InitCheck(eventData);

            if (dealBlock != null)
            {
                if (role != dealBlock.role)
                {
                    AddError("role", role, "Код вида участия в сделке ЮЛ_46.1 не равен коду вида участия в сделке ЮЛ_11.1");
                }

                if (kindCode != dealBlock.kindCode || (kindCodeSpecified != dealBlock.kindCodeSpecified))
                {
                    AddError("kindCode", kindCodeSpecified ? kindCode : null, "Код вида займа (кредита) ЮЛ_46.2 не равен коду вида займа (кредита) ЮЛ_11.4");
                }
                if (!dealBlock.code.In(1) && kindCodeSpecified)
                {
                    AddError("kindCode", kindCode, "Код вида займа (кредита) ЮЛ_46.2 должен отсутствовать");
                }
            }

            if (!UidHelper.CheckUid(uid))
            {
                AddError("uid", uid, "Неверный контрольный символ УИДа сделки ЮЛ_46.3");
            }

            if (uid != null && dealUidBlock?.uid != null && uid != dealUidBlock?.uid)
            {
                AddError("uid", uid, "УИд сделки ЮЛ_46.3 не равен УИду сделки ЮЛ_10.1");
            }

            if (fundDateSpecified)
            {
                if (fundDate.DateGreater(eventData?.eventDate))
                {
                    AddError("fundDate", fundDate, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ЮЛ_46.4 позднее даты наступления события eventDate");
                }
                if (fundDate.DateLess(dealUidBlock?.openDate))
                {
                    AddError("fundDate", fundDate, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ЮЛ_46.4 раньше даты совершения сделки ЮЛ_10.4");
                }
            }

            if (ul_17_20_block?.UL_19_DebtOverdue?.missDuration > 90 && overdueExist != true)
            {
                AddError("overdueExist_0/overdueExist_1", null, "Признак просрочки должника более 90 дней ЮЛ_46.5 должен быть равен «1»");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_46_Participation_Type block, EventData eventData, IUL_10_DealUid dealUidBlock = null, UL_11_Deal_Type dealBlock = null, UL_17_18_19_20_Group_Type ul_17_20_block = null)
        {
            return (block?.Check(eventData, dealUidBlock, dealBlock, ul_17_20_block)).OrEmptyIfNull().ToList();
        }
    }
}
