using System.Text;
using UrlShortener.Contracts;
using UrlShortener.Models;
using UrlShortener.Services;
using static UrlShortener.Helpers.Hashing;
using static UrlShortener.Helpers.UrlExtensions;

namespace UrlShortener.Clients;
/*
public class DatabaseClient(IDatabaseService inMemoryDatabase) : IDatabaseClient
{
    public Url? GetUrlObject(string key)
    {
        try
        {
            return inMemoryDatabase.Data[key];
        }
        catch
        {
            return null;
        }
    }

    public Url CreateUrl(CreateUrlRequest request)
    {
        var key      = GetSha256Hash(request.Url, 8);
        var shortUrl = GetShortUrl("http://localhost:5022/", key);
        var url      = new Url(key, shortUrl, request.Url);
        
        var (added, attempt) = (false, 0);
        while (added is false && attempt < 3)
        {
            try
            {
                inMemoryDatabase.Data.Add(url.Key, url);
                added = true;
            }
            catch (ArgumentException ex)
            {
                attempt++;
                url.Key = GetSha256Hash(request.Url, 8);
                Console.WriteLine(ex);
            }
        }

        return url;
    }

    public bool UpdateUrl(Url request)
    {
        try
        {
            inMemoryDatabase.Data[request.Key].Long = request.Long;
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public void DeleteUrl(string key) =>
        inMemoryDatabase.Data.Remove(key);
}
*/
