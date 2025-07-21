using Data.EntityFramework;
using System.Security.Cryptography.X509Certificates;
using Data.Entities;
using Services.Interfaces;
using Utility.Shared;
using Microsoft.EntityFrameworkCore;


namespace Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(AppDbContext context) : base(context) {}

        public async Task<List<ProductDto>> getAllAsync()
        {

            return await Context.Product.Select(p => new ProductDto 
            {
                ProductId = p.ProductId,
                Name = p.Name,
                ListPrice = p.ListPrice
            }).ToListAsync();
        }
    }
}