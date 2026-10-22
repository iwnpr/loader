namespace Xsd.VersionFour.Main
{
    public interface IFL_17_DealUid
    {
        public string uid
        {
            get;
        }

        public string num
        {
            get;
        }

        public string[] refUid
        {
            get;
        }

        /// <remarks/>
        public string openDate
        {
            get;
        }
    }

    public partial class FL_17_DealUid_Type : IFL_17_DealUid;
    public partial class FL_17_DealUid_R_Type : IFL_17_DealUid;
}
