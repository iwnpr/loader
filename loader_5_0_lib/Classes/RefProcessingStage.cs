using System.ComponentModel;

namespace loader_5_0_lib.Classes
{
    public enum RefProcessingStage
    {
        /// <summary>
        /// Предварительная загрузка
        /// </summary>
        [Description("Предварительная загрузка")]
        PreLoad = 2010100,

        /// <summary>
        /// Обработка
        /// </summary>
        [Description("Обработка")]
        Processing = 2020100
    }
}
