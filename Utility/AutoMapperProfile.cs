using AutoMapper;
using Data.Entities;
using Utility.Shared;

namespace Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
           CreateMap<Product, ProductDto>();
        }
    }
}
