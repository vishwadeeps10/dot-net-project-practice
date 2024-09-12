using CollegeApp.data.Repository;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CollegeApp.data.Services
{
    public class RedisCacheService : ICache
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<RedisCacheService> _logger;
        public RedisCacheService(IDistributedCache cache, ILogger<RedisCacheService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

      public async Task<T> GetData<T>(string key)
    {
        var cachedData = await _cache.GetStringAsync(key);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<T>(cachedData);
        }

        return default;
    }

    public async Task SetData<T>(string key, T value, TimeSpan expirationTime)
    {
        var serializedData = JsonSerializer.Serialize(value);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expirationTime
        };

        await _cache.SetStringAsync(key, serializedData, options);
    }

        public async Task RemoveData(string key) 
        {
            await _cache.RemoveAsync(key);
            _logger.LogInformation($"Cache removed for key: {key}");
        }

    }
}
