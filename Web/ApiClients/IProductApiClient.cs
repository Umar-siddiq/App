using Utility.Shared;

namespace Web.ApiClients
{
    public interface IProductApiClient
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductsByIdAsync(int id);

        public Task<bool> CreateAsync(ProductDto dto);

        public Task<bool> UpdateAsync(int id, ProductDto dto);

        public Task<bool> DeleteAsync(int id);
    }
}
