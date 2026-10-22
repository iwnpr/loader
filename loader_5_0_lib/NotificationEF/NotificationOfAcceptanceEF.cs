using Xsd;

namespace loader_5_0_lib.NotificationEF
{
    public class NotificationOfAcceptanceEF
    {
        public long Id { get; set; }
        public long UploadKeyId { get; set; }
        public string SchemaVersion { get; set; }
        public string? Inn { get; set; }
        public string? Ogrn { get; set; }
        public string? SourceId { get; set; }
        public string DataReceivedStatus { get; set; }
        public string? OutgoingDocDate { get; set; } //ver 4.1
        public string? OutgoingDocDateTime { get; set; }
        public string OutgoingDocNumber { get; set; }
        public string? IncomingDocDate { get; set; } //ver 4.1
        public string? IncomingDocDateTime { get; set; }
        public string IncomingDocNumber { get; set; }
        public string? DateTime { get; set; } //ver 4.1
        public string? DocDateTime { get; set; }
        public string FileName { get; set; }
        public string FileHash { get; set; }
        public ulong SubjectsReceived { get; set; }
        public ulong EventsReceived { get; set; }
        public ulong EventsAccepted { get; set; }
        public ulong EventsRejected { get; set; }
        public List<NotificationOfAcceptanceErrorEF> AcceptanceErrorsEF { get; set; } = [];
        public List<NotificationOfAcceptanceWarningMessageEF> AcceptanceWarningMessagesEF { get; set; } = [];
        public string? SendingDateTime { get; set; }

        public void Load(long uploadKeyId, INotificationOfAcceptance acceptance)
        {
            //TODO: в формат
            //2023-11-24T08:00:00+02:00
            //2023-11-24T09:00:00+03:00

            UploadKeyId = uploadKeyId;
            SchemaVersion = acceptance.SchemaVersion ?? string.Empty;
            Inn = acceptance.DocInfoExt.inn;
            Ogrn = acceptance.DocInfoExt.ogrn;
            SourceId = acceptance.DocInfoExt.sourceId;


            OutgoingDocDateTime = acceptance.DocInfoExt.outgoingDocDateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
            IncomingDocDateTime = acceptance.DocInfoExt.incomingDocDateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
            DocDateTime = acceptance.DocInfoExt.docDateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
            SendingDateTime = acceptance.sendingDateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");


            OutgoingDocNumber = acceptance.DocInfoExt.outgoingDocNumber;
            IncomingDocNumber = acceptance.DocInfoExt.incomingDocNumber;
            FileName = acceptance.DocInfoExt.fileName;
            FileHash = acceptance.DocInfoExt.fileHash;
            SubjectsReceived = acceptance.DocInfoExt.subjectsReceived;
            EventsReceived = acceptance.DocInfoExt.eventsReceived;
            EventsAccepted = acceptance.DocInfoExt.eventsAccepted;
            EventsRejected = acceptance.DocInfoExt.eventsRejected;
            DataReceivedStatus = GetReceivedStatus();
            AddErrors(acceptance.ErrorsExt ?? []);
            AddWarningMessages(acceptance.WarningsExt ?? []);
        }
        private string GetReceivedStatus()
        {
            if (EventsReceived.Equals(0))
            {
                return "REJECTED";
            }
            if (EventsAccepted.Equals(0))
            {
                return "REJECTED";
            }

            if (EventsReceived > 0 && EventsAccepted.Equals(EventsReceived))
            {
                return "ACCEPTED";
            }

            if (EventsReceived > 0 && EventsReceived > EventsAccepted)
            {
                return "PARTIALLY ACCEPTED";
            }

            return "UNKNOWN";
        }
        public void AddErrors(INotificationOfAcceptanceError[] acceptancesErrors)
        {
            foreach (var acceptancesError in acceptancesErrors)
            {
                NotificationOfAcceptanceErrorEF acceptanceErrorEF = new(acceptancesError, SchemaVersion);
                AcceptanceErrorsEF.Add(acceptanceErrorEF);
            }
        }
        public void AddWarningMessages(string[] warningMessages)
        {
            foreach (var warningMessage in warningMessages)
            {
                if (!string.IsNullOrEmpty(warningMessage))
                {
                    NotificationOfAcceptanceWarningMessageEF acceptanceWarningMessageEF = new(warningMessage);
                    AcceptanceWarningMessagesEF.Add(acceptanceWarningMessageEF);
                }
            }
        }
    }
}
