namespace loader_utils
{
    public static class DecimalExtensions
    {
        public static decimal? GetDecimal<T>(this decimal item, T itemName, string value) where T : Enum
        {
            T enumValue = (T)Enum.Parse(typeof(T), value);
            if (itemName.Equals(enumValue))
            {
                return item;
            }
            return null;
        }
    }
}
