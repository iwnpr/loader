using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class UL_Event_3_2_Type
    {
        ValueField<UL_Event_1_4_Type> UL_Event_1_4Field;
        ValueField<UL_Event_1_4_1_Type> UL_Event_1_4_1Field;
        ValueField<UL_Event_2_11_2_Type> UL_Event_2_11_2Field;
        ValueField<UL_Event_2_8_1_Type> UL_Event_2_8_1Field;
        ValueField<UL_Event_2_9_1_Type> UL_Event_2_9_1Field;

        ValueField<UL_Event_2_1_Type[]> UL_Event_2_1Field;
        ValueField<UL_Event_2_11_1_Type[]> UL_Event_2_11_1Field;
        ValueField<UL_Event_2_12_Type[]> UL_Event_2_12Field;
        ValueField<UL_Event_2_2_Type[]> UL_Event_2_2Field;
        ValueField<UL_Event_2_2_1_Type[]> UL_Event_2_2_1Field;
        ValueField<UL_Event_2_3_Type[]> UL_Event_2_3Field;
        ValueField<UL_Event_2_4_Type[]> UL_Event_2_4Field;
        ValueField<UL_Event_2_6_Type[]> UL_Event_2_6Field;
        ValueField<UL_Event_2_8_2_Type[]> UL_Event_2_8_2Field;
        ValueField<UL_Event_2_9_2_Type[]> UL_Event_2_9_2Field;

        ValueField<UL_Event_2_10_Type> UL_Event_2_10Field;
        ValueField<UL_Event_2_11_Type> UL_Event_2_11Field;
        ValueField<UL_Event_2_5_Type> UL_Event_2_5Field;
        ValueField<UL_Event_2_8_3_Type> UL_Event_2_8_3Field;
        ValueField<UL_Event_2_8_4_Type> UL_Event_2_8_4Field;
        ValueField<UL_Event_2_9_3_Type> UL_Event_2_9_3Field;
        ValueField<UL_Event_2_9_4_Type> UL_Event_2_9_4Field;

        [XmlIgnore]
        public UL_Event_1_4_Type UL_Event_1_4
        {
            get
            {
                if (!UL_Event_1_4Field.IsInited)
                    UL_Event_1_4Field.Value = Item.GetObject<UL_Event_1_4_Type>();
                return UL_Event_1_4Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_1_4_1_Type UL_Event_1_4_1
        {
            get
            {
                if (!UL_Event_1_4_1Field.IsInited)
                    UL_Event_1_4_1Field.Value = Item.GetObject<UL_Event_1_4_1_Type>();
                return UL_Event_1_4_1Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_11_2_Type UL_Event_2_11_2
        {
            get
            {
                if (!UL_Event_2_11_2Field.IsInited)
                    UL_Event_2_11_2Field.Value = Item.GetObject<UL_Event_2_11_2_Type>();
                return UL_Event_2_11_2Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_8_1_Type UL_Event_2_8_1
        {
            get
            {
                if (!UL_Event_2_8_1Field.IsInited)
                    UL_Event_2_8_1Field.Value = Item.GetObject<UL_Event_2_8_1_Type>();
                return UL_Event_2_8_1Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_9_1_Type UL_Event_2_9_1
        {
            get
            {
                if (!UL_Event_2_9_1Field.IsInited)
                    UL_Event_2_9_1Field.Value = Item.GetObject<UL_Event_2_9_1_Type>();
                return UL_Event_2_9_1Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_1_Type[] UL_Event_2_1
        {
            get
            {
                if (!UL_Event_2_1Field.IsInited)
                    UL_Event_2_1Field.Value = Items.GetObjects<UL_Event_2_1_Type>();
                return UL_Event_2_1Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_11_1_Type[] UL_Event_2_11_1
        {
            get
            {
                if (!UL_Event_2_11_1Field.IsInited)
                    UL_Event_2_11_1Field.Value = Items.GetObjects<UL_Event_2_11_1_Type>();
                return UL_Event_2_11_1Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_12_Type[] UL_Event_2_12
        {
            get
            {
                if (!UL_Event_2_12Field.IsInited)
                    UL_Event_2_12Field.Value = Items.GetObjects<UL_Event_2_12_Type>();
                return UL_Event_2_12Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_2_Type[] UL_Event_2_2
        {
            get
            {
                if (!UL_Event_2_2Field.IsInited)
                    UL_Event_2_2Field.Value = Items.GetObjects<UL_Event_2_2_Type>();
                return UL_Event_2_2Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_2_1_Type[] UL_Event_2_2_1
        {
            get
            {
                if (!UL_Event_2_2_1Field.IsInited)
                    UL_Event_2_2_1Field.Value = Items.GetObjects<UL_Event_2_2_1_Type>();
                return UL_Event_2_2_1Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_3_Type[] UL_Event_2_3
        {
            get
            {
                if (!UL_Event_2_3Field.IsInited)
                    UL_Event_2_3Field.Value = Items.GetObjects<UL_Event_2_3_Type>();
                return UL_Event_2_3Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_4_Type[] UL_Event_2_4
        {
            get
            {
                if (!UL_Event_2_4Field.IsInited)
                    UL_Event_2_4Field.Value = Items.GetObjects<UL_Event_2_4_Type>();
                return UL_Event_2_4Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_6_Type[] UL_Event_2_6
        {
            get
            {
                if (!UL_Event_2_6Field.IsInited)
                    UL_Event_2_6Field.Value = Items.GetObjects<UL_Event_2_6_Type>();
                return UL_Event_2_6Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_8_2_Type[] UL_Event_2_8_2
        {
            get
            {
                if (!UL_Event_2_8_2Field.IsInited)
                    UL_Event_2_8_2Field.Value = Items.GetObjects<UL_Event_2_8_2_Type>();
                return UL_Event_2_8_2Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_9_2_Type[] UL_Event_2_9_2
        {
            get
            {
                if (!UL_Event_2_9_2Field.IsInited)
                    UL_Event_2_9_2Field.Value = Items.GetObjects<UL_Event_2_9_2_Type>();
                return UL_Event_2_9_2Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_10_Type UL_Event_2_10
        {
            get
            {
                if (!UL_Event_2_10Field.IsInited)
                    UL_Event_2_10Field.Value = Item1.GetObject<UL_Event_2_10_Type>();
                return UL_Event_2_10Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_11_Type UL_Event_2_11
        {
            get
            {
                if (!UL_Event_2_11Field.IsInited)
                    UL_Event_2_11Field.Value = Item1.GetObject<UL_Event_2_11_Type>();
                return UL_Event_2_11Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_5_Type UL_Event_2_5
        {
            get
            {
                if (!UL_Event_2_5Field.IsInited)
                    UL_Event_2_5Field.Value = Item1.GetObject<UL_Event_2_5_Type>();
                return UL_Event_2_5Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_8_3_Type UL_Event_2_8_3
        {
            get
            {
                if (!UL_Event_2_8_3Field.IsInited)
                    UL_Event_2_8_3Field.Value = Item1.GetObject<UL_Event_2_8_3_Type>();
                return UL_Event_2_8_3Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_8_4_Type UL_Event_2_8_4
        {
            get
            {
                if (!UL_Event_2_8_4Field.IsInited)
                    UL_Event_2_8_4Field.Value = Item1.GetObject<UL_Event_2_8_4_Type>();
                return UL_Event_2_8_4Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_9_3_Type UL_Event_2_9_3
        {
            get
            {
                if (!UL_Event_2_9_3Field.IsInited)
                    UL_Event_2_9_3Field.Value = Item1.GetObject<UL_Event_2_9_3_Type>();
                return UL_Event_2_9_3Field.Value;
            }
        }

        [XmlIgnore]
        public UL_Event_2_9_4_Type UL_Event_2_9_4
        {
            get
            {
                if (!UL_Event_2_9_4Field.IsInited)
                    UL_Event_2_9_4Field.Value = Item1.GetObject<UL_Event_2_9_4_Type>();
                return UL_Event_2_9_4Field.Value;
            }
        }
    }
}
