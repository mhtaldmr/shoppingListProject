using AutoMapper;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Mappings
{
    internal class ListMappingProfile : Profile
    {
        public ListMappingProfile()
        {

            CreateMap<ListItemViewModel, Item>();
            CreateMap<ListViewModel, List>()
                .ForMember(x => x.Items, opt => opt.MapFrom(z => z.ListItemViewModel));

        }
    }
}