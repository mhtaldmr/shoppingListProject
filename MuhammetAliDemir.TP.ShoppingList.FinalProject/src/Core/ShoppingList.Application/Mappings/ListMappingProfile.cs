using AutoMapper;
using ShoppingList.Application.Features.ListFeatures.Commands.Create;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Application.ViewModels.Response.ListResponse;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Mappings
{
    internal class ListMappingProfile : Profile
    {
        public ListMappingProfile()
        {
            //Mapping the items in the lists
            CreateMap<ListItemViewModel, Item>().ReverseMap();

            //Mapping the user input to domain
            CreateMap<ListViewModel, List>()
                .ForMember(x => x.Items, opt => opt.MapFrom(z => z.ListItems));


            //Mapping the domain to user response
            CreateMap<List, GetListResponse>()
                .ForMember(x => x.ListItems, opt => opt.MapFrom(z => z.Items));

            //Mapping the user input to domain
            CreateMap<ListItemViewModel, Item>().ReverseMap();
            CreateMap<CreateListCommand, List>()
                .ForMember(x => x.Items, opt => opt.MapFrom(z => z.ListItems));

            //Map
            CreateMap<ListItemViewModel, Item>().ReverseMap();
            CreateMap<ListViewModel, CreateListCommand > ()
                .ForMember(x => x.ListItems, opt => opt.MapFrom(z => z.ListItems));

        }
    }
}