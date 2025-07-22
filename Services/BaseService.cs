using AutoMapper;
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
        public AppDbContext _context {  get; }
        public readonly IMapper _mapper;


        protected BaseService (AppDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;

        }
    }
}
