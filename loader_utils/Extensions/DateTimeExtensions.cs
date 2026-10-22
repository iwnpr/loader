namespace loader_utils
{
    public static class DateTimeExtensions
    {
        public static DateTime RemoveMilliseconds(this DateTime dateTime)
        {
            return dateTime.AddTicks(-(dateTime.Ticks % TimeSpan.TicksPerSecond));
        }
    }
}
