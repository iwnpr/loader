using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_Event_3_2_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, ISubjectTitleData title)
        {
            var errorList = base.Check(source, title);
            if (Item == null && Item1 == null && !Items.OrEmptyIfNull().Any())
            {
                errorList.Add(CreateError(null, null, null, "Должно быть хотя бы одно вложенное событие"));
            }

            if (startDateTime.DateGreater(eventDateTimeLocal))
            {
                errorList.Add(CreateError(null, "startDateTime", startDateTime, "Дата и время, с которой изменения вступают в силу startDateTime позднее даты события eventDateTime"));
            }
            errorList.AddRange(FL_17_DealUid.Validate(this, source));
            errorList.AddRange(CheckEvent(FL_Event_1_4, source, title));
            errorList.AddRange(CheckEvent(FL_Event_1_4_1, source, title));
            errorList.AddRange(CheckEvent(FL_Event_2_8_1, source, title));
            errorList.AddRange(CheckEvent(FL_Event_2_9_1, source, title));
            errorList.AddRange(CheckEvent(FL_Event_2_11_2, source, title));

            foreach (var flEvent in FL_Event_2_1.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(flEvent, source, title));
            }

            foreach (var flEvent in FL_Event_2_2.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(flEvent, source, title));
            }

            foreach (var flEvent in FL_Event_2_2_1.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(flEvent, source, title));
            }

            foreach (var flEvent in FL_Event_2_3.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(flEvent, source, title));
            }

            foreach (var flEvent in FL_Event_2_4.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(flEvent, source, title));
            }

            foreach (var flEvent in FL_Event_2_6.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(flEvent, source, title));
            }

            foreach (var flEvent in FL_Event_2_8_2.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(flEvent, source, title));
            }

            foreach (var flEvent in FL_Event_2_9_2.OrEmptyIfNull())
            {
                ;
                errorList.AddRange(CheckEvent(flEvent, source, title));
            }

            foreach (var flEvent in FL_Event_2_11_1.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(flEvent, source, title));
            }

            foreach (var flEvent in FL_Event_2_12.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(flEvent, source, title));
            }

            errorList.AddRange(CheckEvent(FL_Event_2_10, source, title));
            errorList.AddRange(CheckEvent(FL_Event_2_11, source, title));
            errorList.AddRange(CheckEvent(FL_Event_2_5, source, title));
            errorList.AddRange(CheckEvent(FL_Event_2_8_3, source, title));
            errorList.AddRange(CheckEvent(FL_Event_2_8_4, source, title));
            errorList.AddRange(CheckEvent(FL_Event_2_9_3, source, title));
            errorList.AddRange(CheckEvent(FL_Event_2_9_4, source, title));

            return errorList;
        }

        private IEnumerable<INotificationOfAcceptanceError> CheckEvent(IEventData eventData, ISource source, ISubjectTitleData title)
        {
            List<INotificationOfAcceptanceError> errorList = [];
            if (eventData == null)
            {
                return errorList;
            }

            if (eventData.eventDateTime > eventDateTime)
            {
                errorList.Add(eventData.CreateError(null, "eventDateTime", eventData.eventDateTime, "eventDateTime во вложенном событии должно быть не позднее даты eventDateTime события 3.2"));
            }

            if (eventData is I_DealUid_Event dealUidEvent
                && dealUidEvent.DealUid != null && dealUidEvent.DealUid.uid != FL_17_DealUid.uid)
            {
                errorList.Add(eventData.CreateError(refBlockNameType.FL_17_DealUid, "uid", dealUidEvent.DealUid.uid, "УИД во вложенном событии должен совпадать с УИДом 17.1 блока 17 события 3.2"));
            }
            errorList.AddRange(eventData.Check(source, title));
            errorList = errorList.Where(x => !x.IgnoreErrorIn32).ToList();
            errorList.ForEach(x =>
            {
                x.uid = FL_17_DealUid.uid;
                x.eventName = refEventNameType.FL_Event_3_2;
                x.orderNum_3_2Specified = true;
                x.orderNum_3_2 = x.orderNum;
                x.orderNum = orderNum;
            });
            return errorList;
        }
    }
}
