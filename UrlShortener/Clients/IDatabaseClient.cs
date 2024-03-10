using UrlShortener.Contracts;
using UrlShortener.Models;

namespace UrlShortener.Clients;

public interface IDatabaseClient
{
    public void  GetUrl(string key);
    public Task<Url>  CreateUrl(CreateUrlRequest request);
    public bool  UpdateUrl(Url request);
    public void DeleteUrl(string key);
}
