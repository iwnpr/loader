namespace loader_5_0_lib.Classes
{
    public interface IAppXNames
    {
        public List<string> SubjectNodes { get; }
    }
    public class AppXNames : IAppXNames
    {
        public List<string> SubjectNodes { get { return new List<string>() { "Subject_FL", "Subject_UL" }; } }
    }
}
