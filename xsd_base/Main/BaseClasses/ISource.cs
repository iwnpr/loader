namespace Xsd
{
    public interface ISource
    {
        public DateTime sourceCreditInfoDate
        {
            get;
        }

        public List<INotificationOfAcceptanceError> Check(IDocument document);
    }
}

