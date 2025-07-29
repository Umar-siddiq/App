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

        public async Task<ProductDto> GetProductsByIdAsync( int Id )
        {
            return await _httpclient.GetFromJsonAsync<ProductDto>($"api/products/{Id}");
        }


        public async Task<bool> CreateAsync(ProductDto dto)
        {
            var result = await _httpclient.PostAsJsonAsync("api/products", dto);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateAsync(int Id, ProductDto dto)
        {
            var result = await _httpclient.PutAsJsonAsync<ProductDto>($"api/products/{Id}", dto);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _httpclient.DeleteAsync($"api/products/{id}");
            return result.IsSuccessStatusCode;
        }
    }
}
