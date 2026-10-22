namespace Xsd
{
    public class CurrentBlockCache
    {
        readonly HashSet<string> _cache = [];

        public bool ValidateCurrentBlock(string currentXml)
        {
            if (_cache.Contains(currentXml))
            {
                return false;
            }
            else
            {
                _cache.Add(currentXml);
                return true;
            }
        }
    }
}
