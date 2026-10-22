using loader_utils;


namespace Xsd.VersionFour.Main
{
    public partial class UL_5_Reorg_Type : Block_Type
    {
        public UL_5_Reorg_Type()
        {
            blockName = refBlockNameType.UL_3_Reg;
        }

        public List<INotificationOfAcceptanceError> Check(EventData eventData, ISource source)
        {
            InitCheck(eventData);
            if (exist == true)
            {
                if (!regNum.DigitCheck(false, 13, 15))
                {
                    AddError("regNum", regNum, "Длина регистрационного номера лица, от которого перешли права и обязанности ЮЛ_5.5 не равна 13 или 15 цифрам");
                }

                if (date.DateGreater(source.sourceCreditInfoDate))
                {
                    AddError("date", date, "Дата правопреемства (окончания реорганизации) ЮЛ_5.6 позднее даты формирование кредитной информации");
                }
            }

            return errorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_5_Reorg_Type block, EventData eventData, ISource source)
        {
            return (block?.Check(eventData, source)).OrEmptyIfNull().ToList();
        }
    }
}
