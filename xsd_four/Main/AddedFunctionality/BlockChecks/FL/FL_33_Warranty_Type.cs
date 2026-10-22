using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class FL_33_Warranty_Type : Block_Type
    {
        public FL_33_Warranty_Type()
        {
            blockName = refBlockNameType.FL_33_Warranty;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData)
        {
            InitCheck(eventData);

            if (warrantyFact == true)
            {
                if (!UidHelper.CheckUid(uid))
                {
                    AddError("uid", uid, "Неверный контрольный символ УИда договора поручительства ФЛ_33.2");
                }
                if (!CurrencyDictionary.Contains(currency))
                {
                    AddError("currency", currency, "Валюта поручительства ФЛ_33.4 не соответствует  Общероссийскому классификатору валют");
                }
                if (openDate.DateGreater(eventData?.eventDate))
                {
                    AddError("openDate", openDate, "Дата заключения договора поручительства ФЛ_33.5 позднее даты наступления события eventDate");
                }
                if (endDate.DateLess(openDate))
                {
                    AddError("endDate", endDate, "Дата прекращения поручительства согласно договору ФЛ_33.6 раньше даты заключения договора поручительства ФЛ_33.5");
                }

                if (factEndDate.DateGreater(eventData?.eventDate))
                {
                    AddError("factEndDate", factEndDate, "Дата фактического прекращения поручительства ФЛ_33.7 позднее даты наступления события eventDate");
                }
                if (factEndDate.DateLess(openDate))
                {
                    AddError("factEndDate", factEndDate, "Дата фактического прекращения поручительства ФЛ_33.7 раньше даты заключения договора поручительства ФЛ_33.5");
                }
            }
            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this FL_33_Warranty_Type block, EventData eventData)
        {
            return (block?.Check(eventData)).OrEmptyIfNull().ToList();
        }
    }
}
