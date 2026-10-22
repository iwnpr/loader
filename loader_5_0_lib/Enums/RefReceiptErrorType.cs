using System.ComponentModel;

namespace loader_5_0_lib.Enums
{
    public enum RefReceiptErrorType
    {
        /// <summary>
        /// 01 Некорректное (неверное) наименование файла
        /// </summary>
        [Description("Некорректное (неверное) наименование файла")]
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <summary>
        /// 02 Некорректные (неверные) регистрационные данные файла
        /// </summary>
        [Description("Некорректные (неверные) регистрационные данные файла")]
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <summary>
        /// 03 Некорректные (неверные) идентификационные данные, полученные от источника
        /// </summary>
        [Description("Некорректные (неверные) идентификационные данные, полученные от источника")]
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <summary>
        /// 04 Ошибка архивации файла
        /// </summary>
        [Description("Ошибка архивации файла")]
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <summary>
        /// 05 Ошибка подписи/сертификата/шифрования
        /// </summary>
        [Description("Ошибка подписи/сертификата/шифрования")]
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,

        /// <summary>
        /// 06 Отсутствие доступа источника к системе загрузки файлов
        /// </summary>
        [Description("Отсутствие доступа источника к системе загрузки файлов")]
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,

        /// <summary>
        /// 07 Ошибка в схеме
        /// </summary>
        [Description("Ошибка в схеме")]
        [System.Xml.Serialization.XmlEnumAttribute("7")]
        Item7,

        /// <summary>
        /// 99 Иная ошибка, не предусмотренная данной таблицей. В описании ошибки должна быть дополнительно описана причина ее возникновения
        /// </summary>
        [Description("Иная ошибка, не предусмотренная данной таблицей. В описании ошибки должна быть дополнительно описана причина ее возникновения")]
        [System.Xml.Serialization.XmlEnumAttribute("99")]
        Item99
    }
}
