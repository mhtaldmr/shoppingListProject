using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using ShoppingList.Application.Features.CategoryFeatures.Queries.GetAll;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Mappings;
using ShoppingList.Application.ViewModels.Response.CategoryResponses;
using ShoppingList.Domain.Entities;
using ShoppingList.Infrastructure.Services.RepositoryServices.CategoryServices;
using System.Text;
using System.Text.Json;
using Xunit;

namespace ShoppingList.Tests.ShoppingList.UnitTests.RepositoryTests
{
    public class CategoryRepositoryTests
    {
        private const string _cacheKey = "CategoryListDistributed";
        private readonly CategoryGetService _categoryGetService;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock = new();
        private readonly Mock<IDistributedCache> _cacheMock = new();
        private readonly IMapper _mapper;

        public CategoryRepositoryTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new CategoryMappingProfile())));
            _categoryGetService = new CategoryGetService(_categoryRepositoryMock.Object, _mapper, _cacheMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnsAllList_WhenItsCalled()
        {
            //Arrange
            var request = new GetAllCategoriesQuery();
            List<Category> categoryToGet = new()
            {
                new Category() { Name= "Category1", Id=1 },
                new Category() { Name= "Category2", Id=2 }
            };

            var response = _mapper.Map<ICollection<GetCategoryResponse>>(categoryToGet);
            _categoryRepositoryMock.Setup(u => u.GetAll()).ReturnsAsync(() => categoryToGet);

            byte[] cache = null;
            _cacheMock.Setup(c => c.GetAsync(_cacheKey,default).Result).Returns(() => cache);

            //Act
            var category = await _categoryGetService.GetAllCategory(request);

            //Assert
            Assert.NotNull(category);
            Assert.Equal(2, category.Count());
        }


    }
}
