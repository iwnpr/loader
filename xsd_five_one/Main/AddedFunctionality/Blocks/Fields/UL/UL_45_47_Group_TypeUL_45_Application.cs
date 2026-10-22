namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_45_47_Group_TypeUL_45_Application
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
