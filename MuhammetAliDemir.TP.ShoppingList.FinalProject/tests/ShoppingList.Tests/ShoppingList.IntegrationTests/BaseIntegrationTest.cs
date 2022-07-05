using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Application.ViewModels.Request.UserViewModels;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.ListResponses;
using ShoppingList.Application.ViewModels.Response.TokenResponses;
using ShoppingList.Infrastructure.Persistence.DbContext;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ShoppingList.Tests.ShoppingList.IntegrationTests
{

    public class BaseIntegrationTest
    {
        protected readonly HttpClient _testClient;
        protected BaseIntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.AddControllers()
                            .AddApplicationPart(typeof(Program).Assembly);
                        services.RemoveAll(typeof(ShoppingListDbContext));
                        services.AddDbContext<ShoppingListDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("IntegrationTestDb");
                        });
                    });
                });
            _testClient = appFactory.CreateClient();
            _testClient.BaseAddress = new Uri("https://localhost:5070");
        }

        protected async Task AuthenticateAsync()
        {
            _testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJwtTokenAsync());
        }


        protected async Task<GetListResponse> CreateListAsync(ListCreateViewModel request)
        {
            var response = await _testClient.PostAsJsonAsync("/api/lists", request);
            var result = await response.Content.ReadAsAsync<Result<GetListResponse>>();
            return result.Data;
        }


        private async Task<string> GetJwtTokenAsync()
        {
            await _testClient.PostAsJsonAsync(":/users/signup", new UserSignUpViewModel
            {
                FirstName = "mali",
                LastName = "demir",
                UserName = "mdemir",
                Email = "test@integration.com",
                Password = "MySuperSecretPassword123!."
            });

            var response = await _testClient.PostAsJsonAsync("/users/login", new UserLogInViewModel
            {
                Email = "test@integration.com",
                Password = "MySuperSecretPassword123!."
            });

            var signUpResponse = await response.Content.ReadAsAsync<Result<TokenResponse>>();
            return signUpResponse.Data.AccessToken;
        }
    }
}
