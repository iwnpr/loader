using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveZero.Main
{
    public partial class FL_25_26_27_28_Group_Type
    {
        ValueField<bool?> lastPayExistField;
        ValueField<bool?> existField;
        ValueField<FL_25_26_27_28_Group_TypeFL_25_Debt?> fl25DebtField;
        ValueField<FL_25_26_27_28_Group_TypeFL_26_DebtDue?> fl26DebtDueField;

        [XmlIgnore]
        public bool? lastPayExist
        {
            get
            {
                if (!lastPayExistField.IsInited)
                    lastPayExistField.Value = Item.GetBool(ItemElementName, "lastPayExist");
                return lastPayExistField.Value;
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
        public FL_25_26_27_28_Group_TypeFL_25_Debt? FL_25_Debt
        {
            get
            {
                if (!fl25DebtField.IsInited)
                    fl25DebtField.Value = (FL_25_26_27_28_Group_TypeFL_25_Debt?)Items.GetObject(ItemsElementName, "FL_25_Debt");
                return fl25DebtField.Value;
            }
        }

        [XmlIgnore]
        public FL_25_26_27_28_Group_TypeFL_26_DebtDue? FL_26_DebtDue
        {
            get
            {
                if (!fl26DebtDueField.IsInited)
                    fl26DebtDueField.Value = (FL_25_26_27_28_Group_TypeFL_26_DebtDue?)Items.GetObject(ItemsElementName, "FL_26_DebtDue");
                return fl26DebtDueField.Value;
            }
        }
    }
}