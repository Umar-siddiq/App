using Utility.Shared;

namespace Web.ApiClients
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly HttpClient _httpclient;

        public ProductApiClient(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            return await _httpclient.GetFromJsonAsync<List<ProductDto>>("api/products");
        }
    }
}
