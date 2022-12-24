using AutoMapper;
using JWTApp.BackOffice.Core.Domain;
using JWTApp.BackOffice.Core.DTOs;

namespace JWTApp.BackOffice.Core.Application.Mappings
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryListDTO>().ReverseMap();
        }
    }
}
