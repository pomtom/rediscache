using Microsoft.AspNetCore.Mvc;
using RedisApp.Api.Model;
using RedisApp.Api.Service;
using System.Threading.Tasks;

namespace RedisApp.Api.Controllers
{
    public class CacheController : Controller
    {
        private readonly ICacheService _cacheService;

        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet("cache/{key}")]
        public async Task<IActionResult> GetCacheValue([FromRoute]string key)
        {
            var response = await _cacheService.GetCacheValueAsync(key);
            return string.IsNullOrEmpty(response) ? (IActionResult)NotFound() : Ok(response);
        }

        [HttpPost("cache")]
        public async Task<IActionResult> SetCacheValue([FromBody] Cache request)
        {
            await _cacheService.SetCacheValueAsync(request.key, request.value);
            return Ok();
        }
    }
}
