using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class FL_2_5_Group_TypeFL_5_PrevDoc
    {
        ValueField<string?> countryCodeField;
        ValueField<string?> countryOtherField;
        ValueField<string?> deptCodeField;
        ValueField<refIdentifyDocType?> docCodeField;
        ValueField<string?> docIssuerField;
        ValueField<string?> docNumField;
        ValueField<string?> docOtherNameField;
        ValueField<string?> docSeriesField;
        ValueField<DateTime?> endDateField;
        ValueField<string?> issueDateField;
        ValueField<bool?> prevDocFactField;

        [XmlIgnore]
        public string? countryCode
        {
            get
            {
                if (!countryCodeField.IsInited)
                    countryCodeField.Value = Items.GetString(ItemsElementName, "countryCode");
                return countryCodeField.Value;
            }
        }

        [XmlIgnore]
        public string? countryOther
        {
            get
            {
                if (!countryOtherField.IsInited)
                    countryOtherField.Value = Items.GetString(ItemsElementName, "countryOther");
                return countryOtherField.Value;
            }
        }

        [XmlIgnore]
        public string? deptCode
        {
            get
            {
                if (!deptCodeField.IsInited)
                    deptCodeField.Value = Items.GetString(ItemsElementName, "deptCode");
                return deptCodeField.Value;
            }
        }

        [XmlIgnore]
        public refIdentifyDocType? docCode
        {
            get
            {
                if (!docCodeField.IsInited)
                    docCodeField.Value = (refIdentifyDocType?)Items.GetObject(ItemsElementName, "docCode");
                return docCodeField.Value;
            }
        }

        [XmlIgnore]
        public string? docIssuer
        {
            get
            {
                if (!docIssuerField.IsInited)
                    docIssuerField.Value = Items.GetString(ItemsElementName, "docIssuer");
                return docIssuerField.Value;
            }
        }

        [XmlIgnore]
        public string? docNum
        {
            get
            {
                if (!docNumField.IsInited)
                    docNumField.Value = Items.GetString(ItemsElementName, "docNum");
                return docNumField.Value;
            }
        }

        [XmlIgnore]
        public string? docOtherName
        {
            get
            {
                if (!docOtherNameField.IsInited)
                    docOtherNameField.Value = Items.GetString(ItemsElementName, "docOtherName");
                return docOtherNameField.Value;
            }
        }

        [XmlIgnore]
        public string? docSeries
        {
            get
            {
                if (!docSeriesField.IsInited)
                    docSeriesField.Value = Items.GetString(ItemsElementName, "docSeries");
                return docSeriesField.Value;
            }
        }

        [XmlIgnore]
        public DateTime? endDate
        {
            get
            {
                if (!endDateField.IsInited)
                    endDateField.Value = Items.GetDateTime(ItemsElementName, "endDate");
                return endDateField.Value;
            }
        }

        [XmlIgnore]
        public string? issueDate
        {
            get
            {
                if (!issueDateField.IsInited)
                    issueDateField.Value = Items.GetString(ItemsElementName, "issueDate");
                return issueDateField.Value;
            }
        }

        [XmlIgnore]
        public bool? prevDocFact
        {
            get
            {
                if (!prevDocFactField.IsInited)
                    prevDocFactField.Value = Items.GetBool(ItemsElementName, "prevDocFact");
                return prevDocFactField.Value;
            }
        }
    }
}