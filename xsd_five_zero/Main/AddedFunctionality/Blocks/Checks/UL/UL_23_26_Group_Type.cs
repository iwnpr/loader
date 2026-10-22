using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_23_26_Group_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_23_26_Group;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (assetKind == true)
            {
                #region UL_23_Collateral
                if (UL_23_Collateral!.startDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("date", UL_23_Collateral.startDate, "Дата заключения договора залога ЮЛ_23.4 позднее даты и времени наступления события eventDateTime");
                }
                if (UL_23_Collateral.code != refCollateralType.Item194 && !UL_23_Collateral.Sum_group_UL_23_26_Group.OrEmptyIfNull().Any())
                {
                    errorData.AddError("sum", null, "Отсутствует стоимость предмета залога ЮЛ_23.5");
                }
                foreach (var sumGroup in UL_23_Collateral.Sum_group_UL_23_26_Group.OrEmptyIfNull())
                {
                    if (!CurrencyDictionary.Contains(sumGroup.currency))
                    {
                        errorData.AddError("currency", sumGroup.currency, "Валюта стоимости предмета залога ЮЛ_23.6 не соответствует  Общероссийскому классификатору валют");
                    }
                    if (sumGroup.assessDate.DateGreater(eventData.eventDateTimeLocal))
                    {
                        errorData.AddError("assessDate", sumGroup.assessDate, "Дата проведения оценки предмета залога ЮЛ_23.7 позднее даты и времени наступления события eventDateTime");
                    }
                }

                if (UL_23_Collateral.collateralEndDate.DateLess(UL_23_Collateral.startDate))
                {
                    errorData.AddError("collateralEndDate", UL_23_Collateral.collateralEndDate, "Дата прекращения залога согласно договору ЮЛ_23.9 раньше даты заключения договора залога ЮЛ_23.4");
                }
                if (UL_23_Collateral.collateralEndDate.DateLess(dealUidBlock?.openDate))
                {
                    errorData.AddError("collateralEndDate", UL_23_Collateral.collateralEndDate, "Дата прекращения залога согласно договору ЮЛ_23.9 раньше даты совершения сделки ЮЛ_10.4");
                }


                if (UL_23_Collateral.collateralFactEndDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("collateralFactEndDate", UL_23_Collateral.collateralFactEndDate, "Дата фактического прекращения залога ЮЛ_23.10 позднее даты и времени наступления события eventDateTime");
                }
                if (UL_23_Collateral.collateralFactEndDate.DateLess(UL_23_Collateral.startDate))
                {
                    errorData.AddError("collateralFactEndDate", UL_23_Collateral.collateralFactEndDate, "Дата фактического прекращения залога ЮЛ_23.10 раньше даты заключения договора залога ЮЛ_23.4");
                }


                //TODO ОКАТО
                if (UL_23_Collateral.Sum_group_UL_23_26_Group.OrEmptyIfNull().Any(x => UL_23_Collateral.calcDate.DateLess(x.assessDate)))
                {
                    errorData.AddError("calcDate", UL_23_Collateral.calcDate, "Дата расчета актуальной стоимости предмета залога ЮЛ_23.17 раньше даты проведения оценки предмета залога ЮЛ_23.7");
                }


                if (UL_23_Collateral.actualPriceCodeSpecified)
                {
                    errorData.AddError("actualPriceCode", UL_23_Collateral.actualPriceCode, "Вид актуальной стоимости предмета залога ФЛ_32.18 должен отсутствовать  (вводится в действие с 01.07.2026)");
                }

                if (UL_23_Collateral.acquiredExist != null)
                {
                    errorData.AddError("acquiredExist_0/acquiredExist_1", null, "Признак залога приобретаемого объекта по сделке ФЛ_32.19 должен отсутствовать (вводится в действие с 01.07.2026)");
                }

                if (UL_23_Collateral.inhabitedExist != null)
                {
                    errorData.AddError("inhabitedExist_0/inhabitedExist_1", null, "Признак залога жилой недвижимости ФЛ_32.20 должен отсутствовать (вводится в действие с 01.07.2026)");
                }
                #endregion

                #region UL_26_Insurance
                foreach (var insurance in UL_26_Insurance.OrEmptyIfNull())
                {
                    if (insurance.exist == true)
                    {
                        if (insurance.insuranceEndDate.DateLess(insurance.startDate))
                        {
                            errorData.AddError("insuranceEndDate", insurance.insuranceEndDate, "Дата окончания действия страхования согласно договору ЮЛ_26.6 раньше даты начала действия страхования ЮЛ_26.5");
                        }
                        if (insurance.insuranceFactEndDate != null)
                        {
                            if (insurance.insuranceFactEndDate.DateLess(insurance.startDate))
                            {
                                errorData.AddError("insuranceFactEndDate", insurance.insuranceFactEndDate, "Дата фактического прекращения страхования ЮЛ_26.7 раньше даты начала действия страхования ЮЛ_26.5");
                            }
                            if (insurance.insuranceFactEndDate.DateGreater(eventData.eventDateTimeLocal))
                            {
                                errorData.AddError("insuranceFactEndDate", insurance.insuranceFactEndDate, "Дата фактического прекращения страхования ЮЛ_26.7 позднее даты и времени наступления события eventDateTime");
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
        public static List<INotificationOfAcceptanceError> Validate(this UL_23_26_Group_Type block, IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<UL_23_26_Group_Type> blocks, IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock = null)
        {
            List<INotificationOfAcceptanceError> errorList = [];

            foreach (var block in blocks.OrEmptyIfNull())
            {
                errorList.AddRange(block.Check(eventData, dealUidBlock));
            }
            foreach (var propertyId in blocks.OrEmptyIfNull().GroupBy(x => x.propertyId).Where(x => x.Count() > 1).Select(x => x.Key))
            {
                errorList.Add(new Acceptance.NotificationOfAcceptanceError(eventData, refBlockNameType.UL_23_26_Group, "propertyId", propertyId, "Идентификационный код предмета залога ЮЛ_23.3, 26.9 не уникален в рамках одного обязательства"));
            }
            return errorList;
        }
    }
}
