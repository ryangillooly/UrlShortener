namespace UrlShortener.Models;

public class Url(string hash, string shortUrl, string longUrl)
{
    public string Hash   { get; set; } = hash;
    public string ShortUrl { get; set; } = shortUrl;
    public string LongUrl  { get; set; } = longUrl;
}
