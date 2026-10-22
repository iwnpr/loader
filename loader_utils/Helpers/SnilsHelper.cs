namespace loader_utils
{
    public static class SnilsHelper
    {
        public static bool CheckSnils(string snils)
        {
            if (snils == null || snils == "-")
                return true;
            snils = snils.Replace("-", "").Replace(" ", "");
            var smilsNumber = int.Parse(snils[0..^2]);
            if (smilsNumber <= 001001998)
                return true;
            if (snils.Reverse().Skip(2).Select((element, index) => int.Parse(element.ToString()) * (index + 1)).Sum() % 101 % 100 != int.Parse(new string([.. snils.TakeLast(2)])))
                return false;
            return true;
        }
    }
}
