using UrlShortener.Models;

namespace UrlShortener.Repositories;

public interface IDatabaseRepository
{
    public Task<Url?>  GetUrlByHash(string key);
    public Task<Url?>  GetUrlByLongUrl(string longUrl);
    public Task<Url> CreateUrl(Url request);
    public Task<Url?>  UpdateUrl(string key, string url);
    public Task<bool> DeleteUrl(string key);
}
