
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrderManager.Api.Contracts;
using OrderManager.Common;
using OrderManager.ModelDTO;

namespace OrderManager.Api.Repository
{
    public class OrderApiRepository : IOrderApiRepository
    {
        private readonly ApiSetting? _setting;

        public OrderApiRepository(IOptions<ApiSetting> options)
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));
            ArgumentNullException.ThrowIfNull(options?.Value, nameof(options));
            ArgumentNullException.ThrowIfNull(options?.Value?.Url, nameof(options));

            this._setting = options?.Value;
        }

        public async Task<IEnumerable<OrderJson>?> GetOrdersByProcessing()
        {
            var uri = new Uri(this._setting!.Url!);

            using HttpClient client = new();
            using HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (string.IsNullOrEmpty(result))
            {
                return Array.Empty<OrderJson>();
            }
            
            var resultFormated = JsonConvert.DeserializeObject<IEnumerable<OrderJson>?>(result);

            return resultFormated;
        }
    }
}
