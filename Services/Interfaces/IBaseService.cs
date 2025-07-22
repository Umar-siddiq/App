using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.EntityFramework;


namespace Services.Interfaces
{
    public interface IBaseService
    {
        AppDbContext _context { get; }
    }
}
