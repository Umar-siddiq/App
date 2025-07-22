using System.Net.Http.Json;
using Utility.Shared;

namespace Web.Blazor
{

    public class ProductApiClient
    {
        private readonly HttpClient _httpClient;

        public ProductApiClient( HttpClient httpclient ) 
        {
            _httpClient = httpclient;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync() 
        {
        
            return await _httpClient.GetFromJsonAsync<List<ProductDto>>("/api/products");
        }

    }
}
