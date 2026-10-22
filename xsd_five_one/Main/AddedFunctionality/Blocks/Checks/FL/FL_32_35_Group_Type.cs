using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_32_35_Group_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_32_35_Group;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (assetKind == true)
            {
                #region FL_32_Collateral
                if (FL_32_Collateral!.startDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("date", FL_32_Collateral.startDate, "Дата заключения договора залога ФЛ_32.4 позднее даты и времени наступления события eventDateTime");
                }
                if (FL_32_Collateral.code != refCollateralType.Item194 && !FL_32_Collateral.Sum_group_FL_32_35_Group.OrEmptyIfNull().Any())
                {
                    errorData.AddError("sum", null, "Отсутствует стоимость предмета залога ФЛ_32.5");
                }
                foreach (var sumGroup in FL_32_Collateral.Sum_group_FL_32_35_Group.OrEmptyIfNull())
                {
                    if (!CurrencyDictionary.Contains(sumGroup.currency))
                    {
                        errorData.AddError("currency", sumGroup.currency, "Валюта стоимости предмета залога ФЛ_32.6 не соответствует  Общероссийскому классификатору валют");
                    }
                    if (sumGroup.assessDate.DateGreater(eventData.eventDateTimeLocal))
                    {
                        errorData.AddError("assessDate", sumGroup.assessDate, "Дата проведения оценки предмета залога ФЛ_32.7 позднее даты и времени наступления события eventDateTime");
                    }
                }

                if (FL_32_Collateral.collateralEndDate.DateLess(FL_32_Collateral.startDate))
                {
                    errorData.AddError("collateralEndDate", FL_32_Collateral.collateralEndDate, "Дата прекращения залога согласно договору ФЛ_32.9 раньше даты заключения договора залога ФЛ_32.4");
                }
                if (FL_32_Collateral.collateralEndDate.DateLess(dealUidBlock?.openDate))
                {
                    errorData.AddError("collateralEndDate", FL_32_Collateral.collateralEndDate, "Дата прекращения залога согласно договору ФЛ_32.9 раньше даты совершения сделки ФЛ_17.4");
                }

                if (FL_32_Collateral.collateralFactEndDate != DateTime.MinValue)
                {
                    if (FL_32_Collateral.collateralFactEndDate.DateGreater(eventData.eventDateTimeLocal))
                    {
                        errorData.AddError("collateralFactEndDate", FL_32_Collateral.collateralFactEndDate, "Дата фактического прекращения залога ФЛ_32.10 позднее даты и времени наступления события eventDateTime");
                    }
                    if (FL_32_Collateral.collateralFactEndDate.DateLess(FL_32_Collateral.startDate))
                    {
                        errorData.AddError("collateralFactEndDate", FL_32_Collateral.collateralFactEndDate, "Дата фактического прекращения залога ФЛ_32.10 раньше даты заключения договора залога ФЛ_32.4");
                    }
                }

                //TODO ОКАТО
                if (FL_32_Collateral.Sum_group_FL_32_35_Group.OrEmptyIfNull().Any(x => FL_32_Collateral.calcDate.DateLess(x.assessDate)))
                {
                    errorData.AddError("calcDate", FL_32_Collateral.calcDate, "Дата расчета актуальной стоимости предмета залога ФЛ_32.17 раньше даты проведения оценки предмета залога ФЛ_32.7");
                }
                #endregion

                #region FL_35_Insurance
                foreach (var insurance in FL_35_Insurance.OrEmptyIfNull())
                {
                    if (insurance.exist == true)
                    {
                        if (insurance.insuranceEndDate.DateLess(insurance.startDate))
                        {
                            errorData.AddError("insuranceEndDate", insurance.insuranceEndDate, "Дата окончания действия страхования согласно договору ФЛ_35.6 раньше даты начала действия страхования ФЛ_35.5");
                        }
                        if (insurance.insuranceFactEndDate != null)
                        {
                            if (insurance.insuranceFactEndDate.DateLess(insurance.startDate))
                            {
                                errorData.AddError("insuranceFactEndDate", insurance.insuranceFactEndDate, "Дата фактического прекращения страхования ФЛ_35.7 раньше даты начала действия страхования ФЛ_35.5");
                            }
                            if (insurance.insuranceFactEndDate.DateGreater(eventData.eventDateTimeLocal))
                            {
                                errorData.AddError("insuranceFactEndDate", insurance.insuranceFactEndDate, "Дата фактического прекращения страхования ФЛ_35.7 позднее даты и времени наступления события eventDateTime");
                            }
                        }
                    }
                }
                #endregion
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_32_35_Group_Type block, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_32_35_Group_Type> blocks, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            List<INotificationOfAcceptanceError> errorList = [];

            foreach (var block in blocks.OrEmptyIfNull())
            {
                errorList.AddRange(block.Check(eventData, dealUidBlock));
            }
            foreach (var propertyId in blocks.OrEmptyIfNull().GroupBy(x => x.propertyId).Where(x => x.Count() > 1).Select(x => x.Key))
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.FL_32_35_Group, "propertyId", propertyId, "Идентификационный код предмета залога ФЛ_32.3, 35.9 не уникален в рамках одного обязательства"));
            }
            return errorList;
        }
    }
}
