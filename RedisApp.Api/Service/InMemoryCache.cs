using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace RedisApp.Api.Service
{
    public class InMemoryCache : ICacheService
    {
        private readonly IMemoryCache _cache;
        public InMemoryCache(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public Task<string> GetCacheValueAsync(string key)
        {
            return Task.FromResult(_cache.Get<string>(key));
        }

        public Task SetCacheValueAsync(string key, string value)
        {
            _cache.Set(key, value);
            return Task.CompletedTask;
        }
    }
}
