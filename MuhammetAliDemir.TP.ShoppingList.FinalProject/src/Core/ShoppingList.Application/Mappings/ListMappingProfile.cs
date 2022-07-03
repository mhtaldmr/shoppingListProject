using AutoMapper;
using ShoppingList.Application.Features.ListFeatures.Commands.Create;
using ShoppingList.Application.Features.ListFeatures.Commands.Update;
using ShoppingList.Application.Features.ListFeatures.Queries.GetAllByFilter;
using ShoppingList.Application.ViewModels.Request.FilterViewModels;
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
            CreateMap<ListItemCreateViewModel, Item>().ReverseMap();
            CreateMap<ListItemUpdateViewModel, Item>().ReverseMap();
            CreateMap<Item, ListItemResponse>()
                .ForMember(x => x.UoMCode, opt => opt.MapFrom(z => z.UoM.UoMCode))
                .ReverseMap();

            //Mapping the user input to domain
            CreateMap<ListViewModel, List>().ReverseMap();

            //Mapping the domain to user response
            CreateMap<List, GetListResponse>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(z => z.Category.Name))
                .ReverseMap();
            CreateMap<List, GetListResponseMessage>();

            //Mapping the user input to domain
            CreateMap<CreateListCommand, List>().ReverseMap();

            //Map the user view model into command
            CreateMap<ListViewModel, CreateListCommand>().ReverseMap();
            CreateMap<ListCreateViewModel, CreateListCommand>().ReverseMap();
            CreateMap<ListUpdateViewModel, UpdateListCommand>().ReverseMap();
            CreateMap<ListPatchViewModel, PatchListCommand>().ReverseMap();

            //map the filter view model to query
            CreateMap<FilterViewModel, GetAllByFilterQuery>().ReverseMap();
        }
    }
}