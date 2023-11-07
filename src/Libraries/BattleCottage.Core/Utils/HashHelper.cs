using System.Security.Cryptography;
using System.Text;

namespace BattleCottage.Core.Utils
{
    public class HashHelper
    {
        public static string GenerateSHA265Hash(object parameter)
        {
            string source = parameter.ToString() ?? throw new ArgumentNullException(nameof(parameter));

            using SHA256 sha256Hash = SHA256.Create();
            return GetHash(sha256Hash, source);
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
