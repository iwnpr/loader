using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class UL_6_Bankruptcy_Type
    {
        ValueField<bool?> bankruptcyFactField;
        ValueField<string?> dateField;
        ValueField<string[]> fakeActDateField;
        ValueField<bool?> fakeActExistField;
        ValueField<bool?> illegalActExistField;
        ValueField<string[]> illegalActDateField;
        ValueField<string?> infoField;
        ValueField<string?> linkField;
        ValueField<string?> stageField;

        [XmlIgnore]
        public bool? bankruptcyFact
        {
            get
            {
                if (!bankruptcyFactField.IsInited)
                    bankruptcyFactField.Value = Items.GetBool(ItemsElementName, "bankruptcyFact");
                return bankruptcyFactField.Value;
            }
        }

        [XmlIgnore]
        public string date
        {
            get
            {
                if (!dateField.IsInited)
                    dateField.Value = Items.GetString(ItemsElementName, "date");
                return dateField.Value;
            }
        }

        [XmlIgnore]
        public string[] fakeActDate
        {
            get
            {
                if (!fakeActDateField.IsInited)
                    fakeActDateField.Value = Items.GetObjects(ItemsElementName, "fakeActDate").Select(x => x.ToString()).ToArray(); ;
                return fakeActDateField.Value;
            }
        }

        [XmlIgnore]
        public bool? fakeActExist
        {
            get
            {
                if (!fakeActExistField.IsInited)
                    fakeActExistField.Value = Items.GetBool(ItemsElementName, "fakeActExist");
                return fakeActExistField.Value;
            }
        }

        [XmlIgnore]
        public bool? illegalActExist
        {
            get
            {
                if (!illegalActExistField.IsInited)
                    illegalActExistField.Value = Items.GetBool(ItemsElementName, "illegalActExist");
                return illegalActExistField.Value;
            }
        }

        [XmlIgnore]
        public string[] illegalActDate
        {
            get
            {
                if (!illegalActDateField.IsInited)
                    illegalActDateField.Value = Items.GetObjects(ItemsElementName, "illegalActDate").Select(x => x.ToString()).ToArray(); ;
                return illegalActDateField.Value;
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
        public string link
        {
            get
            {
                if (!linkField.IsInited)
                    linkField.Value = Items.GetString(ItemsElementName, "link");
                return linkField.Value;
            }
        }

        [XmlIgnore]
        public string stage
        {
            get
            {
                if (!stageField.IsInited)
                    stageField.Value = Items.GetString(ItemsElementName, "stage");
                return stageField.Value;
            }
        }
    }
}
