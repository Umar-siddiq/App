using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Data.EntityFramework;
using Services.Interfaces;
using Utility.Shared;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;


namespace Services.FrontEnd
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IMemoryCache _cache;
        
        public ProductService(AppDbContext context, IMapper mapper, IMemoryCache cache) : base(context, mapper) 
        { 
            _cache = cache;
        }
        public async Task<List<ProductDto>> getAllAsync()
        {
            const string cacheKey = "product_list";

            if (_cache.TryGetValue(cacheKey, out List<ProductDto> cachedProducts)) 
            {
                return cachedProducts;
            }

            //Slower AutoMapper Methods
            //return _mapper.Map<List<ProductDto>>(await _context.Product.ToListAsync());
            //return _mapper.Map<List<ProductDto>>(await _context.Product.ProjectTo<ProductDto>(_mapper.ConfigurationProvider).ToListAsync());

            //~20 ms VS ~7 ms
            
            //Fastest Method
                var products = await _context.Product.Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    ListPrice = p.ListPrice
                }).ToListAsync();

            _cache.Set(cacheKey, products, TimeSpan.FromMinutes(10));

            return products;
        }
        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var p = await _context.Product.FindAsync(id);
            if (p == null) return null;

            return new ProductDto { ProductId = p.ProductId, Name = p.Name, ListPrice = p.ListPrice };
        }

        public async Task<ProductDto> CreateAsync(ProductDto productdto)
        {
            var product = new ProductDto { 
                ProductName = productdto.Name, 
                ListPrice= productdto.ListPrice, 
                ProductNumber = Guid.NewGuid().ToString() };

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            _cache.Remove("product_list");

            productdto.ProductId = product.ProductId;
            
            return productdto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Product.FindAsync(id);

            if (existing == null) return false;


            _context.Product.Remove(existing);
            await _context.SaveChangesAsync();

            _cache.Remove("product_list");
            return true;
        }



        public async Task<bool> UpdateAsyc(int id, ProductDto productdto)
        {
            var existing = _context.Product.Find(id);
            if (existing == null) return false;

            existing.Name = productdto.Name;
            existing.ListPrice = productdto.ListPrice;

            _cache.Remove("product_List");
            return true;
        }
    }
}