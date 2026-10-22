using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_Event_3_2_Type
    {
        public override List<INotificationOfAcceptanceError> Check(ISource source, SubjectTitleDataUL title)
        {
            var errorList = BaseCheck(source, title);
            if (Item == null && Item1 == null && Items.OrEmptyIfNull().Count() == 0)
            {
                errorList.Add(CreateError(null, null, null, "Должно быть хотя бы одно вложенное событие"));
            }

            if (startDate.DateGreater(eventDate))
            {
                errorList.Add(CreateError(null, "startDate", startDate, "Дата, с которой изменения вступают в силу startDate позднее даты события eventDate"));
            }
            errorList.AddRange(UL_10_DealUid.Validate(this, source));
            var orderNumList = new List<ulong>();
            errorList.AddRange(CheckEvent(UL_Event_1_4, source, title, orderNumList));
            errorList.AddRange(CheckEvent(UL_Event_1_4_1, source, title, orderNumList));
            errorList.AddRange(CheckEvent(UL_Event_2_8_1, source, title, orderNumList));
            errorList.AddRange(CheckEvent(UL_Event_2_9_1, source, title, orderNumList));
            errorList.AddRange(CheckEvent(UL_Event_2_11_2, source, title, orderNumList));

            foreach (var ULEvent in UL_Event_2_1.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(ULEvent, source, title, orderNumList));
            }

            foreach (var ULEvent in UL_Event_2_2.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(ULEvent, source, title, orderNumList));
            }

            foreach (var ULEvent in UL_Event_2_2_1.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(ULEvent, source, title, orderNumList));
            }

            foreach (var ULEvent in UL_Event_2_3.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(ULEvent, source, title, orderNumList));
            }

            foreach (var ULEvent in UL_Event_2_4.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(ULEvent, source, title, orderNumList));
            }

            foreach (var ULEvent in UL_Event_2_6.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(ULEvent, source, title, orderNumList));
            }

            foreach (var ULEvent in UL_Event_2_8_2.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(ULEvent, source, title, orderNumList));
            }

            foreach (var ULEvent in UL_Event_2_9_2.OrEmptyIfNull())
            {
                ;
                errorList.AddRange(CheckEvent(ULEvent, source, title, orderNumList));
            }

            foreach (var ULEvent in UL_Event_2_11_1.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(ULEvent, source, title, orderNumList));
            }

            foreach (var ULEvent in UL_Event_2_12.OrEmptyIfNull())
            {
                errorList.AddRange(CheckEvent(ULEvent, source, title, orderNumList));
            }

            errorList.AddRange(CheckEvent(UL_Event_2_10, source, title, orderNumList));
            errorList.AddRange(CheckEvent(UL_Event_2_11, source, title, orderNumList));
            errorList.AddRange(CheckEvent(UL_Event_2_5, source, title, orderNumList));
            errorList.AddRange(CheckEvent(UL_Event_2_8_3, source, title, orderNumList));
            errorList.AddRange(CheckEvent(UL_Event_2_8_4, source, title, orderNumList));
            errorList.AddRange(CheckEvent(UL_Event_2_9_3, source, title, orderNumList));
            errorList.AddRange(CheckEvent(UL_Event_2_9_4, source, title, orderNumList));

            return errorList;
        }

        private IEnumerable<INotificationOfAcceptanceError> CheckEvent(EventData eventData, ISource source, SubjectTitleDataUL title, List<ulong> orderNumList)
        {
            List<INotificationOfAcceptanceError> errorList = [];
            if (eventData == null)
            {
                return errorList;
            }
            if (orderNumList.Contains(eventData.orderNum))
            {
                errorList.Add(eventData.CreateError(null, "orderNum", eventData.orderNum, "OrderNum во вложенном событии должен быть уникальным"));
            }
            else
            {
                orderNumList.Add(eventData.orderNum);
            }
            if (eventData.eventDate > eventDate)
            {
                errorList.Add(eventData.CreateError(null, "eventDate", eventData.eventDate, "EventDate во вложенном событии должно быть не позднее даты eventDate события 3.2"));
            }

            if (eventData is I_UL_10_DealUid IDealUidField
                && IDealUidField.DealUid != null && IDealUidField.DealUid.uid != UL_10_DealUid.uid)
            {
                errorList.Add(eventData.CreateError(refBlockNameType.UL_10_DealUid, "uid", IDealUidField.DealUid.uid, "УИД во вложенном событии должен совпадать с УИДом 10.1 блока 10 события 3.2"));
            }
            errorList.AddRange(eventData.Check(source, title));
            errorList.ForEach(x =>
            {
                x.uid = UL_10_DealUid.uid;
                x.eventName = refEventNameType.UL_Event_3_2;
                x.orderNum_3_2Specified = true;
                x.orderNum_3_2 = x.orderNum;
                x.orderNum = orderNum;
            });
            return errorList;
        }
    }
}
