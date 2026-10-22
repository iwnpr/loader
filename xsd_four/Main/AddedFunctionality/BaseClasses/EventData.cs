using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class EventData : IEventData
    {
        protected refEventNameType? eventName = null;

        [XmlIgnore]
        public DateTime? EventDate
        {
            get
            {
                return eventDateField;
            }
        }

        [XmlIgnore]
        public string? EventTime
        {
            get
            {
                return eventTimeField;
            }
        }

        [XmlIgnore]
        public refEventNameType EventName
        {
            get
            {
                if (eventName == null)
                {
                    eventName = (refEventNameType)Enum.Parse(typeof(refEventNameType), GetType().Name.Replace("_Type", ""));
                }
                return eventName.Value;
            }
        }

        public List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData titleData)
        {
            if (titleData is SubjectTitleDataFL)
            {
                return Check(source, (SubjectTitleDataFL)titleData);
            }
            else
            {
                return Check(source, (SubjectTitleDataUL)titleData);
            }
        }

        public virtual List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataFL titleData)
        {
            return BaseCheck(source, titleData);
        }

        public virtual List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataUL titleData)
        {
            return BaseCheck(source, titleData);
        }

        protected List<INotificationOfAcceptanceError> BaseCheck(ISource source, object titleData)
        {
            List<INotificationOfAcceptanceError> errorList = new List<INotificationOfAcceptanceError>();

            //

            if (source is FL_46_UL_36_OrgSource_Type orgSource)
            {
                if (eventDate.DateGreater(orgSource.sourceCreditInfoDate))
                {
                    errorList.Add(CreateError(null, "eventDate", eventDate, "Дата события eventDate больше даты формирования кредитной информации"));
                }

                if (eventDate.DateLess(orgSource.sourceDateStart))
                {
                    errorList.Add(CreateError(null, "eventDate", eventDate, "Дата наступления события eventDate раньше даты создания источника ФЛ_46.7 (ЮЛ_36.7)"));
                }

                if (eventDate.DateLess(orgSource.beginDateSourceBankruptcy))
                {
                    errorList.Add(CreateError(null, "eventDate", eventDate, "Дата признания источника банкротом ФЛ_46.11 (ЮЛ_36.11) позднее даты наступления события eventDate"));
                }

                if (eventDate.DateLess(orgSource.endDateSourceBankruptcy))
                {
                    errorList.Add(CreateError(null, "eventDate", eventDate, "Дата окончания конкурсного производства ФЛ_46.12 (ЮЛ_36.12) позднее даты наступления события eventDate"));
                }

                if (eventDate.DateLess(orgSource.beginDateSourceLiquidation))
                {
                    errorList.Add(CreateError(null, "eventDate", eventDate, "Дата начала ликвидации источника ФЛ_46.13 (ЮЛ_36.13) позднее даты наступления события eventDate"));
                }

                if (eventDate.DateLess(orgSource.endDateSourceLiquidation))
                {
                    errorList.Add(CreateError(null, "eventDate", eventDate, "Дата окончания ликвидации источника ФЛ_46.14 (ЮЛ_36.14) позднее даты наступления события eventDate"));
                }
            }

            if (source is FL_47_UL_37_PersonSource_Type personSource)
            {
                if (eventDate.DateGreater(personSource.sourceCreditInfoDate))
                {
                    errorList.Add(CreateError(null, "eventDate", eventDate, "Дата события eventDate больше даты формирования кредитной информации"));
                }

                if (personSource.birthDate.DateGreaterEqual(eventDate))
                {
                    errorList.Add(CreateError(refBlockNameType.FL_47_UL_37_PersonSource, "birthDate", personSource.birthDate, "Дата рождения ФЛ_47.4 (ЮЛ_37.4) должна быть раньше даты наступления события eventDatee"));
                }
                if (personSource.birthDate.DateLess(eventDate, -150))
                {
                    errorList.Add(CreateError(refBlockNameType.FL_47_UL_37_PersonSource, "birthDate", personSource.birthDate, "Дата рождения ФЛ_47.4 (ЮЛ_37.4) раньше, чем дата наступления события eventDate минус 150 лет"));
                }
            }

            if (source is FL_48_UL_38_ArbitrationManagerSource_Type arbitrationManagerSource)
            {

                if (eventDate.DateGreater(arbitrationManagerSource.sourceCreditInfoDate))
                {
                    errorList.Add(CreateError(null, "eventDate", eventDate, "Дата события eventDate больше даты формирования кредитной информации"));
                }

                if (arbitrationManagerSource.birthDate.DateGreaterEqual(eventDate))
                {
                    errorList.Add(CreateError(refBlockNameType.FL_48_UL_38_ArbitrationManagerSource, "birthDate", arbitrationManagerSource.birthDate, "Дата рождения ФЛ_48.4 (ЮЛ_38.4) раньше, чем дата наступления события eventDate минус 150 лет"));
                }
                if (arbitrationManagerSource.birthDate.DateLess(eventDate, -150))
                {
                    errorList.Add(CreateError(refBlockNameType.FL_48_UL_38_ArbitrationManagerSource, "birthDate", arbitrationManagerSource.birthDate, "Дата рождения ФЛ_48.4 (ЮЛ_38.4) должна быть раньше даты наступления события eventDate"));
                }
            }

            if (titleData is SubjectTitleDataFL titleFL)
            {
                if (titleFL.FL_3_Birth.birthDate.DateGreaterEqual(eventDate))
                {
                    errorList.Add(CreateError(refBlockNameType.FL_3_Birth, "birthDate", titleFL.FL_3_Birth.birthDate, "Дата наступления события eventDate должна быть позднее даты рождения ФЛ_3.1"));
                }
                if (titleFL.FL_3_Birth.birthDate.DateLess(eventDate, -150))
                {
                    errorList.Add(CreateError(refBlockNameType.FL_3_Birth, "birthDate", titleFL.FL_3_Birth.birthDate, "Дата рождения ФЛ_3.1 раньше даты наступления события eventDate минус 150 лет"));
                }
            }

            if (this is FL_Event_4_1_Type fl_event_4_1 && fl_event_4_1.operationCode.In(FL_Event_4_1_TypeOperationCode.D1, FL_Event_4_1_TypeOperationCode.D2) && !fl_event_4_1.changeReasonSpecified
                || this is FL_Event_4_2_Type fl_event_4_2 && fl_event_4_2.operationCode.In(FL_Event_4_2_TypeOperationCode.D1, FL_Event_4_2_TypeOperationCode.D2) && !fl_event_4_2.changeReasonSpecified
                || this is FL_Event_4_3_Type fl_event_4_3 && fl_event_4_3.operationCode.In(FL_Event_4_3_TypeOperationCode.D1, FL_Event_4_3_TypeOperationCode.D2) && !fl_event_4_3.changeReasonSpecified
                || this is FL_Event_4_4_Type fl_event_4_4 && fl_event_4_4.operationCode.In(FL_Event_4_4_TypeOperationCode.D1, FL_Event_4_4_TypeOperationCode.D2) && !fl_event_4_4.changeReasonSpecified
                || this is UL_Event_4_1_Type ul_event_4_1 && ul_event_4_1.operationCode.In(UL_Event_4_1_TypeOperationCode.D1, UL_Event_4_1_TypeOperationCode.D2) && !ul_event_4_1.changeReasonSpecified
                || this is UL_Event_4_2_Type ul_event_4_2 && ul_event_4_2.operationCode.In(UL_Event_4_2_TypeOperationCode.D1, UL_Event_4_2_TypeOperationCode.D2) && !ul_event_4_2.changeReasonSpecified
                || this is UL_Event_4_3_Type ul_event_4_3 && ul_event_4_3.operationCode.In(UL_Event_4_3_TypeOperationCode.D1, UL_Event_4_3_TypeOperationCode.D2) && !ul_event_4_3.changeReasonSpecified
                || this is UL_Event_4_4_Type ul_event_4_4 && ul_event_4_4.operationCode.In(UL_Event_4_4_TypeOperationCode.D1, UL_Event_4_4_TypeOperationCode.D2) && !ul_event_4_4.changeReasonSpecified)
            {
                errorList.Add(CreateError(null, "changeReason", null, "Отсутствует код причины изменения кредитной информации changeReason"));
            }
            return errorList;
        }

        protected List<INotificationOfAcceptanceError> CheckEquality(CurrentNewBlock[] groupList, refBlockNameType groupName, CurrentBlockCache currentBlockCache)
        {
            List<INotificationOfAcceptanceError> errorList = new List<INotificationOfAcceptanceError>();

            if (groupList.OrEmptyIfNull().Count() > 0)
            {
                List<string> currentXmlList = new List<string>();
                foreach (var group in groupList.OrEmptyIfNull())
                {
                    if (group.New == null && groupList.OrEmptyIfNull().Count() == 1)
                    {
                        return errorList;
                    }

                    var currentXml = SerializeHelper.Serialize(group.Current);
                    if (currentXmlList.Count > 0 && currentXmlList.Contains(currentXml))
                    {
                        errorList.Add(CreateError(groupName, null, null, "Части _current одного блока в одном событии совпадают"));
                    }
                    else
                    {
                        if (!currentBlockCache.ValidateCurrentBlock(currentXml))
                        {
                            errorList.Add(CreateError(groupName, null, null, "Части _current одного блока в разных событиях одного субъекта совпадают"));
                        }
                        currentXmlList.Add(currentXml);
                    }
                    if (group.New != null && currentXml == SerializeHelper.Serialize(group.New).Replace(group.New.GetType().Name.Replace("[]", ""), group.Current.GetType().Name.Replace("[]", "")))
                    {
                        errorList.Add(CreateError(groupName, null, null, "Части _current и _new в одном блоке совпадают"));
                    }
                }
            }

            return errorList;
        }

        public INotificationOfAcceptanceError CreateError(refBlockNameType? blockName, string? fieldName, object? fieldValue, string errorMessage)
        {
            return new Acceptance.NotificationOfAcceptanceError(this, blockName, fieldName, fieldValue, errorMessage);
        }

        ValueField<string?> uid;

        public string? Uid
        {
            get
            {
                if (!uid.IsInited)
                {
                    if (this is I_FL_17_DealUid i_FL_17_DealUid)
                        uid.Value = i_FL_17_DealUid.DealUid?.uid;
                    if (this is I_FL_17_DealUid_R i_FL_17_DealUid_R)
                        uid.Value = i_FL_17_DealUid_R.DealUid_R?.uid;
                    if (this is I_UL_10_DealUid i_UL_10_DealUid)
                        uid.Value = i_UL_10_DealUid.DealUid?.uid;
                    if (this is I_UL_10_DealUid_R i_UL_10_DealUid_R)
                        uid.Value = i_UL_10_DealUid_R.DealUid_R?.uid;
                }
                return uid.Value;
            }
        }
    }
}
