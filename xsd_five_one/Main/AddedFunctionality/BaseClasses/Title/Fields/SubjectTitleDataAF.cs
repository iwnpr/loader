using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class SubjectTitleDataAF
    {
        ValueField<FL_1_4_Group_Type?> FL_1_4_GroupField;
        ValueField<FL_2_5_Group_Type[]?> FL_2_5_GroupField;
        ValueField<FL_3_Birth_Type?> FL_3_BirthField;
        ValueField<FL_6_Tax_Type[]?> FL_6_TaxField;
        ValueField<FL_7_Social_Type?> FL_7_SocialField;

        [XmlIgnore]
        public FL_1_4_Group_Type? FL_1_4_Group
        {
            get
            {
                if (!FL_1_4_GroupField.IsInited)
                    FL_1_4_GroupField.Value = Items.GetObject<FL_1_4_Group_Type>();
                return FL_1_4_GroupField.Value;
            }
        }

        [XmlIgnore]
        public FL_2_5_Group_Type[]? FL_2_5_Group
        {
            get
            {
                if (!FL_2_5_GroupField.IsInited)
                    FL_2_5_GroupField.Value = Items.GetObjects<FL_2_5_Group_Type>();
                return FL_2_5_GroupField.Value;
            }
        }

        [XmlIgnore]
        public FL_3_Birth_Type? FL_3_Birth
        {
            get
            {
                if (!FL_3_BirthField.IsInited)
                    FL_3_BirthField.Value = Items.GetObject<FL_3_Birth_Type>();
                return FL_3_BirthField.Value;
            }
        }

        [XmlIgnore]
        public FL_6_Tax_Type[]? FL_6_Tax
        {
            get
            {
                if (!FL_6_TaxField.IsInited)
                    FL_6_TaxField.Value = Items.GetObjects<FL_6_Tax_Type>();
                return FL_6_TaxField.Value;
            }
        }

        [XmlIgnore]
        public FL_7_Social_Type? FL_7_Social
        {
            get
            {
                if (!FL_7_SocialField.IsInited)
                    FL_7_SocialField.Value = Items.GetObject<FL_7_Social_Type>();
                return FL_7_SocialField.Value;
            }
        }
    }
}
