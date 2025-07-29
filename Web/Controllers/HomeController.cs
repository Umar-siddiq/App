using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Utility.Shared;
using Web.ApiClients;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductApiClient _productApiClient;

        public HomeController(ILogger<HomeController> logger, IProductApiClient productApiClient)
        {
            _logger = logger;
            _productApiClient = productApiClient;
        }


        public async Task<IActionResult> Index()
        {
            var products = await _productApiClient.GetAllProductsAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create() 
        {
            return View(new ProductDto());
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto productDto) 
        {
            if (!ModelState.IsValid) return View(productDto);

            var success = await _productApiClient.CreateAsync(productDto);

            if (!success)
            {
                ModelState.AddModelError("", "Create Failed, Please Try Again");
                return View(productDto);
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit( int id ) 
        {
            var product = await _productApiClient.GetProductsByIdAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit ( int id, ProductDto productDto) 
        {
            if (!ModelState.IsValid)
            {
                return View(productDto);
            }

            var success = await _productApiClient.UpdateAsync(id, productDto);

            if (!success)
            {
                ModelState.AddModelError("", "Update Failed, Please Try Again");
                return View(productDto);
            }
            
            return RedirectToAction("Index") ;
        }
        
        public async Task<IActionResult> Delete( int id)
        {
            var product = await _productApiClient.GetProductsByIdAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            var success = await _productApiClient.DeleteAsync(id);
            if (!success) return BadRequest();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}