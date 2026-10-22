using System.ComponentModel;

namespace loader_5_0_lib.Classes
{
    public enum RefProcessingStatus
    {
        /// <summary>
        /// -1 Статус не определен
        /// </summary>
        [Description("Статус не определен")]
        NotDefine = -1,

        /// <summary>
        /// 0 Обработка запущена / выполняется
        /// </summary>
        [Description("Обработка запущена / выполняется")]
        Running = 0,

        /// <summary>
        /// 10 Обработка отложена – отправлено уведомление
        /// </summary>
        [Description("Обработка отложена – отправлено уведомление")]
        AwaitSentMessage = 10,

        /// <summary>
        /// 11 Обработка отложена – задача поставлена в очередь
        /// </summary>
        [Description("Обработка отложена – задача поставлена в очередь")]
        AwaitSentToQueue = 11,

        /// <summary>
        /// 20 Обработка остановлена
        /// </summary>
        [Description("Обработка остановлена")]
        StopManual = 20,

        /// <summary>
        /// 21 Обработка прервана в результате ошибки
        /// </summary>
        [Description("Обработка прервана в результате ошибки")]
        Error = 21,

        /// <summary>
        /// 100 Обработка завершена
        /// </summary>
        [Description("Обработка завершена")]
        Finish = 100,
    }
}
