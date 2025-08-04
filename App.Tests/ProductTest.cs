using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Data.EntityFramework;
using Services.FrontEnd;
using Data.Entities;
using AutoMapper;
using Utility;
using Utility.Shared;
using System.ComponentModel.DataAnnotations;


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
                Assert.Equal("Test", result[0].ProductName);
                Assert.Equal(999, result[0].ListPrice);
            }
        }


        [Fact]
        public void ProductDto_Model_IsInvalid_WhenNameIsNull() 
        {
            var model = new ProductDto { ListPrice = 100};

            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, v => v.MemberNames.Contains("ProductNames"));
        }
    }
}