using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class FL_23_1_ContractTermsChanges_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.FL_23_1_ContractTermsChanges;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (changeFact == true)
            {
                //if (termsChangeCode?.In(refContractOtherChangeType.Item23, refContractOtherChangeType.Item24) == true)
                //{
                //    errorData.AddError("termsChangeCode", termsChangeCode, "Код вида изменения условий обязательства ФЛ_23(1).2 равен 23 или 24, данные коды вводятся в действие с 01.07.2026");
                //}

                if (termsChangeCode == refContractOtherChangeType.Item99 && termsChangeDesc == null)
                {
                    errorData.AddError("termsChangeDesc", termsChangeDesc, "Отсутствует описание иного изменения условий обязательства ФЛ_23(1).3");
                }

                if (changingDate.DateLess(dealUidBlock?.openDate))
                {
                    errorData.AddError("changingDate", changingDate, "Дата начала действия изменения условий обязательства ФЛ_23(1).4 раньше даты совершения сделки ФЛ_17.4");
                }
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_23_1_ContractTermsChanges_Type block, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<FL_23_1_ContractTermsChanges_Type> blocks, IEventData eventData, I_FL_17_DealUid_Type? dealUidBlock = null)
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
