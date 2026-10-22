using loader_utils;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_15_1_ContractTermsChanges_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_15_1_ContractTermsChanges;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock)
        {
            ErrorData errorData = new(blockName, eventData);

            if (changeFact == true)
            {
                if (termsChangeCode == refContractOtherChangeType.Item99 && termsChangeDesc == null)
                {
                    errorData.AddError("termsChangeDesc", termsChangeDesc, "Отсутствует описание иного изменения условий обязательства ЮЛ_15(1).3");
                }

                if (changingDate.DateLess(dealUidBlock?.openDate))
                {
                    errorData.AddError("changingDate", changingDate, "Дата начала действия изменения условий обязательства ЮЛ_15(1).4 раньше даты совершения сделки ЮЛ_10.4");
                }
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_15_1_ContractTermsChanges_Type block, IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock = null)
        {
            return [.. (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<UL_15_1_ContractTermsChanges_Type> blocks, IEventData eventData, I_UL_10_DealUid_Type? dealUidBlock = null)
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
