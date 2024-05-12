
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Redis.CashService
{
    public class CacheService02 : ICashService
    {
        private readonly IDistributedCache _cache;

        public CacheService02(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<string> GetAsync(string key) => await _cache.GetStringAsync(key) ?? "";

        public Task SetAsync(string key, object value, TimeSpan time)
            => _cache.SetStringAsync(key, JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = true }), new DistributedCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddMinutes(1) });
    }
}
