using StackExchange.Redis;
using UrlShortener.Models;

namespace UrlShortener.Repositories;

public class RedisRepository(IConnectionMultiplexer connectionMultiplexer) : IDatabaseRepository
{
    private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

    public async Task<Url> CreateUrl(Url request)
    {
        var data = await GetShortUrlCode(request.Long);
        if (data != null)
        {
            return await GetUrl(data);
        }
        
        await _database.StringSetAsync(request.Long, request.Key);
        await _database.HashSetAsync
        (
            request.Key, 
            new []
            {
                new HashEntry("LongUrl", request.Long), 
                new HashEntry("ShortUrl", request.Short)
            }
        );
        
        return await GetUrl(request.Key);
    }

    public async Task<Url?> UpdateUrl(string key, string url)
    {
        var data = await GetUrl(key);
        if (data == null) return null;
        
        await _database.HashSetAsync
        (
            key,
            new[]
            {
                new HashEntry("LongUrl", url)
            }
        );

        return await GetUrl(key);
    }

    public async Task<Url?> DeleteUrl(string key)
    {
        var data = await GetUrl(key);
        if (data == null) return null;

        await _database.KeyDeleteAsync(data.Long);
        await _database.KeyDeleteAsync(key);
        return data;
    }

    public async Task<Url?> GetUrl(string key)
    {
        var dataHash = await _database.HashGetAllAsync(key);
        var data     = dataHash.ToDictionary(entry => entry.Name, entry => entry.Value);

        return data.Count == 0 
            ? null 
            : new Url(key, data["ShortUrl"], data["LongUrl"]);
    }

    private async Task<string?> GetShortUrlCode(string longUrl) =>
        await _database.StringGetAsync(longUrl);
}
