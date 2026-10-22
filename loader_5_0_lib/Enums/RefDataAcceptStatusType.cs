using System.ComponentModel;

namespace loader_5_0_lib.Enums
{
    public enum RefDataAcceptStatusType
    {
        /// <summary>
        /// Данные приняты полностью
        /// </summary>
        [Description("Данные приняты полностью")]
        ACCEPTED,

        /// <summary>
        /// Данные приняты частично
        /// </summary>
        [Description("Данные приняты частично")]
        PARTIALLY_ACCEPTED,

        /// <summary>
        /// Данные приняты полностью
        /// </summary>
        [Description("Данные приняты полностью")]
        REJECTED
    }
}