using AutoMapper;
using ShoppingList.Application.ViewModels.Response.UomResponses;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Mappings
{
    public class UomMappingProfile : Profile
    {
        public UomMappingProfile()
        {
            CreateMap<UnitOfMaterial, GetUomResponse>();
        }
    }
}
