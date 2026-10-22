using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class UL_30_Court_Type
    {
        ValueField<bool?> actExistField;
        ValueField<refCourtsDecisionType> actResolutionField;
        ValueField<bool?> actStartExistField;
        ValueField<string?> currencyField;
        ValueField<DateTime?> dateField;
        ValueField<bool?> existField;
        ValueField<string?> infoField;
        ValueField<ushort?> lawsuitCodeField;
        ValueField<string?> numField;
        ValueField<decimal?> sumTotalField;

        [XmlIgnore]
        public bool? actExist
        {
            get
            {
                if (!actExistField.IsInited)
                    actExistField.Value = Items.GetBool(ItemsElementName, "actExist");
                return actExistField.Value;
            }
        }

        [XmlIgnore]
        public refCourtsDecisionType actResolution
        {
            get
            {
                if (!actResolutionField.IsInited)
                    actResolutionField.Value = (refCourtsDecisionType)Items.GetObject(ItemsElementName, "actResolution");
                return actResolutionField.Value;
            }
        }

        [XmlIgnore]
        public bool? actStartExist
        {
            get
            {
                if (!actStartExistField.IsInited)
                    actStartExistField.Value = Items.GetBool(ItemsElementName, "actStartExist");
                return actStartExistField.Value;
            }
        }

        [XmlIgnore]
        public string currency
        {
            get
            {
                if (!currencyField.IsInited)
                    currencyField.Value = Items.GetString(ItemsElementName, "currency");
                return currencyField.Value;
            }
        }

        [XmlIgnore]
        public DateTime? date
        {
            get
            {
                if (!dateField.IsInited)
                    dateField.Value = Items.GetDateTime(ItemsElementName, "date");
                return dateField.Value;
            }
        }

        [XmlIgnore]
        public bool? exist
        {
            get
            {
                if (!existField.IsInited)
                    existField.Value = Items.GetBool(ItemsElementName, "exist");
                return existField.Value;
            }
        }

        [XmlIgnore]
        public string info
        {
            get
            {
                if (!infoField.IsInited)
                    infoField.Value = Items.GetString(ItemsElementName, "info");
                return infoField.Value;
            }
        }

        [XmlIgnore]
        public ushort? lawsuitCode
        {
            get
            {
                if (!lawsuitCodeField.IsInited)
                    lawsuitCodeField.Value = Items.GetUshort(ItemsElementName, "lawsuitCode");
                return lawsuitCodeField.Value;
            }
        }

        [XmlIgnore]
        public string num
        {
            get
            {
                if (!numField.IsInited)
                    numField.Value = Items.GetString(ItemsElementName, "num");
                return numField.Value;
            }
        }

        [XmlIgnore]
        public decimal? sumTotal
        {
            get
            {
                if (!sumTotalField.IsInited)
                    sumTotalField.Value = Items.GetDecimal(ItemsElementName, "sumTotal");
                return sumTotalField.Value;
            }
        }
    }
}