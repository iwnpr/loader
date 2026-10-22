using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_17_DealUid_Type : Block_Type
    {
        public FL_17_DealUid_Type()
        {
            blockName = refBlockNameType.FL_17_DealUid;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, object source)
        {
            InitCheck(eventData);

            if (!UidHelper.CheckUid(uid))
            {
                AddError("uid", uid, "Неверный контрольный символ УИда сделки ФЛ_17.1");
            }
            foreach (var refUidValue in refUid.OrEmptyIfNull())
            {
                if (!UidHelper.CheckUid(refUidValue))
                {
                    AddError("refUid", refUidValue, "Неверный контрольный символ УИда сделки, по которой задолженность была полностью или частично рефинансирована ФЛ_17.3");
                }
            }
            if (openDate.DateGreater(eventData?.eventDate))
            {
                AddError("openDate", openDate, "Дата совершения сделки ФЛ_17.4 позднее даты наступления события eventDate");
            }
            if (source is FL_46_UL_36_OrgSource_Type orgSource)
            {
                if (orgSource.beginDateSourceLiquidation.DateLess(openDate))
                {
                    errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_46_UL_36_OrgSource, "beginDateSourceLiquidation", orgSource.beginDateSourceLiquidation, "Дата начала ликвидации источника ФЛ_46.13 (ЮЛ_36.13) раньше даты совершения сделки ФЛ_17.4 (ЮЛ_10.4)"));
                }
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_17_DealUid_Type block, EventData eventData, object source)
        {
            return (block?.Check(eventData, source)).OrEmptyIfNull().ToList();
        }
    }
}
