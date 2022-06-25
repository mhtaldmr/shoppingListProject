using AutoMapper;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Mappings
{
    internal class ListMappingProfile : Profile
    {
        public ListMappingProfile()
        {
            CreateMap<List, ListMappingProfile>();
        }
    }
}
