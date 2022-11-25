using OrderManager.Api.Contracts;
using OrderManager.Database.Contracts;
using OrderManager.Entity;

namespace OrderManager
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IUnitOfWork? _repository;
        private readonly IOrderApiRepository? _api;

        public Worker(ILogger<Worker> logger, IUnitOfWork repository, IOrderApiRepository api)
        {
            this._logger = logger;
            this._repository = repository;
            this._api = api;
        }


        private async Task RegisterNewOrders()
        {
            try
            {
                var result = await this._api!.GetOrdersByProcessing();

                if (result == null)
                    return;

                if (result.Any())
                {
                    foreach (var order in result.Where(o => o.Status == "Processing"))
                    {
                        try
                        {
                            if (order.Items == null)
                            {
                                continue;
                            }

                            if (!order.Items.Any())
                            {
                                continue;
                            }

                            var orderInDb = await this._repository!.Order!.QueryAsync(x => x.Number == order.Number).ConfigureAwait(false);

                            if (!orderInDb.Any())
                            {
                                var orderForRegister = new OrderBson
                                {
                                    Customer = new CustomerBson
                                    {
                                        Email = order?.Customer?.Email ?? "N/D",
                                        Name = order?.Customer?.Name ?? "N/D",
                                        Phone = order?.Customer?.Phone ?? "N/D"
                                    },
                                    Number = order!.Number,
                                    Status = order!.Status,
                                    Date = order!.Date,  
                                    Version = "1.0",
                                };
                                                                
                                var items = new List<ItemBson>();
                                foreach (var item in order.Items)
                                {
                                    var i = new ItemBson
                                    { 
                                        Cost = item.Cost,
                                        ProductName = item.Product,
                                        Quantity = item.Quantity,
                                        Sku = item.Sku
                                    };

                                    items.Add(i);
                                }

                                orderForRegister.Items = items;

                                await this._repository!.Order!.CreateAsync(orderForRegister).ConfigureAwait(false);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger!.LogError(ex.Message, ex);
                            _ = ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger!.LogError(ex.Message, ex);
                _ = ex;
            }
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await RegisterNewOrders().ConfigureAwait(false);

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}