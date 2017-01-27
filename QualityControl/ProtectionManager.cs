using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityControl_Client
{
    class ProtectionManager
    {
        private static Random random = new Random();

        private const string secretWord = "DFTPB1CMXA";

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string SHA1(string input)
        {
            byte[] hash;
            using (var sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider())
            {
                hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                
            }
            var sb = new StringBuilder();
            foreach (byte b in hash) sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }

        public static bool CompareHash(string clientKey, string producerKey)
        {
            return producerKey.Equals(SHA1(secretWord+clientKey));
        }
    }
}
