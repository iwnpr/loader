namespace loader_utils
{
    public static class EnumHelper
    {
        public static TEnum GetCode<TEnum>(string value) where TEnum : Enum
        {
            foreach (object o in System.Enum.GetValues(typeof(TEnum)))
            {
                TEnum enumValue = (TEnum)o;
                if (enumValue.GetXmlEnumAttribute()?.Equals(value, StringComparison.OrdinalIgnoreCase) == true)
                {
                    return (TEnum)o;
                }
            }
            throw new ArgumentException("No XmlEnumAttribute code exists for type " + typeof(TEnum).ToString() + " corresponding to value of " + value);
        }
    }
}
