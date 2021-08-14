using Microsoft.AspNetCore.Mvc;
using RedisApp.Api.Model;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisApp.Api.Controllers
{
    public class RedisController : Controller
    {
        //https://www.youtube.com/watch?v=Ozq8koFsYvY&lc=z225dz1qtpresbrcnacdp435idet2wdysaimyicqzmhw03c010c

        private readonly IRedisCacheClient _redisCcheClient;

        public RedisController(IRedisCacheClient redisCcheClient)
        {
            _redisCcheClient = redisCcheClient;
        }

        [HttpGet("redis/{key}")]
        public async Task<Student> Index(string key)
        {
            return await _redisCcheClient.GetDbFromConfiguration().GetAsync<Student>(key);
        }

        [HttpPost("redis/AddStudent")]
        public async Task PostKeys([FromBody] string key, Student student)
        {
            await _redisCcheClient.GetDbFromConfiguration().AddAsync<Student>(key, student);
        }
    }
}
