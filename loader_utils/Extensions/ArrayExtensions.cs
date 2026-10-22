namespace loader_utils
{
    public static class ArrayExtensions
    {
        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T>? source)
        {
            return source ?? [];
        }

        private static int[] IndexesOf<T>(this IEnumerable<T> list, T value)
        {
            return [.. list.Select((element, index) => EqualityComparer<T>.Default.Equals(element, value) ? index : -1).Where(i => i >= 0)];
        }

        private static T[] SelectByName<T, Enum>(this IEnumerable<object> items, IEnumerable<Enum> itemNames, Enum value)
        {
            return [.. items.Where((element, index) => index.In(itemNames.IndexesOf(value))).Select(x => (T)x)];
        }

        public static T GetObject<T>(this object[] items)
        {
            if (items == null)
                return default!;
            return (T)items.Where(x => x is T).FirstOrDefault()!;
        }

        public static object? GetObject<T>(this object[] items, IEnumerable<T> itemNames, string value) where T : Enum
        {
            T enumValue = (T)Enum.Parse(typeof(T), value);
            if (itemNames.Contains(enumValue))
            {
                return items[Array.IndexOf([.. itemNames], enumValue)];
            }
            return null;
        }

        public static T[] GetObjects<T>(this object[] items)
        {
            if (items == null)
                return default!;
            return [.. items.Where(x => x is T).Select(x => (T)x)];
        }

        public static object[] GetObjects<T>(this object[] items, IEnumerable<T> itemNames, string value) where T : Enum
        {
            T enumValue = (T)Enum.Parse(typeof(T), value);
            return items.SelectByName<object, T>(itemNames, enumValue);
        }

        public static decimal? GetDecimal<T>(this object[] items, IEnumerable<T> itemNames, string value) where T : Enum
        {
            return (decimal?)items.GetObject(itemNames, value);
        }

        public static DateTime? GetDateTime<T>(this object[] items, IEnumerable<T> itemNames, string value) where T : Enum
        {
            return (DateTime?)items.GetObject(itemNames, value);
        }

        public static DateTimeOffset? GetDateTimeOffset<T>(this object[] items, IEnumerable<T> itemNames, string value) where T : Enum
        {
            return (DateTimeOffset?)items.GetObject(itemNames, value);
        }

        public static ulong? GetUlong<T>(this object[] items, IEnumerable<T> itemNames, string value) where T : Enum
        {
            return (ulong?)items.GetObject(itemNames, value);
        }

        public static ushort? GetUshort<T>(this object[] items, IEnumerable<T> itemNames, string value) where T : Enum
        {
            return (ushort?)items.GetObject(itemNames, value);
        }

        public static string? GetString<T>(this object[] items, IEnumerable<T> itemNames, string value) where T : Enum
        {
            return (string?)items.GetObject(itemNames, value);
        }

        public static bool? GetBool<T>(this object[] items, IEnumerable<T> itemNames, string value) where T : Enum
        {
            if (items == null)
            {
                return null;
            }
            if (items.GetObject(itemNames, value + "_1") != null)
            {
                return true;
            }
            if (items.GetObject(itemNames, value + "_0") != null)
            {
                return false;
            }
            return null;
        }
    }
}
