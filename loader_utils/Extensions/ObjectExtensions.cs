namespace loader_utils
{
    public static class ObjectExtensions
    {
        public static bool In<T>(this T item, params T[] items)
        {
            ArgumentNullException.ThrowIfNull(item);
            ArgumentNullException.ThrowIfNull(items);

            return items.Contains(item);
        }

        public static T GetObject<T>(this object item)
        {
            if (item is T t)
                return t;
            else
                return default!;
        }

        public static T[]? ConvertToArray<T>(this T item)
        {
            if (item is null)
            {
                return null;
            }
            return [item];
        }
    }
}
