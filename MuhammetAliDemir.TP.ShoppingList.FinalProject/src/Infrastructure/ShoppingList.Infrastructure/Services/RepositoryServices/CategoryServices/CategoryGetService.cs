using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using ShoppingList.Application.Features.CategoryFeatures.Queries.GetAll;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.CategoryServices;
using ShoppingList.Application.ViewModels.Response.CategoryResponses;
using System.Text;
using System.Text.Json;

namespace ShoppingList.Infrastructure.Services.RepositoryServices.CategoryServices
{
    public class CategoryGetService : ICategoryGetService
    {
        private const string _cacheKey = "CategoryListDistributed";
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public CategoryGetService(ICategoryRepository repository, IMapper mapper, IDistributedCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<IEnumerable<GetCategoryResponse>> GetAllCategory(GetAllCategoriesQuery request)
        {
            var categoryCache = _cache.GetAsync(_cacheKey).Result;
            if (categoryCache is not null)
            {
                var jsonCategory = Encoding.UTF8.GetString(categoryCache);
                return JsonSerializer.Deserialize<IEnumerable<GetCategoryResponse>>(jsonCategory);
            }
            else
            {
                var category = await _repository.GetAll();
                if (category is null)
                    throw new ArgumentNullException();


                var cacheEntryOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                string jsonCategory = JsonSerializer.Serialize(category);
                await _cache.SetAsync(_cacheKey, Encoding.UTF8.GetBytes(jsonCategory), cacheEntryOptions);

                return _mapper.Map<IEnumerable<GetCategoryResponse>>(category);
            }
        }
    }
}