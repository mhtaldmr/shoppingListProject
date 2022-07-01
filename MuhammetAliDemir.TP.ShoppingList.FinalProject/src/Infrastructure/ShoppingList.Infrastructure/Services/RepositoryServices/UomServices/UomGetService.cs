using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using ShoppingList.Application.Features.UoMFeatures.Queries.GetAll;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.UomServices;
using ShoppingList.Application.ViewModels.Response.UomResponses;
using System.Text;
using System.Text.Json;

namespace ShoppingList.Infrastructure.Services.RepositoryServices.UomServices
{
    public class UomGetService : IUomGetService
    {
        private const string _cacheKey = "UomListDistributed";
        private readonly IUomRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public UomGetService(IUomRepository repository, IMapper mapper, IDistributedCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<IEnumerable<GetUomResponse>> GetAllUom(GetAllUomsQuery request)
        {
            var uomCache = _cache.GetAsync(_cacheKey).Result;
            if (uomCache is not null)
            {
                var jsonCategory = Encoding.UTF8.GetString(uomCache);
                return JsonSerializer.Deserialize<IEnumerable<GetUomResponse>>(jsonCategory);
            }
            else
            {
                var uom = await _repository.GetAll();
                if (uom is null)
                    throw new ArgumentNullException();


                var cacheEntryOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                string jsonCategory = JsonSerializer.Serialize(uom);
                await _cache.SetAsync(_cacheKey, Encoding.UTF8.GetBytes(jsonCategory), cacheEntryOptions);

                return _mapper.Map<IEnumerable<GetUomResponse>>(uom);
            }
        }
    }
}