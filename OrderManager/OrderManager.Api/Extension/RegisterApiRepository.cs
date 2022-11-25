using Microsoft.Extensions.DependencyInjection;
using OrderManager.Api.Contracts;
using OrderManager.Api.Repository;

namespace OrderManager.Api.Extension
{
    public static class RegisterApiRepository
    {
        public static void AddApi(this IServiceCollection service)
        {
            _ = service.AddSingleton<IOrderApiRepository, OrderApiRepository>();
        }
    }
}
