using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Data.EntityFramework;
using Services.FrontEnd;
using Data.Entities;
using AutoMapper;
using Utility;


namespace Tests
{
    public class ProductServiceTests
    {

        [Fact]
        public async void GettAllProductsAsync_ReturnsMappedDtoList()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb").EnableSensitiveDataLogging()
                .Options;

            var memoryCache = new MemoryCache(new MemoryCacheOptions());

            using (var context = new AppDbContext(options))
            {
                context.Product.Add(new Product
                { ProductId = 1, Name = "Test", ListPrice = 999, ProductNumber = "TP-0001"});

                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options)) 
            {
                var service = new ProductService(context, null, memoryCache);

                var result = await service.getAllAsync();
            
                Assert.Single(result);
                Assert.Equal("Test", result[0].Name);
                Assert.Equal(999, result[0].ListPrice);
            }
        }
    }
}