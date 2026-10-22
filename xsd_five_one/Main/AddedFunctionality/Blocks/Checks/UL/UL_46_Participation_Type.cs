using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_46_Participation_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_46_Participation;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock, UL_11_Deal_Type? dealBlock, UL_17_18_19_20_Group_Type? ul_17_20_block, UL_16_Fund_Type[]? fundBlocks)
        {
            ErrorData errorData = new(blockName, eventData);

            if (dealBlock != null)
            {
                if (role != dealBlock.role)
                {
                    errorData.AddError("role", role, "Код вида участия в сделке ЮЛ_46.1 не равен коду вида участия в сделке ЮЛ_11.1");
                }

                if (dealBlock.kindCodeSpecified && kindCode != dealBlock.kindCode)
                {
                    errorData.AddError("kindCode", kindCode, "Код вида займа (кредита) ЮЛ_46.2 не равен коду вида займа (кредита) ЮЛ_11.4");
                }
            }

            if (!UidHelper.CheckUid(uid))
            {
                errorData.AddError("uid", uid, "Неверный контрольный символ УИДа сделки ЮЛ_46.3");
            }

            if (uid != null && dealUidBlock?.uid != null && uid != dealUidBlock?.uid)
            {
                errorData.AddError("uid", uid, "УИд сделки ЮЛ_46.3 не равен УИду сделки ЮЛ_10.1");
            }

            if (fundDateSpecified)
            {
                if (fundDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("fundDate", fundDate, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ЮЛ_46.4 позднее даты и времени наступления события eventDateTime");
                }
                if (fundDate.DateLess(dealUidBlock?.openDate))
                {
                    errorData.AddError("fundDate", fundDate, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ЮЛ_46.4 раньше даты совершения сделки ЮЛ_10.4");
                }
                if (fundBlocks != null &&
                    fundBlocks.Where(x => x.dateSpecified).Any() &&
                    fundDate != fundBlocks.Where(x => x.dateSpecified).Max(x => x.date))
                {
                    errorData.AddError("fundDate", fundDate, "Дата передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ЮЛ_46.4 не равна дате передачи финансирования субъекту или возникновения обеспечения исполнения обязательства ЮЛ_16.1");
                }
            }

            if (ul_17_20_block?.UL_19_DebtOverdue?.missDuration.IntGreater(90) == true && overdueExist != true)
            {
                errorData.AddError("overdueExist_0/overdueExist_1", null, "Признак просрочки должника более 90 дней ЮЛ_46.5 должен быть равен «1»");
            }
            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_46_Participation_Type block, IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock = null, UL_11_Deal_Type? dealBlock = null, UL_17_18_19_20_Group_Type? ul_17_20_block = null, UL_16_Fund_Type[]? fundBlocks = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock, dealBlock, ul_17_20_block, fundBlocks)).OrEmptyIfNull()];
        }
    }
}
