using Utility.Shared;

namespace Web.ApiClients
{
    public interface IProductApiClient
    {
        Task<List<ProductDto>> GetAllProductsAsync();
    }
}
