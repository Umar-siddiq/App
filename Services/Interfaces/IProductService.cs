using Utility.Shared;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> getAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(ProductDto productdto);
        Task<bool> UpdateAsync(int id, ProductDto productdto);
        Task<bool> DeleteAsync(int id);
    }
}
