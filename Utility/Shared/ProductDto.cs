using Data.Entities;

namespace Utility.Shared
{
    public class ProductDto : Product 
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? ListPrice { get; set; }

    }
}
