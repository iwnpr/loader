using loader_utils;

namespace Xsd.VersionFour.Main
{
    public partial class FL_48_UL_38_ArbitrationManagerSource_Type : Block_Type
    {
        public FL_48_UL_38_ArbitrationManagerSource_Type()
        {
            blockName = refBlockNameType.FL_48_UL_38_ArbitrationManagerSource;
        }

        public List<INotificationOfAcceptanceError> Check(IDocument document)
        {
            InitCheck(null);

            if (sourceTerminationDate.DateLess(sourceAcceptDate))
            {
                AddError("sourceTerminationDate", sourceTerminationDate, "Дата прекращения полномочий арбитражного управляющего ФЛ_48.9 (ЮЛ_38.9) раньше даты утверждения арбитражного управляющего ФЛ_48.8 (ЮЛ_38.8)");
            }

            foreach (var group in TaxNum_group_FL_48_UL_38_ArbitrationManagerSource.OrEmptyIfNull())
            {
                if (!group.taxNum.DigitCheck(false, 12))
                {
                    AddError("taxNum", group.taxNum, $"Длина номера налогоплательщика ФЛ_48.11 (ЮЛ_38.11) не равна 12 цифрам");
                }
            }

            if (!SnilsHelper.CheckSnils(socialNum))
            {
                AddError("socialNum", socialNum, $"В СНИЛСе ФЛ_48.12 (ЮЛ_38.12) неверный контрольный символ");
            }

            return errorList;
        }
    }
}
