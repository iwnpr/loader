using System.Text.RegularExpressions;

namespace loader_utils
{
    public static partial class UidHelper
    {
        public static bool CheckUid(string? uid)
        {
            if (uid == null || uid == "-")
            {
                return true;
            }
            uid = uid.Replace("-", "").ToLower();
            if (uid.Length != 33 || !UidRegex().IsMatch(uid))
            {
                return false;
            }
            int sum = 0;
            for (int i = 0; i < 32; i++)
            {
                sum += (i % 10 + 1) * CharToHex(uid[i]);
            }
            if (CharToHex(uid[32]) != sum % 16)
            {
                return false;
            }
            return true;
        }

        static int CharToHex(char uidChar)
        {
            if (uidChar == '0')
                return 0;
            if (uidChar == '1')
                return 1;
            if (uidChar == '2')
                return 2;
            if (uidChar == '3')
                return 3;
            if (uidChar == '4')
                return 4;
            if (uidChar == '5')
                return 5;
            if (uidChar == '6')
                return 6;
            if (uidChar == '7')
                return 7;
            if (uidChar == '8')
                return 8;
            if (uidChar == '9')
                return 9;
            if (uidChar == 'a')
                return 10;
            if (uidChar == 'b')
                return 11;
            if (uidChar == 'c')
                return 12;
            if (uidChar == 'd')
                return 13;
            if (uidChar == 'e')
                return 14;
            if (uidChar == 'f')
                return 15;
            throw new Exception();
        }

        [GeneratedRegex(@"^[a-f0-9]+$")]
        private static partial Regex UidRegex();
    }
}
