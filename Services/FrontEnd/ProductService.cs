using System.Security.Cryptography.X509Certificates;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Data.EntityFramework;
using Services.Interfaces;
using Utility.Shared;
using AutoMapper;


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


    }
}