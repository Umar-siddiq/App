using Data.EntityFramework;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    
    public abstract class BaseService : IBaseService
    {
        public AppDbContext Context {  get; }

        protected BaseService (AppDbContext context) 
        {
            Context = context;
        }
    }
}
