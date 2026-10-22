namespace loader_5_0_lib.Helpers
{
    public static class FileHelper
    {
        public static bool ExistFileName(string path, string fileName, out string fullFileNameReal)
        {
            string fullFileName = Path.Combine(path, fileName);

            EnumerationOptions enumerationOptions = new EnumerationOptions();
            enumerationOptions.MatchCasing = MatchCasing.CaseInsensitive;
            enumerationOptions.ReturnSpecialDirectories = false;

            if (Directory.Exists(path))
            {
                foreach (var _fileName in Directory.GetFiles(path, "*.*", enumerationOptions))
                {
                    if (_fileName.ToUpper().Equals(fullFileName.ToUpper()))
                    {
                        fullFileNameReal = _fileName;
                        return true;
                    }
                }
            }

            fullFileNameReal = fullFileName;
            return false;
        }

        public static bool HasPriorityOrder(string fileName, string priorityPrefix)
        {
            return fileName.Contains($"_{priorityPrefix}", StringComparison.OrdinalIgnoreCase);
        }
    }
}