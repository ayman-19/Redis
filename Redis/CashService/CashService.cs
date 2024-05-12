
using StackExchange.Redis;
using System.Text.Json;

namespace Redis.CashService
{
    public class CashService : ICashService
    {
        private readonly IDatabase _database;

        public CashService(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<string> GetAsync(string key)
            => (await _database.StringGetAsync(key)).ToString() ?? "";

        public async Task SetAsync(string key, object value, TimeSpan time)
        {
            var str = JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = true });
            await _database.StringSetAsync(key, str, time);
        }

    }
}
