namespace Xsd
{
    public interface ISubjectTitleData
    {
        string subjectID
        {
            get;
        }

        string subjectCode
        {
            get;
        }

        SubjectType SubjectType
        {
            get;
        }

        CurrentBlockCache CurrentBlockCache
        {
            get;
        }

        bool SkipTitleChecksInChangeEvents
        {
            get; set;
        }

        List<INotificationOfAcceptanceError> Check(ISource source);
    }
}
