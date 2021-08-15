using Microsoft.AspNetCore.Mvc;
using RedisApp.Api.Model;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

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
        public async Task PostKey([FromBody] string key, Student student)
        {
            await _redisCcheClient.GetDbFromConfiguration().AddAsync<Student>(key, student);
        }

        [HttpPost("redis/AddStudents")]
        public async Task PostMultipleKeys([FromBody] string key, Student student)
        {

            List<Tuple<string, Student>> studentlist = new List<Tuple<string, Student>>();
            studentlist.Add(new Tuple<string, Student>(key, student));
            await _redisCcheClient.GetDbFromConfiguration().AddAllAsync<Student>(studentlist);
        }

        [HttpGet("FakeData")]
        public async Task<List<Tuple<string, Student>>> InsertFakeData()
        {
            List<Tuple<string, Student>> fakedata = ReadFakeData();
            await _redisCcheClient.GetDbFromConfiguration().AddAllAsync<Student>(fakedata);
            return fakedata;
        }

        private List<Tuple<string, Student>> ReadFakeData()
        {
            var folderDetails = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{"FakeData\\Fake.json"}");
            var JSON = System.IO.File.ReadAllText(folderDetails);
            var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Student>>(JSON);

            List<Tuple<string, Student>> tuple = new List<Tuple<string, Student>>();
            int counter = 0;
            foreach (var item in jsonObj)
            {
                counter = counter + 1;
                tuple.Add(new Tuple<string, Student>("key" + counter.ToString(), (Student)item));
            }
            return tuple;
        }
    }
}
