using System.Diagnostics.CodeAnalysis;

namespace loader_utils
{
    public static class StringExtensions
    {
        // Проверяет, что значение value имеет пустое значение (прочерк)
        public static bool IsEmptyValueDash(this string? value)
        {
            return value == "-";
        }
        public static bool IsNotNull([NotNullWhen(true)] this string? value, bool acceptDash = true)
        {
            if (string.IsNullOrEmpty(value) || (value == "-" && acceptDash))
                return false;
            return true;
        }

        public static bool? GetBool<T>(this string item, T itemName, string value) where T : Enum
        {
            if (item != null)
            {
                T enumValueTrue = (T)Enum.Parse(typeof(T), value + "_1");
                T enumValueFalse = (T)Enum.Parse(typeof(T), value + "_0");

                if (itemName.Equals(enumValueTrue))
                {
                    return true;
                }
                else if (itemName.Equals(enumValueFalse))
                {
                    return false;
                }
            }
            return null;
        }

        #region DigitCheck

        public static bool DigitCheck(this string str, params int[] lengths)
        {
            return str.DigitCheck(true, lengths);
        }

        public static bool DigitCheck(this string str, bool acceptDash, params int[] lengths)
        {
            if (IsNotNull(str, acceptDash) && (!str.Length.In(lengths) || !str.All(char.IsDigit)))
            {
                return false;
            }
            return true;
        }

        #endregion 
    }
}