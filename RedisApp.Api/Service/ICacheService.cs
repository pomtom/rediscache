using System.Threading.Tasks;

namespace RedisApp.Api.Service
{
    public interface ICacheService
    {
        Task<string> GetCacheValueAsync(string key);

        Task SetCacheValueAsync(string key, string value);
    }
}
