namespace loader_5_0_lib.Classes
{
    public static class AppContextParams
    {
        private static long? _uploadKeyId;

        public static string? GetSomething()
        {
            return _uploadKeyId.ToString();
        }

        public static void SetSomething(long? uploadKeyId)
        {
            _uploadKeyId = uploadKeyId;
        }
    }
}
