using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using ShoppingList.Client.Data;
using ShoppingList.Client.Data.UomResponse;

namespace ShoppingList.Client.Services.UnitOfMaterials
{
    public class UomService : IUomService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ProtectedLocalStorage _storage;

        public UomService(HttpClient httpClient, IHttpClientFactory httpClientFactory, ProtectedLocalStorage storage)
        {
            _httpClient = httpClient;
            _httpClientFactory = httpClientFactory;
            _storage = storage;
        }

        public async Task<BaseResponse<UomResponse>> GetAllUnits()
        {
            //taking the base url
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, _httpClient.BaseAddress + "api/unitofmaterials");

            //getting the token and writing it into header
            var token = await _storage.GetAsync<string>("token");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Value);

            //sending the request
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BaseResponse<UomResponse>>(jsonResponse);
            }

            return null;
        }
    }
}

