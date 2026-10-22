using System.Security.Cryptography;
using System.Xml.Linq;
using System.Xml.XPath;

namespace loader_5_0_lib.Helpers
{
    public static class HashHelper
    {
        public static MemoryStream GetMemoryStream(string fileFullPath)
        {
            FileInfo docFile = new(fileFullPath);
            FileStream fileStream = docFile.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            MemoryStream memoryStream = new();
            fileStream.CopyTo(memoryStream);
            return memoryStream;
        }
        public static string GetFileHash(MemoryStream memoryStream)
        {
            using SHA256 mySHA256 = SHA256.Create();
            memoryStream.Position = 0;
            byte[] hashValue = mySHA256.ComputeHash(memoryStream);
            return BitConverter.ToString(hashValue).Replace("-", "").ToLowerInvariant();
        }
        public static string? GetTitleHash(XElement xSubject)
        {
            List<string> joinSubjectsParams = [];
            List<string> titleParams = ["lastName", "firstName", "countryCode", "docCode", "docNum", "issueDate", "foreignerCode", "birthDate", "taxNum", "regNum"];

            foreach (string titleParam in titleParams)
            {
                string? param = xSubject?.XPathSelectElement($"//Title//{titleParam}")?.Value;

                if (!string.IsNullOrEmpty(param))
                {
                    joinSubjectsParams.Add(param);
                }
            }

            return joinSubjectsParams.Count > 0 ? string.Join("#", joinSubjectsParams) : null;
        }
    }
}