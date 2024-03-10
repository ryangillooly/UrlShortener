using Microsoft.AspNetCore.Http.HttpResults;
using UrlShortener.Clients;
using UrlShortener.Contracts;
using UrlShortener.Models;
using StackExchange.Redis;
using UrlShortener.Repositories;
using static UrlShortener.Helpers.UrlExtensions;
using static UrlShortener.Helpers.Hashing;

namespace UrlShortener.Services;

public class DatabaseService(IDatabaseRepository databaseRepository) : IDatabaseService
{
    public async Task<Url?> GetUrl(string key) => 
        await databaseRepository.GetUrl(key);
    
    public async Task<Url> CreateUrl(CreateUrlRequest request)
    {
        var url = GetUrlObject(request);

        var (added, attempt) = (false, 0);
        while (added is false && attempt < 3)
        {
            try
            {
                await databaseRepository.CreateUrl(url);
                added = true;
            }
            catch (ArgumentException ex)
            {
                attempt++;
                url = GetUrlObject(request);
                Console.WriteLine(ex);
            }
        }
        
        return url;
    }
    
    public async Task<Url?> UpdateUrl(string key, UpdateUrlRequest request)
    {
        var url = await databaseRepository.GetUrl(key);
        if (url == null) return null;

        return await databaseRepository.UpdateUrl(key, request.Url);
    }

    public async Task<Url?> DeleteUrl(string key) =>
        await databaseRepository.DeleteUrl(key);
}
