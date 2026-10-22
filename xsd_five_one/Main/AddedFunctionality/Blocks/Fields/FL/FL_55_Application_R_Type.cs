namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_55_Application_R_Type
    {
        private bool loanSumFieldSpecified;
        private bool loanCurrencyFieldSpecified;

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool loanSumSpecified
        {
            get
            {
                return this.loanSumFieldSpecified;
            }
            set
            {
                this.loanSumFieldSpecified = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool loanCurrencySpecified
        {
            get
            {
                return this.loanCurrencyFieldSpecified;
            }
            set
            {
                this.loanCurrencyFieldSpecified = value;
            }
        }
    }
}
