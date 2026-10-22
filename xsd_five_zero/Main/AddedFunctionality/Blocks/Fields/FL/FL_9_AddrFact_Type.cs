using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_9_AddrFact_Type
    {
        ValueField<string?> apartField;
        ValueField<string?> blockField;
        ValueField<string?> buildField;
        ValueField<string?> countryCodeField;
        ValueField<string?> countryOtherField;
        ValueField<string?> estateField;
        ValueField<bool?> existField;
        ValueField<string?> houseField;
        ValueField<string?> locationCodeField;
        ValueField<string?> locationOtherField;
        ValueField<string?> postCodeField;
        ValueField<string?> regStateNumField;
        ValueField<string?> streetField;

        [XmlIgnore]
        public string? apart
        {
            get
            {
                if (!apartField.IsInited)
                    apartField.Value = Items.GetString(ItemsElementName, "apart");
                return apartField.Value;
            }
        }

        [XmlIgnore]
        public string? block
        {
            get
            {
                if (!blockField.IsInited)
                    blockField.Value = Items.GetString(ItemsElementName, "block");
                return blockField.Value;
            }
        }

        [XmlIgnore]
        public string? build
        {
            get
            {
                if (!buildField.IsInited)
                    buildField.Value = Items.GetString(ItemsElementName, "build");
                return buildField.Value;
            }
        }

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
        public string? estate
        {
            get
            {
                if (!estateField.IsInited)
                    estateField.Value = Items.GetString(ItemsElementName, "estate");
                return estateField.Value;
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
        public string? house
        {
            get
            {
                if (!houseField.IsInited)
                    houseField.Value = Items.GetString(ItemsElementName, "house");
                return houseField.Value;
            }
        }

        [XmlIgnore]
        public string? locationCode
        {
            get
            {
                if (!locationCodeField.IsInited)
                    locationCodeField.Value = Items.GetString(ItemsElementName, "locationCode");
                return locationCodeField.Value;
            }
        }

        [XmlIgnore]
        public string? locationOther
        {
            get
            {
                if (!locationOtherField.IsInited)
                    locationOtherField.Value = Items.GetString(ItemsElementName, "locationOther");
                return locationOtherField.Value;
            }
        }

        [XmlIgnore]
        public string? postCode
        {
            get
            {
                if (!postCodeField.IsInited)
                    postCodeField.Value = Items.GetString(ItemsElementName, "postCode");
                return postCodeField.Value;
            }
        }

        [XmlIgnore]
        public string? regStateNum
        {
            get
            {
                if (!regStateNumField.IsInited)
                    regStateNumField.Value = Items.GetString(ItemsElementName, "regStateNum");
                return regStateNumField.Value;
            }
        }

        [XmlIgnore]
        public string? street
        {
            get
            {
                if (!streetField.IsInited)
                    streetField.Value = Items.GetString(ItemsElementName, "street");
                return streetField.Value;
            }
        }
    }
}