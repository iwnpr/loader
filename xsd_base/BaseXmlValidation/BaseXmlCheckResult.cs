using Xsd;

namespace xsd_base.BaseXmlValidation
{
    public class BaseXmlCheckResult
    {
        public BaseXmlCheckResult SetError(INotificationOfAcceptanceError error)
        {
            _error = error;
            return this;
        }

        INotificationOfAcceptanceError? _error;
        int _subjectCount;
        int _eventCount;

        public INotificationOfAcceptanceError? Error
        {
            get { return _error; }
        }
        public int SubjectCount
        {
            get { return _subjectCount; }
            set { _subjectCount = value; }
        }

        public int EventCount
        {
            get { return _eventCount; }
            set { _eventCount = value; }
        }
    }
}
