using OrderManager.ModelDTO;

namespace OrderManager.Api.Contracts
{
    public interface IOrderApiRepository
    {
        Task<IEnumerable<OrderJson>?> GetOrdersByProcessing();
    }
}
