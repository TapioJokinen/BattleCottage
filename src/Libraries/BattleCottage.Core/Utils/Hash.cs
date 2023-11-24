using System.Security.Cryptography;
using System.Text;

namespace BattleCottage.Core.Utils;

public static class Hash
{
    public static string GetSha256Hash(string input)
    {
        var data = SHA256.HashData(Encoding.UTF8.GetBytes(input));

        var sBuilder = new StringBuilder();

        foreach (var t in data)
            sBuilder.Append(t.ToString("x2"));

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }
}