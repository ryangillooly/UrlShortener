using System.Text;
using UrlShortener.Contracts;
using UrlShortener.Models;
using static UrlShortener.Helpers.Hashing;

namespace UrlShortener.Helpers;

public static class UrlExtensions
{
    private static string GetShortUrl(string url, string hash) =>
        new StringBuilder(url).Append(hash).ToString();
    
    public static Url GetUrlObject(CreateUrlRequest request)
    {
        var hash      = GenerateShortUrlHash(request.Url, 8);
        var shortUrl = GetShortUrl("http://localhost:5022/", hash);
        
        return new Url(hash, shortUrl, request.Url);
    }
}
