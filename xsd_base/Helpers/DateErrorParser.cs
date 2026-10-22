namespace Xsd
{
    public record DateError
    {
        public required string Field;
        public required string Value;
    }

    public static class DateErrorParser
    {
        public static DateError? Parse(string errorString)
        {
            var errorStringParts = errorString.Split("'");
            if (errorStringParts.Length < 4)
            {
                return null;
            }
            return new DateError()
            {
                Field = errorStringParts[1],
                Value = errorStringParts[3]
            };
        }
    }
}
