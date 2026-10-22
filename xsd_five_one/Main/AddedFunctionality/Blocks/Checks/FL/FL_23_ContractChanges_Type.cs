using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_23_ContractChanges_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_23_ContractChanges;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (exist == true)
            {
                if (changeDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("changeDate", changeDate, "Дата изменения договора ФЛ_23.2 позднее даты и времени наступления события eventDateTime");
                }
                if (changeDate.DateLess(dealUidBlock?.openDate))
                {
                    errorData.AddError("changeDate", changeDate, "Дата изменения договора ФЛ_23.2 раньше даты совершения сделки ФЛ_17.4");
                }

                //if (specialCode?.In(refSpecialContractChangeType.Item23, refSpecialContractChangeType.Item24) == true)
                //{
                //    errorData.AddError("specialCode", specialCode, "Код специального изменения договора ФЛ_23.4 равен 23 или 24, данные коды вводятся в действие с 01.07.2026");
                //}

                if ((code == refContractChangeType.Item1 && (specialCode == null || !specialCode.In(refSpecialContractChangeType.Item1,
                                                                                                    refSpecialContractChangeType.Item2,
                                                                                                    refSpecialContractChangeType.Item3,
                                                                                                    refSpecialContractChangeType.Item4,
                                                                                                    refSpecialContractChangeType.Item5,
                                                                                                    refSpecialContractChangeType.Item6,
                                                                                                    refSpecialContractChangeType.Item7,
                                                                                                    refSpecialContractChangeType.Item8,
                                                                                                    refSpecialContractChangeType.Item9)))
                    || (code == refContractChangeType.Item2 && (specialCode == null || !specialCode.In(refSpecialContractChangeType.Item1,
                                                                                                    refSpecialContractChangeType.Item2,
                                                                                                    refSpecialContractChangeType.Item3,
                                                                                                    refSpecialContractChangeType.Item4,
                                                                                                    refSpecialContractChangeType.Item5,
                                                                                                    refSpecialContractChangeType.Item6,
                                                                                                    refSpecialContractChangeType.Item7,
                                                                                                    refSpecialContractChangeType.Item8,
                                                                                                    refSpecialContractChangeType.Item99)))
                    || (code == refContractChangeType.Item3 && (specialCode == null || !specialCode.In(refSpecialContractChangeType.Item1,
                                                                                                    refSpecialContractChangeType.Item2,
                                                                                                    refSpecialContractChangeType.Item3,
                                                                                                    refSpecialContractChangeType.Item4,
                                                                                                    refSpecialContractChangeType.Item5,
                                                                                                    refSpecialContractChangeType.Item6,
                                                                                                    refSpecialContractChangeType.Item7,
                                                                                                    refSpecialContractChangeType.Item8,
                                                                                                    refSpecialContractChangeType.Item9,
                                                                                                    refSpecialContractChangeType.Item10,
                                                                                                    refSpecialContractChangeType.Item11,
                                                                                                    refSpecialContractChangeType.Item12,
                                                                                                    refSpecialContractChangeType.Item13,
                                                                                                    refSpecialContractChangeType.Item14,
                                                                                                    refSpecialContractChangeType.Item15,
                                                                                                    refSpecialContractChangeType.Item16,
                                                                                                    refSpecialContractChangeType.Item17,
                                                                                                    refSpecialContractChangeType.Item18,
                                                                                                    refSpecialContractChangeType.Item19,
                                                                                                    refSpecialContractChangeType.Item20,
                                                                                                    refSpecialContractChangeType.Item21,
                                                                                                    refSpecialContractChangeType.Item22,
                                                                                                    refSpecialContractChangeType.Item23,
                                                                                                    refSpecialContractChangeType.Item24,
                                                                                                    refSpecialContractChangeType.Item99))))
                {
                    errorData.AddError("specialCode", specialCode, "Неверное значение кода специального изменения договора ФЛ_23.4");
                }


                if (specialCode == refSpecialContractChangeType.Item99 && otherDesc == null)
                {
                    errorData.AddError("otherDesc", otherDesc, "Отсутствует описание иного изменения договора ФЛ_23.5");
                }

                if (startDate.DateLess(dealUidBlock?.openDate))
                {
                    errorData.AddError("startDate", startDate, "Дата вступления изменения договора в силу ФЛ_23.6 раньше даты совершения сделки ФЛ_17.4");
                }

                if (endDate.DateLess(changeDate))
                {
                    errorData.AddError("endDate", endDate, "Дата планового прекращения действия изменения договора ФЛ_23.7 раньше даты изменения договора ФЛ_23.2");
                }
                if (endDate.DateLess(startDate))
                {
                    errorData.AddError("endDate", endDate, "Дата планового прекращения действия изменения договора ФЛ_23.7 раньше даты вступления изменения договора в силу ФЛ_23.6");
                }

                if (actualEndDate.DateGreater(eventData.eventDateTimeLocal))
                {
                    errorData.AddError("actualEndDate", actualEndDate, "Дата фактического прекращения действия изменения договора ФЛ_23.8 позднее даты и времени наступления события eventDateTime");
                }
                if (actualEndDate.DateLess(startDate))
                {
                    errorData.AddError("actualEndDate", actualEndDate, "Дата фактического прекращения действия изменения договора ФЛ_23.8 раньше даты вступления изменения договора в силу ФЛ_23.6");
                }
                if (actualEndDate.DateGreater(endDate))
                {
                    errorData.AddError("actualEndDate", actualEndDate, "Дата фактического прекращения действия изменения договора ФЛ_23.8 позднее даты планового прекращения действия изменения договора ФЛ_23.7");
                }
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_23_ContractChanges_Type block, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_23_ContractChanges_Type> blocks, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            List<INotificationOfAcceptanceError> errorList = [];
            foreach (var block in blocks.OrEmptyIfNull())
            {
                errorList.AddRange(block.Check(eventData, dealUidBlock));
            }
            return errorList;
        }
    }
}
