using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_23_26_Group_Type : Block_Type
    {
        public UL_23_26_Group_Type()
        {
            blockName = refBlockNameType.UL_23_26_Group;
        }
        public List<INotificationOfAcceptanceError> Check(EventData eventData, IUL_10_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (assetKind == true)
            {
                #region UL_23_Collateral
                if (UL_23_Collateral!.date.DateGreater(eventData?.eventDate))
                {
                    AddError("date", UL_23_Collateral!.date, "Дата заключения договора залога ЮЛ_23.4 позднее даты наступления события eventDate");
                }
                if (UL_23_Collateral!.code != refCollateralType.Item194 && UL_23_Collateral!.Sum_group_UL_23_26_Group.OrEmptyIfNull().Count() == 0)
                {
                    AddError("sum", null, "Отсутствует стоимость предмета залога ЮЛ_23.5");
                }
                foreach (var sumGroup in UL_23_Collateral!.Sum_group_UL_23_26_Group.OrEmptyIfNull())
                {
                    if (!CurrencyDictionary.Contains(sumGroup.currency))
                    {
                        AddError("currency", sumGroup.currency, "Валюта стоимости предмета залога ЮЛ_23.6 не соответствует  Общероссийскому классификатору валют");
                    }
                    if (sumGroup.assessDate.DateGreater(eventData?.eventDate))
                    {
                        AddError("assessDate", sumGroup.assessDate, "Дата проведения оценки предмета залога ЮЛ_23.7 позднее даты наступления события eventDate");
                    }
                }

                if (UL_23_Collateral!.collateralEndDate < DateTime.Parse(UL_23_Collateral!.date))
                {
                    AddError("collateralEndDate", UL_23_Collateral!.collateralEndDate, "Дата прекращения залога согласно договору ЮЛ_23.9 раньше даты заключения договора залога ЮЛ_23.4");
                }
                if (UL_23_Collateral!.collateralEndDate.DateLess(dealUidBlock?.openDate))
                {
                    AddError("collateralEndDate", UL_23_Collateral!.collateralEndDate, "Дата прекращения залога согласно договору ЮЛ_23.9 раньше даты совершения сделки ЮЛ_10.4");
                }

                if (UL_23_Collateral!.collateralFactEndDate != DateTime.MinValue)
                {
                    if (UL_23_Collateral!.collateralFactEndDate.DateGreater(eventData?.eventDate))
                    {
                        AddError("collateralFactEndDate", UL_23_Collateral!.collateralFactEndDate, "Дата фактического прекращения залога ЮЛ_23.10 позднее даты наступления события eventDate");
                    }
                    if (UL_23_Collateral!.collateralFactEndDate.DateLess(UL_23_Collateral!.date))
                    {
                        AddError("collateralFactEndDate", UL_23_Collateral!.collateralFactEndDate, "Дата фактического прекращения залога ЮЛ_23.10 раньше даты заключения договора залога ЮЛ_23.4");
                    }
                }

                //TODO ОКАТО
                if (UL_23_Collateral!.Sum_group_UL_23_26_Group.OrEmptyIfNull().Any(x => UL_23_Collateral!.calcDate.DateLess(x.assessDate)))
                {
                    AddError("calcDate", UL_23_Collateral!.calcDate, "Дата расчета актуальной стоимости предмета залога ЮЛ_23.17 раньше даты проведения оценки предмета залога ЮЛ_23.7");
                }
                #endregion

                #region UL_26_Insurance
                foreach (var insurance in UL_26_Insurance.OrEmptyIfNull())
                {
                    if (insurance.exist == true)
                    {
                        if (insurance.insuranceEndDate.DateLess(insurance.startDate))
                        {
                            AddError("insuranceEndDate", insurance.insuranceEndDate, "Дата окончания действия страхования согласно договору ЮЛ_26.6 раньше даты начала действия страхования ЮЛ_26.5");
                        }
                        if (insurance.insuranceFactEndDate != null)
                        {
                            if (insurance.insuranceFactEndDate.DateLess(insurance.startDate))
                            {
                                AddError("insuranceFactEndDate", insurance.insuranceFactEndDate, "Дата фактического прекращения страхования ЮЛ_26.7 раньше даты начала действия страхования ЮЛ_26.5");
                            }
                            if (insurance.insuranceFactEndDate.DateGreater(eventData?.eventDate))
                            {
                                AddError("insuranceFactEndDate", insurance.insuranceFactEndDate, "Дата фактического прекращения страхования ЮЛ_26.7 позднее даты наступления события eventDate");
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
        public static List<INotificationOfAcceptanceError> Validate(this UL_23_26_Group_Type[] blocks, EventData eventData, IUL_10_DealUid dealUidBlock = null)
        {
            var errorList = new List<INotificationOfAcceptanceError>();

            foreach (var block in blocks)
            {
                errorList.AddRange(block.Check(eventData, dealUidBlock));
            }
            foreach (var propertyId in blocks.OrEmptyIfNull().GroupBy(x => x.propertyId).Where(x => x.Count() > 1).Select(x => x.Key))
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_23_26_Group, "propertyId", propertyId, "Идентификационный код предмета залога ЮЛ_23.3, 26.9 не уникален в рамках одного обязательства"));
            }
            return errorList;
        }

        public static List<INotificationOfAcceptanceError> Validate(this UL_23_26_Group_Type block, EventData eventData, IUL_10_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}
