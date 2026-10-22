using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_23_1_ContractTermsChanges_Type : Block_Type
    {
        public FL_23_1_ContractTermsChanges_Type()
        {
            blockName = refBlockNameType.FL_23_1_ContractTermsChanges;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            InitCheck(eventData);

            if (changeFact == true)
            {
                if (termsChangeCode == 99 && termsChangeDesc == null)
                {
                    AddError("termsChangeDesc", termsChangeDesc, "Отсутствует описание иного изменения условий обязательства ФЛ_23(1).3");
                }

                if (changingDate.DateLess(dealUidBlock?.openDate))
                {
                    AddError("changingDate", changingDate, "Дата начала действия изменения условий обязательства ФЛ_23(1).4 раньше даты совершения сделки ФЛ_17.4");
                }
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_23_1_ContractTermsChanges_Type block, EventData eventData, IFL_17_DealUid dealUidBlock = null)
        {
            return (block?.Check(eventData, dealUidBlock)).OrEmptyIfNull().ToList();
        }
    }
}
