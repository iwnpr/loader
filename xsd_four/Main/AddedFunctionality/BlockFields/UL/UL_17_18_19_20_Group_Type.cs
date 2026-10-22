using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class UL_17_18_19_20_Group_Type
    {
        ValueField<bool?> existField;
        ValueField<UL_17_18_19_20_Group_TypeUL_17_Debt> ul17DebtField;
        ValueField<UL_17_18_19_20_Group_TypeUL_18_DebtDue> ul18DebtDueField;

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
        public UL_17_18_19_20_Group_TypeUL_17_Debt UL_17_Debt
        {
            get
            {
                if (!ul17DebtField.IsInited)
                    ul17DebtField.Value = (UL_17_18_19_20_Group_TypeUL_17_Debt)Items.GetObject(ItemsElementName, "UL_17_Debt");
                return ul17DebtField.Value;
            }
        }

        [XmlIgnore]
        public UL_17_18_19_20_Group_TypeUL_18_DebtDue UL_18_DebtDue
        {
            get
            {
                if (!ul18DebtDueField.IsInited)
                    ul18DebtDueField.Value = (UL_17_18_19_20_Group_TypeUL_18_DebtDue)Items.GetObject(ItemsElementName, "UL_18_DebtDue");
                return ul18DebtDueField.Value;
            }
        }
    }
}