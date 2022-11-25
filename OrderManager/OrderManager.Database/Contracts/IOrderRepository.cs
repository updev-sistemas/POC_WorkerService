using OrderManager.Entity;

namespace OrderManager.Database.Contracts
{
    public interface IOrderRepository : IDefaultRepository<OrderBson>
    {
    }
}
