using StackExchange.Redis;
using UrlShortener.Models;

namespace UrlShortener.Repositories;

public class RedisRepository(IConnectionMultiplexer connectionMultiplexer) : IDatabaseRepository
{
    private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

    public async Task CreateUrl(Url request)
    {
        await _database.HashSetAsync
        (
            request.Key, 
            new []
            {
                new HashEntry("LongUrl", request.Long), 
                new HashEntry("ShortUrl", request.Short)
            }
        );
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
}
