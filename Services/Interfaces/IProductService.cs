using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Shared;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> getAllAsync();
    }
}
