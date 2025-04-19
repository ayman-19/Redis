using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Redis.CashService;
using Redis.Helper;
using Redis.Models;

namespace Redis.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching",
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly RevisionContext revision;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheService _memoryCache;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            RevisionContext revision,
            IMemoryCache cache,
            MemoryCacheService memoryCache
        )
        {
            _logger = logger;
            this.revision = revision;
            _cache = cache;
            _memoryCache = memoryCache;
        }

        [HttpGet(Name = "GetWeatherForecastWithRedis")]
        [Cache(1)]
        public IActionResult GetWithRedis()
        {
            return Ok(revision.Os.ToList());
        }

        [HttpGet(Name = "GetWeatherForecastWithMemoryCache")]
        public IActionResult GetWithMemoryCache()
        {
            return Ok(_memoryCache.GetOrCreateCache(Request.Path));
        }

        [HttpPost(Name = "UploadImage")]
        //[ValidateAntiForgeryToken]
        public IActionResult Upload(IFormFile file)
        {
            return Ok(file.FileName);
        }
    }
}
