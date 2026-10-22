using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_56_Participation_Type : Block_Type
    {
        public FL_56_Participation_Type()
        {
            blockName = refBlockNameType.FL_56_Participation;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IFL_17_DealUid dealUidBlock = null, FL_18_Deal_Type dealBlock = null, FL_25_26_27_28_Group_Type fl_25_28_block = null)
        {
            InitCheck(eventData);

            if (dealBlock != null)
            {
                if (role != dealBlock.role)
                {
                    AddError("role", role, "Код вида участия в сделке ФЛ_56.1 не равен коду вида участия в сделке ФЛ_18.1");
                }

                if (kindCode != dealBlock.kindCode || (kindCodeSpecified != dealBlock.kindCodeSpecified))
                {
                    AddError("kindCode", kindCodeSpecified ? kindCode : null, "Код вида займа (кредита) ФЛ_56.2 не равен коду вида займа (кредита) ФЛ_18.4");
                }
                if (!dealBlock.code.In((ushort)1) && kindCodeSpecified)
                {
                    AddError("kindCode", kindCode, "Код вида займа (кредита) ФЛ_56.2 должен отсутствовать");
                }
            }

            if (!UidHelper.CheckUid(uid))
            {
                AddError("uid", uid, "Неверный контрольный символ УИДа сделки ФЛ_56.3");
            }

            if (uid != null && dealUidBlock?.uid != null && uid != dealUidBlock.uid)
            {
                AddError("uid", uid, "УИд сделки ФЛ_56.3 не равен УИду сделки ФЛ_17.1");
            }

            if (fundDateSpecified)
            {
                if (fundDate.DateGreater(eventData?.eventDate))
                {
                    AddError("fundDate", fundDate, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ФЛ_56.4 позднее даты наступления события eventDate");
                }
                if (fundDate.DateLess(dealUidBlock?.openDate))
                {
                    AddError("fundDate", fundDate, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ФЛ_56.4 раньше даты совершения сделки ФЛ_17.4");
                }
            }

            if (fl_25_28_block?.FL_27_DebtOverdue?.missDuration > 90 && overdueExist != true)
            {
                AddError("overdueExist_0/overdueExist_1", null, "Признак просрочки должника более 90 дней ФЛ_56.5 должен быть равен «1»");
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_56_Participation_Type block, EventData eventData, IFL_17_DealUid dealUidBlock = null, FL_18_Deal_Type dealBlock = null, FL_25_26_27_28_Group_Type fl_25_28_block = null)
        {
            return (block?.Check(eventData, dealUidBlock, dealBlock, fl_25_28_block)).OrEmptyIfNull().ToList();
        }
    }
}
