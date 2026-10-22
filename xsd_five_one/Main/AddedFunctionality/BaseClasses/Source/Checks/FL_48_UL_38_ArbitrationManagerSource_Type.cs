using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_48_UL_38_ArbitrationManagerSource_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_48_UL_38_ArbitrationManagerSource;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IDocument document)
        {
            ErrorData errorData = new(blockName);

            if (sourceTerminationDate.DateLess(sourceAcceptDate))
            {
                errorData.AddError("sourceTerminationDate", sourceTerminationDate, "Дата прекращения полномочий арбитражного управляющего ФЛ_48.9 (ЮЛ_38.9) раньше даты утверждения арбитражного управляющего ФЛ_48.8 (ЮЛ_38.8)");
            }

            foreach (var group in TaxNum_group_FL_48_UL_38_ArbitrationManagerSource.OrEmptyIfNull())
            {
                if (!group.taxNum.DigitCheck(false, 12))
                {
                    errorData.AddError("taxNum", group.taxNum, $"Длина номера налогоплательщика ФЛ_48.11 (ЮЛ_38.11) не равна 12 цифрам");
                }
            }

            if (!SnilsHelper.CheckSnils(socialNum))
            {
                errorData.AddError("socialNum", socialNum, $"В СНИЛСе ФЛ_48.12 (ЮЛ_38.12) неверный контрольный символ");
            }

            if (sourceCreditInfoDate.Date != document.dateTimeDocLocal.Date)
            {
                errorData.AddError("sourceCreditInfoDate", sourceCreditInfoDate, "Дата формирования кредитной информации ФЛ_48.13 (ЮЛ_38.13) не равна исходящей дате и времени регистрации документа, содержащего кредитную информацию dateTimeDoc");
            }

            return errorData.ErrorList;
        }
    }
}
