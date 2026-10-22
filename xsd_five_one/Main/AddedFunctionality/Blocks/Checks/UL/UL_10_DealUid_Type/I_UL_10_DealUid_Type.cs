using loader_utils;
using xsd_five_one.Main.AddedFunctionality.Blocks.Fields;

namespace Xsd.VersionFiveOne.Main
{
    public interface I_UL_10_DealUid_Type : I_DealUid_Type, I_BlockType
    {
        public List<INotificationOfAcceptanceError> Check(IEventData eventData, object source)
        {
            ErrorData errorData = new(blockName, eventData);

            if (!UidHelper.CheckUid(uid))
            {
                errorData.AddError("uid", uid, "Неверный контрольный символ УИда сделки ЮЛ_10.1");
            }

            foreach (var refUidValue in refUid.OrEmptyIfNull())
            {
                if (!UidHelper.CheckUid(refUidValue))
                {
                    errorData.AddError("refUid", refUidValue, "Неверный контрольный символ УИда сделки, по которой задолженность была полностью или частично рефинансирована ЮЛ_10.3");
                }
            }

            if (openDate.DateGreater(eventData.eventDateTimeLocal))
            {
                errorData.AddError("openDate", openDate, "Дата совершения сделки ЮЛ_10.4 позднее даты и времени наступления события eventDateTime");
            }

            if (source is FL_46_UL_36_OrgSource_Type orgSource)
            {
                if (orgSource.beginDateSourceLiquidation.DateLess(openDate))
                {
                    errorData.AddError("openDate", openDate, "Дата совершения сделки ЮЛ_10.4 позднее даты начала ликвидации источника ФЛ_46.13 (ЮЛ_36.13)");
                }
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this I_UL_10_DealUid_Type block, IEventData eventData, object source)
        {
            return [.. (block?.Check(eventData, source)).OrEmptyIfNull()];
        }
    }
}
