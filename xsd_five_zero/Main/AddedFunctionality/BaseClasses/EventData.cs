using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class EventData : IEventData
    {
        protected ValueField<refEventNameType> eventName;
        ValueField<string?> uid;

        [XmlIgnore]
        public DateTime eventDateTimeLocal
        {
            get
            {
                return eventDateTime.DateTime;
            }
        }


        [XmlIgnore]
        public refEventNameType EventName
        {
            get
            {
                if (!eventName.IsInited)
                {
                    eventName.Value = (refEventNameType)Enum.Parse(typeof(refEventNameType), GetType().Name.Replace("_Type", ""));
                }
                return eventName.Value;
            }
        }

        [XmlIgnore]
        public string? Uid
        {
            get
            {
                if (!uid.IsInited)
                {
                    if (this is I_DealUid_Event dealUidEvent)
                        uid.Value = dealUidEvent.DealUid?.uid;
                }
                return uid.Value;
            }
        }

        public virtual List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            List<INotificationOfAcceptanceError> errorList = [];

            if (source is FL_46_UL_36_OrgSource_Type orgSource)
            {
                if (eventDateTimeLocal.DateLess(orgSource.sourceDateStart))
                {
                    errorList.Add(CreateError(null, "eventDateTime", eventDateTime, "Дата и время наступления события eventDateTime раньше даты создания источника ФЛ_46.7 (ЮЛ_36.7)"));
                }

                if (eventDateTimeLocal.DateLess(orgSource.beginDateSourceBankruptcy))
                {
                    errorList.Add(CreateError(null, "eventDateTime", eventDateTime, "Дата и время наступления события eventDateTime раньше даты признания источника банкротом ФЛ_46.11 (ЮЛ_36.11)"));
                }

                if (eventDateTimeLocal.DateLess(orgSource.endDateSourceBankruptcy))
                {
                    errorList.Add(CreateError(null, "eventDateTime", eventDateTime, "Дата и время наступления события eventDateTime раньше даты окончания конкурсного производства ФЛ_46.12 (ЮЛ_36.12)"));
                }

                if (eventDateTimeLocal.DateLess(orgSource.beginDateSourceLiquidation))
                {
                    errorList.Add(CreateError(null, "eventDateTime", eventDateTime, "Дата и время наступления события eventDateTime раньше даты начала ликвидации источника ФЛ_46.13 (ЮЛ_36.13)"));
                }

                if (eventDateTimeLocal.DateLess(orgSource.endDateSourceLiquidation))
                {
                    errorList.Add(CreateError(null, "eventDateTime", eventDateTime, "Дата и время наступления события eventDateTime раньше даты окончания ликвидации источника ФЛ_46.14 (ЮЛ_36.14)"));
                }
            }

            if (source is FL_47_UL_37_PersonSource_Type personSource)
            {
                if (personSource.birthDate.DateGreaterEqual(eventDateTimeLocal))
                {
                    errorList.Add(CreateError(refBlockNameType.FL_47_UL_37_PersonSource, "birthDate", personSource.birthDate, "Дата и время наступления события eventDateTime должна быть позднее даты рождения ФЛ_47.4 (ЮЛ_37.4)"));
                }
                if (personSource.birthDate.DateLess(eventDateTimeLocal, -150))
                {
                    errorList.Add(CreateError(refBlockNameType.FL_47_UL_37_PersonSource, "birthDate", personSource.birthDate, "Дата и время наступления события eventDateTime минус 150 лет позднее, чем дата рождения ФЛ_47.4 (ЮЛ_37.4)"));
                }
            }
            if (source is FL_48_UL_38_ArbitrationManagerSource_Type arbitrationManagerSource)
            {
                if (arbitrationManagerSource.birthDate.DateGreaterEqual(eventDateTimeLocal))
                {
                    errorList.Add(CreateError(refBlockNameType.FL_48_UL_38_ArbitrationManagerSource, "birthDate", arbitrationManagerSource.birthDate, "Дата наступления события eventDateTime должна быть позднее даты рождения ФЛ_48.4 (ЮЛ_38.4)"));
                }
                if (arbitrationManagerSource.birthDate.DateLess(eventDateTimeLocal, -150))
                {
                    errorList.Add(CreateError(refBlockNameType.FL_48_UL_38_ArbitrationManagerSource, "birthDate", arbitrationManagerSource.birthDate, "Дата и время наступления события eventDateTime минус 150 лет позднее, чем дата рождения ФЛ_48.4 (ЮЛ_38.4)"));
                }

                if (eventDateTimeLocal.DateLess(arbitrationManagerSource.sourceAcceptDate))
                {
                    errorList.Add(CreateError(null, "eventDateTime", eventDateTime, "Дата и время наступления события eventDateTime раньше даты утверждения арбитражного управляющего ФЛ_48.8(38.8)"));
                }
            }

            if (title is SubjectTitleDataFL titleFL)
            {
                if (titleFL.FL_3_Birth.birthDate.DateGreaterEqual(eventDateTimeLocal))
                {
                    errorList.Add(CreateError(null, "eventDateTime", eventDateTime, "Дата и время наступления события eventDateTime должна быть позднее даты рождения ФЛ_3.1"));
                }
                if (titleFL.FL_3_Birth.birthDate.DateLess(eventDateTimeLocal, -150))
                {
                    errorList.Add(CreateError(null, "eventDateTime", eventDateTime, "Дата и время наступления события eventDateTime минус 150 лет позднее, чем дата рождения ФЛ_3.1"));
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

        public List<INotificationOfAcceptanceError> CheckEquality(CurrentNewBlock[] groupList, refBlockNameType groupName, CurrentBlockCache currentBlockCache)
        {
            List<INotificationOfAcceptanceError> errorList = [];

            if (groupList.OrEmptyIfNull().Any())
            {
                List<string> currentXmlList = [];
                foreach (var group in groupList.OrEmptyIfNull())
                {
                    if (group.New == null && groupList.OrEmptyIfNull().Count() == 1)
                    {
                        return errorList;
                    }

                    var currentXml = SerializeHelper.Serialize(group.Current);
                    if (currentXmlList.Count > 0 && currentXmlList.Contains(currentXml))
                    {
                        errorList.Add(CreateError(groupName, null, null, $"Части _current одного блока в одном событии совпадают"));
                    }
                    else
                    {
                        if (!currentBlockCache.ValidateCurrentBlock(currentXml))
                        {
                            errorList.Add(CreateError(groupName, null, null, $"Части _current одного блока в разных событиях одного субъекта совпадают"));
                        }
                        currentXmlList.Add(currentXml);
                    }
                    if (group.New != null && currentXml == SerializeHelper.Serialize(group.New).Replace(group.New.GetType().Name.Replace("[]", ""), group.Current.GetType().Name.Replace("[]", "")))
                    {
                        errorList.Add(CreateError(groupName, null, null, $"Части _current и _new в одном блоке совпадают"));
                    }
                }
            }

            return errorList;
        }

        public INotificationOfAcceptanceError CreateError(refBlockNameType? blockName, string? fieldName, object? fieldValue, string errorMessage)
        {
            return new Acceptance.NotificationOfAcceptanceError(this, blockName, fieldName, fieldValue, errorMessage);
        }

    }
}
