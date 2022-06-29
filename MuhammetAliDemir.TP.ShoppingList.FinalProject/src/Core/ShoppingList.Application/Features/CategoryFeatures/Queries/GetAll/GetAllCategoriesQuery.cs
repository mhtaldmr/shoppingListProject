using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.CategoryResponses;
using System.Text;
using System.Text.Json;

namespace ShoppingList.Application.Features.CategoryFeatures.Queries.GetAll
{
    public record GetAllCategoriesQuery : IRequest<Result<IEnumerable<GetCategoryResponse>>>;

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<IEnumerable<GetCategoryResponse>>>
    {
        private const string _cacheKey = "CategoryListDistributed";
        private readonly IDistributedCache _cache;
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(ICategoryRepository repository, IMapper mapper, IDistributedCache cache)
            => (_repository, _mapper, _cache) = (repository, mapper, cache);

        public async Task<Result<IEnumerable<GetCategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categoryCache = _cache.GetAsync(_cacheKey, cancellationToken).Result;
            if (categoryCache is not null)
            {
                var jsonCategory = Encoding.UTF8.GetString(categoryCache);
                var result = JsonSerializer.Deserialize<IEnumerable<GetCategoryResponse>>(jsonCategory);
                return Result.Success(result, "Successful");
            }
            else
            {
                var category = await _repository.GetAll();
                if (category is null)
                    throw new ArgumentNullException();

                var result = _mapper.Map<IEnumerable<GetCategoryResponse>>(category);

                var cacheEntryOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                string jsonCategory = JsonSerializer.Serialize(category);
                await _cache.SetAsync(_cacheKey, Encoding.UTF8.GetBytes(jsonCategory), cacheEntryOptions, cancellationToken);

                return Result.Success(result, "Successful");
            }
        }
    }
}