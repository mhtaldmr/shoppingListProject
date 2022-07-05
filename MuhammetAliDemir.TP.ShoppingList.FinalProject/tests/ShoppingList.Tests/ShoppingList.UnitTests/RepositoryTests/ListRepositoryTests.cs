using AutoMapper;
using Moq;
using ShoppingList.Application.Features.ListFeatures.Commands.Create;
using ShoppingList.Application.Features.ListFeatures.Queries.GetAll;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Mappings;
using ShoppingList.Application.ViewModels.Response.ListResponses;
using ShoppingList.Domain.Entities;
using ShoppingList.Infrastructure.Services.RepositoryServices.ListServices;
using Xunit;

namespace ShoppingList.Tests.ShoppingList.UnitTests.RepositoryTests
{
    public class ListRepositoryTests
    {
        private readonly ListCreateService _listCreateService;
        private readonly ListGetAllService _listGetAllService;
        private readonly Mock<IListRepository> _listRepositoryMock = new();
        private readonly IMapper _mapper;

        public ListRepositoryTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new ListMappingProfile())));
            _listCreateService = new ListCreateService(_mapper, _listRepositoryMock.Object);
            _listGetAllService = new ListGetAllService(_listRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task CreateList_ShouldReturnsAList_AfterListCreated()
        {
            //Arrange
            var request = new CreateListCommand() { UserId = "1", Title = "List", CategoryId = 1, Description = "List" };
            var listToCreate = _mapper.Map<List>(request);

            _listRepositoryMock.Setup(u => u.Create(listToCreate)).Returns(() => null);

            //Act
            var list = await _listCreateService.CreateList(request);

            //Assert
            Assert.NotNull(list);
            Assert.Equal("List", list.Title);
        }

        [Fact]
        public async Task GetAll_ShouldReturnsAllList_WhenItsCalled()
        {
            //Arrange
            var request = new GetAllListsQuery() { UserId = "1" };
            List<List> listToGet = new()
            {
                new List() { UserId = "1", Id=1 },
                new List() { UserId = "1", Id=2 }
            };

            var response = _mapper.Map<ICollection<GetListResponse>>(listToGet);
            _listRepositoryMock.Setup(u => u.GetAllListsByUsers(request.UserId)).ReturnsAsync(() => listToGet);

            //Act
            var list = await _listGetAllService.GetAll(request);

            //Assert
            Assert.NotNull(list);
            Assert.Equal(response.Count, list.Count);
        }

        [Fact]
        public async Task GetAll_ShouldThrowsException_WhenThereIsNoList()
        {
            //Arrange
            var request = new GetAllListsQuery() { UserId = "1" };
            List<List> listToGet = new() { };

            var response = _mapper.Map<ICollection<GetListResponse>>(listToGet);
            _listRepositoryMock.Setup(u => u.GetAllListsByUsers(request.UserId)).ReturnsAsync(() => listToGet);

            //Act
            //Assert
            var exceptionMessage = await Assert.ThrowsAsync<KeyNotFoundException>(
                async () => await _listGetAllService.GetAll(request));
        }
    }
}
