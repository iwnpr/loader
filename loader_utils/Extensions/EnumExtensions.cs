using System.Xml.Serialization;

namespace loader_utils
{
    public static class EnumExtensions
    {
        public static string? GetXmlEnumAttribute<TEnum>(this TEnum value) where TEnum : Enum
        {
            var enumType = value.GetType();

            var member = enumType.GetMember(value.ToString()).FirstOrDefault();
            if (member == null) return null;

            var attribute = member.GetCustomAttributes(false).OfType<XmlEnumAttribute>().FirstOrDefault();
            if (attribute == null) return null;
            return attribute.Name;
        }
    }
}
