using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Utility.Shared;

namespace API.Controllers
{
    //[ServiceFilter(typeof(APILoggingFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            var products = await _productService.getAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProductDto>>> GetById(int id)
        {
            var products = await _productService.GetByIdAsync(id);
            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<List<ProductDto>>> Create([FromBody] ProductDto productDto)
        {
            var created = await _productService.CreateAsync(productDto);
            return Ok(created);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _productService.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] ProductDto productDto) 
        {
            var products = await _productService.UpdateAsync(id, productDto);
            return Ok(products);
        }
    }
}
