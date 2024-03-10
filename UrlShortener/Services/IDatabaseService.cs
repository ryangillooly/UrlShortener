using UrlShortener.Contracts;
using UrlShortener.Models;

namespace UrlShortener.Services;

public interface IDatabaseService
{
    public Task<Url?> GetUrl(string key);
    public Task<Url>  CreateUrl(CreateUrlRequest request);
    public Task<Url?>  UpdateUrl(string key, UpdateUrlRequest request);
    public Task<Url?> DeleteUrl(string key);
}
