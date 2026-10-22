using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_15_ContractChanges_Type : Block_Type
    {
        public UL_15_ContractChanges_Type()
        {
            blockName = refBlockNameType.UL_15_ContractChanges;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IUL_10_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (exist == true)
            {
                if (changeDate.DateLess(dealUidBlock?.openDate))
                {
                    AddError("changeDate", changeDate, "Дата изменения договора ЮЛ_15.2 раньше даты совершения сделки ЮЛ_10.4");
                }
                if ((code == 1 && !specialCode.In(Enumerable.Range(1, 9).Select(x => (ushort?)x).ToArray()))
                    || (code == 2 && !specialCode.In((ushort?)7, (ushort?)99))
                    || (code == 3 && !specialCode.In(Enumerable.Range(1, 22).Concat([99]).Select(x => (ushort?)x).ToArray())))
                {
                    AddError("specialCode", specialCode, "Неверное значение кода специального изменения договора ЮЛ_15.4");
                }


                if (specialCode == 99 && otherDesc == null)
                {
                    AddError("otherDesc", otherDesc, "Отсутствует описание иного изменения договора ЮЛ_15.5");
                }

                if (startDate.DateLess(dealUidBlock?.openDate))
                {
                    AddError("startDate", startDate, "Дата вступления изменения договора в силу ЮЛ_15.6 раньше даты совершения сделки ЮЛ_10.4");
                }

                if (endDate.DateLess(changeDate))
                {
                    AddError("endDate", endDate, "Дата планового прекращения действия изменения договора ЮЛ_15.7 раньше даты изменения договора ЮЛ_15.2");
                }
                if (endDate.DateLess(startDate))
                {
                    AddError("endDate", endDate, "Дата планового прекращения действия изменения договора ЮЛ_15.7 раньше даты вступления изменения договора в силу ЮЛ_15.6");
                }

                if (actualEndDate.DateGreater(eventData?.eventDate))
                {
                    AddError("actualEndDate", actualEndDate, "Дата фактического прекращения действия изменения договора ЮЛ_15.8 позднее даты наступления события eventDate");
                }
                if (actualEndDate.DateLess(startDate))
                {
                    AddError("actualEndDate", actualEndDate, "Дата фактического прекращения действия изменения договора ЮЛ_15.8 раньше даты вступления изменения договора в силу ЮЛ_15.6");
                }
                if (actualEndDate.DateGreater(endDate))
                {
                    AddError("actualEndDate", actualEndDate, "Дата фактического прекращения действия изменения договора ЮЛ_15.8 позднее даты планового прекращения действия изменения договора ЮЛ_15.7");
                }
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_15_ContractChanges_Type block, EventData eventData, IUL_10_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}
