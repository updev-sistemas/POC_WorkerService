using MongoDB.Driver;
using OrderManager.Database.Contracts;
using OrderManager.Entity;
using System.Linq.Expressions;

namespace OrderManager.Database.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<OrderBson>? collection;

        public OrderRepository(IMongoDatabase context)
            => collection = context.GetCollection<OrderBson>("orders");

        public async Task CreateAsync(OrderBson target)
            => await collection!.InsertOneAsync(target);

        public async Task<List<OrderBson>>? FindAllAsync()
            => await collection!.Find(_ => true).ToListAsync();

        public async Task<OrderBson?> FindByIdAsync(string id)
            => await collection!.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<OrderBson>>? QueryAsync(Expression<Func<OrderBson, bool>> expression)
            => await collection!.Find(expression).ToListAsync();

        public async Task RemoveAsync(string id)
            => await collection!.DeleteOneAsync(x => x.Id == id);

        public async Task UpdateAsync(string id, OrderBson target)
            => await collection!.ReplaceOneAsync(x => x.Id == id, target);
    }
}
