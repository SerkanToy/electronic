using electronic.Application.IMemory;
using electronic.Domain.Entities.Employees.Basket;
using StackExchange.Redis;
using System.Text.Json;

namespace electronic.Infrastructure.Memory
{
    public class InMemoryRepository : IInMemoryRepository
    {
        private readonly IDatabase _database;
        public InMemoryRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<Basket?> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);
            if(data.IsNullOrEmpty)
            {
                return null;
            }
            return JsonSerializer.Deserialize<Basket>(json:data);
        }

        public async Task<Basket?> UpdateBasketAsync(Basket basket)
        {
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if(!created)
            {
                return null;
            }
            return await GetBasketAsync(basket.Id);
        }
    }
}
