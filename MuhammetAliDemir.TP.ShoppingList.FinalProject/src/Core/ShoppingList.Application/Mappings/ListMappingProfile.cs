using AutoMapper;
using ShoppingList.Application.Features.ListFeatures.Commands.Create;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Application.ViewModels.Response.ListResponses;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Mappings
{
    internal class ListMappingProfile : Profile
    {
        public ListMappingProfile()
        {
            //Mapping the items in the lists
            CreateMap<ListItemViewModel, Item>().ReverseMap();
            CreateMap<ListItemActionViewModel, Item>().ReverseMap();

            //Mapping the user input to domain
            CreateMap<ListViewModel, List>().ReverseMap();

            //Mapping the domain to user response
            CreateMap<List, GetListResponse>().ReverseMap();

            //Mapping the user input to domain
            CreateMap<CreateListCommand, List>().ReverseMap();

            //Map the user view model into command
            CreateMap<ListViewModel, CreateListCommand>().ReverseMap();

        }
    }
}