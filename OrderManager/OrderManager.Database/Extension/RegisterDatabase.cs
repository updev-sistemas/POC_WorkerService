using Microsoft.Extensions.DependencyInjection;
using OrderManager.Database.Contracts;
using OrderManager.Database.Repository;

namespace OrderManager.Database.Extension
{
    public static class RegisterDatabase
    {
        public static void AddMongoDb(this IServiceCollection service)
        {
            _ = service.AddTransient<Factory>();
            _ = service.AddSingleton<IUnitOfWork, UnitOfWork>();
        }
    }
}
