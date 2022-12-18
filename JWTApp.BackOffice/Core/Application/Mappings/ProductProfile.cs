using AutoMapper;
using JWTApp.BackOffice.Core.Domain;
using JWTApp.BackOffice.Core.DTOs;

namespace JWTApp.BackOffice.Core.Application.Mappings
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductListDto>().ReverseMap();
        }
    }
}
