namespace Xsd.VersionFour.Main
{
    public interface IUL_10_DealUid
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

    public partial class UL_10_DealUid_Type : IUL_10_DealUid;
    public partial class UL_10_DealUid_R_Type : IUL_10_DealUid;
}
