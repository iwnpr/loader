namespace loader_5_0_lib.Classes
{
    public class DocInfo
    {
        public string DocDate { get; set; } = string.Empty;
        public string DocNumber { get; set; } = string.Empty;
        public DocInfo() { }
        public DocInfo(string docNumber, string docDate)
        {
            DocNumber = docNumber;
            DocDate = docDate;
        }
    }
}