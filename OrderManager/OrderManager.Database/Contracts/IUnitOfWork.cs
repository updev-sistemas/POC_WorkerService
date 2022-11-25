namespace OrderManager.Database.Contracts
{
    public interface IUnitOfWork
    {
        IOrderRepository Order { get; }
    }
}
