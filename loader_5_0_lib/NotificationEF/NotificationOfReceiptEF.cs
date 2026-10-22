using Xsd;

namespace loader_5_0_lib.NotificationEF
{
    /// <summary>
    /// Извещение о получении/отклонении документа, содержащего кредитную информацию
    /// </summary>
    public class NotificationOfReceiptEF
    {
        public long Id { get; set; }
        public long UploadKeyId { get; set; }
        public string SchemaVersion { get; set; }
        public string? Inn { get; set; }
        public string? Ogrn { get; set; }
        public string? SourceId { get; set; }
        public string? OutgoingDocDate { get; set; }
        public string? OutgoingDocDateTime { get; set; }
        public string OutgoingDocNumber { get; set; }
        public string AcceptanceStatus { get; set; }
        public List<NotificationOfReceiptErrorEF> Errors { get; } = [];
        public string? IncomingDocDate { get; set; }
        public string? IncomingDocDateTime { get; set; }
        public string? IncomingDocNumber { get; set; }
        public string FileName { get; set; }
        public string FileHash { get; set; }
        public string? DateTime { get; set; }
        public string? DocDateTime { get; set; }
        public string? SendingDateTime { get; set; }
        public void Load(long uploadKeyId, INotificationOfReceipt nr)
        {
            UploadKeyId = uploadKeyId;
            SchemaVersion = nr.SchemaVersion;
            Inn = nr.Inn;
            Ogrn = nr.Ogrn;
            SourceId = nr.SourceId;
            OutgoingDocDate = nr.outgoingDocDateTime;
            OutgoingDocNumber = nr.OutgoingDocNumber;
            AcceptanceStatus = nr.AcceptanceStatus;

            if (AcceptanceStatus.Equals("acceptanceStatusOK"))
            {
                IncomingDocNumber = nr.IncomingDocNumber;

                if (nr.incomingDocDateTime != null)
                {
                    IncomingDocDateTime = nr.incomingDocDateTime.Value.ToString("yyyy-MM-ddTHH:mm:sszzz");
                }
            }
            else
            {
                AddErrors([.. nr.Errors]);
            }

            if (SchemaVersion.Equals("4.1"))
            {
                DateTime = nr.docDateTime.ToString("s");
            }
            else
            {
                DocDateTime = nr.docDateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
                SendingDateTime = nr.sendingDateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
            }

            FileName = nr.FileName;
            FileHash = nr.FileHash;
        }

        private void AddErrors(List<INotificationOfReceiptErrors> receiptErrors)
        {
            foreach (var receiptError in receiptErrors)
            {
                Errors.Add(new NotificationOfReceiptErrorEF(receiptError.ErrorCode, receiptError.ErrorMessage));
            }
        }
    }
}
