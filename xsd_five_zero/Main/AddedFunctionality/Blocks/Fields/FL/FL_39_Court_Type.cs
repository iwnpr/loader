using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_39_Court_Type
    {
        ValueField<bool?> actExistField;
        ValueField<refCourtsDecisionType?> actResolutionField;
        ValueField<bool?> actStartExistField;
        ValueField<string?> currencyField;
        ValueField<DateTime?> dateField;
        ValueField<bool?> existField;
        ValueField<string?> infoField;
        ValueField<string?> numField;
        ValueField<decimal?> sumTotalField;
        ValueField<bool?> rightLimitExistField;

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
        public refCourtsDecisionType? actResolution
        {
            get
            {
                if (!actResolutionField.IsInited)
                    actResolutionField.Value = (refCourtsDecisionType?)Items.GetObject(ItemsElementName, "actResolution");
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
        public string? currency
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
        public string? info
        {
            get
            {
                if (!infoField.IsInited)
                    infoField.Value = Items.GetString(ItemsElementName, "info");
                return infoField.Value;
            }
        }

        [XmlIgnore]
        public string? num
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

        [XmlIgnore]
        public bool? rightLimitExist
        {
            get
            {
                if (!rightLimitExistField.IsInited)
                    rightLimitExistField.Value = Item.GetBool(ItemElementName, "rightLimitExist");
                return rightLimitExistField.Value;
            }
        }
    }
}