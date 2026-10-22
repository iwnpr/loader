using loader_utils;

namespace Xsd.VersionFiveZero.Main
{
    public partial class UL_5_Reorg_Type : I_BlockType
    {
        public refBlockNameType blockName
        {
            get
            {
                return refBlockNameType.UL_3_Reg;
            }
        }

        public List<INotificationOfAcceptanceError> Check(IEventData? eventData, ISource source)
        {
            ErrorData errorData = new(blockName, eventData);
            if (exist == true)
            {
                if (regNum?.DigitCheck(false, 13, 15) == false)
                {
                    errorData.AddError("regNum", regNum, "Длина регистрационного номера лица, от которого перешли права и обязанности ЮЛ_5.5 не равна 13 или 15 цифрам");
                }

                if (date.DateGreater(source.sourceCreditInfoDate))
                {
                    errorData.AddError("date", date, "Дата правопреемства (окончания реорганизации) ЮЛ_5.6 позднее даты формирование кредитной информации");
                }
            }

            return errorData.ErrorList;
        }
    }

    public static partial class Validate_Extensions
    {
        public static List<INotificationOfAcceptanceError> Validate(this UL_5_Reorg_Type block, IEventData? eventData, ISource source)
        {
            return [.. (block?.Check(eventData, source)).OrEmptyIfNull()];
        }

        public static List<INotificationOfAcceptanceError> Validate(this IEnumerable<UL_5_Reorg_Type> blocks, IEventData? eventData, ISource source)
        {
            List<INotificationOfAcceptanceError> errorList = [];
            foreach (var block in blocks.OrEmptyIfNull())
            {
                errorList.AddRange(block.Check(eventData, source));
            }
            return errorList;
        }
    }
}
