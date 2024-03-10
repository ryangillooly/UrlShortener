using System.Security.Cryptography;
using System.Text;

namespace UrlShortener.Helpers;

public static class Hashing
{
    public static string GetSha256Hash(string input, int length)
    {
        var bytes   = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        var builder = new StringBuilder();
        foreach (var t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }
        return builder.ToString().GetRandomSubstring(length);
    }

    private static string GetRandomSubstring(this string str, int length)
    {
        if (str.Length < length) throw new ArgumentException("String length is less than the requested substring length.");

        var random     = new Random();
        var startIndex = random.Next(0, str.Length - length + 1);
        
        return str.Substring(startIndex, length);
    }
}
