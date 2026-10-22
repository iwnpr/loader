using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_56_Participation_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_56_Participation;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock, FL_18_Deal_Type? dealBlock, FL_24_Fund_Type[]? fundBlocks, FL_25_26_27_28_Group_Type? fl_25_28_block)
        {
            ErrorData errorData = new(blockName, eventData);

            if (dealBlock != null)
            {
                if (role != dealBlock.role)
                {
                    errorData.AddError("role", role, "Код вида участия в сделке ФЛ_56.1 не равен коду вида участия в сделке ФЛ_18.1");
                }
                if (dealBlock.kindCodeSpecified && kindCode != dealBlock.kindCode)
                {
                    errorData.AddError("kindCode", kindCode, "Код вида займа (кредита) ФЛ_56.2 не равен коду вида займа (кредита) ФЛ_18.4");
                }
            }

            if (!UidHelper.CheckUid(uid))
            {
                errorData.AddError("uid", uid, "Неверный контрольный символ УИДа сделки ФЛ_56.3");
            }

            if (uid != null && dealUidBlock?.uid != null && uid != dealUidBlock.uid)
            {
                errorData.AddError("uid", uid, "УИд сделки ФЛ_56.3 не равен УИду сделки ФЛ_17.1");
            }

            if (fundDateSpecified)
            {
                if (fundDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("fundDate", fundDate, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ФЛ_56.4 позднее даты и времени наступления события eventDateTime");
                }
                if (fundDate.DateLess(dealUidBlock?.openDate))
                {
                    errorData.AddError("fundDate", fundDate, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ФЛ_56.4 раньше даты совершения сделки ФЛ_17.4");
                }
                if (fundBlocks != null &&
                    fundBlocks.Where(x => x.dateSpecified).Any() &&
                    fundDate != fundBlocks.Where(x => x.dateSpecified).Max(x => x.date))
                {
                    errorData.AddError("fundDate", fundDate, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ФЛ_56.4 не равна дате передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ФЛ_24.1");
                }
            }

            if (fl_25_28_block?.FL_27_DebtOverdue?.missDuration.DecimalGreater(90) == true && overdueExist != true)
            {
                errorData.AddError("overdueExist_0/overdueExist_1", null, "Признак просрочки должника более 90 дней ФЛ_56.5 должен быть равен «1»");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_56_Participation_Type block, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null, FL_18_Deal_Type? dealBlock = null, FL_24_Fund_Type[]? fundBlocks = null, FL_25_26_27_28_Group_Type? fl_25_28_block = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock, dealBlock, fundBlocks, fl_25_28_block)).OrEmptyIfNull()];
        }
    }
}
