using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_46_UL_36_OrgSource_Type : Block_Type
    {
        public FL_46_UL_36_OrgSource_Type()
        {
            blockName = refBlockNameType.FL_46_UL_36_OrgSource;
        }

        public List<INotificationOfAcceptanceError> Check(IDocument document)
        {
            InitCheck(null);

            if (sourceRegistrationFact == true && !regNum.DigitCheck(true, 13))
            {
                AddError("regNum", regNum, $"Длина регистрационного номера ФЛ_46.8 (ЮЛ_36.8) не равна 13 цифрам");
            }

            foreach (var group in TaxNum_group_FL_46_UL_36_OrgSource.OrEmptyIfNull())
            {
                if (group.taxCode == 1 && !group.taxNum.DigitCheck(false, 10))
                {
                    AddError("taxNum", group.taxNum, $"Длина номера налогоплательщика ФЛ_46.10 (ЮЛ_36.10) не равна 10 цифрам");
                }
            }

            if (beginDateSourceBankruptcy.DateLess(sourceDateStart))
            {
                AddError("beginDateSourceBankruptcy", beginDateSourceBankruptcy, "Дата признания источника банкротом ФЛ_46.11 (ЮЛ_36.11) раньше даты создания источника ФЛ_46.7 (ЮЛ_36.7)");
            }

            if (endDateSourceBankruptcy.DateLess(beginDateSourceBankruptcy))
            {
                AddError("endDateSourceBankruptcy", endDateSourceBankruptcy, "Дата окончания конкурсного производства ФЛ_46.12 (ЮЛ_36.12) раньше даты признания источника банкротом ФЛ_46.11 (ЮЛ_36.11)");
            }

            if (endDateSourceLiquidation.DateLess(beginDateSourceLiquidation))
            {
                AddError("endDateSourceLiquidation", endDateSourceLiquidation, "Дата окончания ликвидации источника ФЛ_46.14 (ЮЛ_36.14) раньше даты начала ликвидации источника ФЛ_46.13 (ЮЛ_36.13)");
            }

            return errorList;
        }
    }
}
