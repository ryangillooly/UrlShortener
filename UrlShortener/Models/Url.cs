namespace UrlShortener.Models;

public class Url(string key, string shortUrl, string longUrl)
{
    public string Key   { get; set; } = key;
    public string Short { get; set; } = shortUrl;
    public string Long  { get; set; } = longUrl;
}
