namespace loader_utils
{
    public static class UshortExtensions
    {
        public static bool In(this ushort item, params int[] items)
        {
            ArgumentNullException.ThrowIfNull(items);

            return items.Contains((int)item);
        }
    }
}
