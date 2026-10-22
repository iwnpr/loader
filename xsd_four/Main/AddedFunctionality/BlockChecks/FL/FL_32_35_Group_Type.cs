using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_32_35_Group_Type : Block_Type
    {
        public FL_32_35_Group_Type()
        {
            blockName = refBlockNameType.FL_32_35_Group;
        }
        public List<INotificationOfAcceptanceError> Check(EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (assetKind == true)
            {
                #region FL_32_Collateral
                if (FL_32_Collateral!.date.DateGreater(eventData?.eventDate))
                {
                    AddError("date", FL_32_Collateral!.date, "Дата заключения договора залога ФЛ_32.4 позднее даты наступления события eventDate");
                }
                if (FL_32_Collateral!.code != refCollateralType.Item194 && FL_32_Collateral!.Sum_group_FL_32_35_Group.OrEmptyIfNull().Count() == 0)
                {
                    AddError("sum", null, "Отсутствует стоимость предмета залога ФЛ_32.5");
                }
                foreach (var sumGroup in FL_32_Collateral!.Sum_group_FL_32_35_Group.OrEmptyIfNull())
                {
                    if (!CurrencyDictionary.Contains(sumGroup.currency))
                    {
                        AddError("currency", sumGroup.currency, "Валюта стоимости предмета залога ФЛ_32.6 не соответствует  Общероссийскому классификатору валют");
                    }
                    if (sumGroup.assessDate.DateGreater(eventData?.eventDate))
                    {
                        AddError("assessDate", sumGroup.assessDate, "Дата проведения оценки предмета залога ФЛ_32.7 позднее даты наступления события eventDate");
                    }
                }

                if (FL_32_Collateral!.collateralEndDate < DateTime.Parse(FL_32_Collateral!.date))
                {
                    AddError("collateralEndDate", FL_32_Collateral!.collateralEndDate, "Дата прекращения залога согласно договору ФЛ_32.9 раньше даты заключения договора залога ФЛ_32.4");
                }
                if (FL_32_Collateral!.collateralEndDate.DateLess(dealUidBlock?.openDate))
                {
                    AddError("collateralEndDate", FL_32_Collateral!.collateralEndDate, "Дата прекращения залога согласно договору ФЛ_32.9 раньше даты совершения сделки ФЛ_17.4");
                }

                if (FL_32_Collateral!.collateralFactEndDate != DateTime.MinValue)
                {
                    if (FL_32_Collateral!.collateralFactEndDate.DateGreater(eventData?.eventDate))
                    {
                        AddError("collateralFactEndDate", FL_32_Collateral!.collateralFactEndDate, "Дата фактического прекращения залога ФЛ_32.10 позднее даты наступления события eventDate");
                    }
                    if (FL_32_Collateral!.collateralFactEndDate.DateLess(FL_32_Collateral!.date))
                    {
                        AddError("collateralFactEndDate", FL_32_Collateral!.collateralFactEndDate, "Дата фактического прекращения залога ФЛ_32.10 раньше даты заключения договора залога ФЛ_32.4");
                    }
                }

                //TODO ОКАТО
                if (FL_32_Collateral!.Sum_group_FL_32_35_Group.OrEmptyIfNull().Any(x => FL_32_Collateral!.calcDate.DateLess(x.assessDate)))
                {
                    AddError("calcDate", FL_32_Collateral!.calcDate, "Дата расчета актуальной стоимости предмета залога ФЛ_32.17 раньше даты проведения оценки предмета залога ФЛ_32.7");
                }
                #endregion

                #region FL_35_Insurance
                foreach (var insurance in FL_35_Insurance.OrEmptyIfNull())
                {
                    if (insurance.exist == true)
                    {
                        if (insurance.insuranceEndDate.DateLess(insurance.startDate))
                        {
                            AddError("insuranceEndDate", insurance.insuranceEndDate, "Дата окончания действия страхования согласно договору ФЛ_35.6 раньше даты начала действия страхования ФЛ_35.5");
                        }
                        if (insurance.insuranceFactEndDate != null)
                        {
                            if (insurance.insuranceFactEndDate.DateLess(insurance.startDate))
                            {
                                AddError("insuranceFactEndDate", insurance.insuranceFactEndDate, "Дата фактического прекращения страхования ФЛ_35.7 раньше даты начала действия страхования ФЛ_35.5");
                            }
                            if (insurance.insuranceFactEndDate.DateGreater(eventData?.eventDate))
                            {
                                AddError("insuranceFactEndDate", insurance.insuranceFactEndDate, "Дата фактического прекращения страхования ФЛ_35.7 позднее даты наступления события eventDate");
                            }
                        }
                    }
                }
                #endregion
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_32_35_Group_Type[] blocks, EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            var errorList = new List<INotificationOfAcceptanceError>();

            foreach (var block in blocks)
            {
                errorList.AddRange(block.Check(eventData, dealUidBlock));
            }
            foreach (var propertyId in blocks.OrEmptyIfNull().GroupBy(x => x.propertyId).Where(x => x.Count() > 1).Select(x => x.Key))
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_32_35_Group, "propertyId", propertyId, "Идентификационный код предмета залога ФЛ_32.3, 35.9 не уникален в рамках одного обязательства"));
            }
            return errorList;
        }

        public static List<INotificationOfAcceptanceError> Validate(this FL_32_35_Group_Type block, EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}
