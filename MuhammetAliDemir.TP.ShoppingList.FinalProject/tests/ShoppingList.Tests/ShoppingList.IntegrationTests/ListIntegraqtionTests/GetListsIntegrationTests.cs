using FluentAssertions;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.ListResponses;
using Xunit;

namespace ShoppingList.Tests.ShoppingList.IntegrationTests.ListIntegraqtionTests
{
    public class GetListsIntegrationTests : BaseIntegrationTest
    {
        [Theory]
        [InlineData("api/lists")]
        [InlineData("api/lists/search")]
        [InlineData("api/categories")]
        [InlineData("api/unitofmaterials")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            await AuthenticateAsync();
            // Act
            var response = await _testClient.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }



        [Fact]
        public async Task CreateList_AddsAListToDB_ReturnsListResponse()
        {
            //Arrange
            await AuthenticateAsync();

            var createdList = await CreateListAsync(new ListCreateViewModel
            {
                CategoryId = 1,
                Description = "Test Description",
                Title = "Test Title",
                Items = new List<ListItemCreateViewModel>()
                {
                    new ListItemCreateViewModel(){ UoMId = 1, Name = "item" , Quantity = 1}
                }
            });

            //Act
            var response = await _testClient.GetAsync($"api/lists/{createdList.Id}");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var result = await response.Content.ReadAsAsync<Result<GetListResponse>>();
            result.Data.Should().NotBeNull();
            result.Data.Title.Should().BeEquivalentTo(createdList.Title);
        }


        [Fact]
        public async Task GetAllLists_ReturnAListOfLists_WhenDBIsNotEmpty()
        {
            //Arrange
            await AuthenticateAsync();

            //Act
            var response = await _testClient.GetAsync("api/lists");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var result = await response.Content.ReadAsAsync<Result<IEnumerable<GetListResponse>>>();
            result.Should().NotBeNull();
            result.Data.Count().Should().BeGreaterThan(0);
        }
    }
}