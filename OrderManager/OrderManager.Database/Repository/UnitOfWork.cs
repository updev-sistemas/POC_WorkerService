using OrderManager.Database.Contracts;
using System;

namespace OrderManager.Database.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(Factory mongoDb)
        {
            var database = mongoDb.GetDatabase();

            this._order = new OrderRepository(database);
        }

        private readonly IOrderRepository _order;
        public IOrderRepository Order => _order;

    }
}
