using Microsoft.Extensions.Caching.Memory;
using Redis.Models;

namespace Redis.CashService
{
    public class MemoryCacheService
    {
        private readonly IMemoryCache _cache;
        private readonly RevisionContext _context;

        public MemoryCacheService(IMemoryCache cache, RevisionContext context)
        {
            _cache = cache;
            _context = context;
        }

        public List<O> GetOrCreateCache(string key)
        {
            var result = _cache.GetOrCreate(key, entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                return _context.Os.ToList();
            });
            return result ?? new List<O>();
        }

    }
}
