using UrlShortener.Models;

namespace UrlShortener.Repositories;

public interface IDatabaseRepository
{
    public Task<Url?>  GetUrl(string key);
    public Task<Url> CreateUrl(Url request);
    public Task<Url?>  UpdateUrl(string key, string url);
    public Task<Url?> DeleteUrl(string key);
}
