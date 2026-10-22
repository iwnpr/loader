namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_45_Application_Type
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
