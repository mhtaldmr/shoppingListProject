using AutoMapper;
using ShoppingList.Application.ViewModels.Response.CategoryResponses;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, GetCategoryResponse>();
        }
    }
}
