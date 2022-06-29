using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.UomResponses;
using System.Text;
using System.Text.Json;

namespace ShoppingList.Application.Features.UoMFeatures.Queries.GetAll
{
    public record GetAllUomsQuery : IRequest<Result<IEnumerable<GetUomResponse>>>;

    public class GetAllUomsQueryHandler : IRequestHandler<GetAllUomsQuery, Result<IEnumerable<GetUomResponse>>>
    {
        private const string _cacheKey = "UomListDistributed";
        private readonly IUomRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public GetAllUomsQueryHandler(IUomRepository repository, IMapper mapper, IDistributedCache cache)
            => (_repository, _mapper, _cache) = (repository, mapper, cache);

        public async Task<Result<IEnumerable<GetUomResponse>>> Handle(GetAllUomsQuery request, CancellationToken cancellationToken)
        {
            var uomCache = _cache.GetAsync(_cacheKey, cancellationToken).Result;
            if (uomCache is not null)
            {
                var jsonCategory = Encoding.UTF8.GetString(uomCache);
                var result = JsonSerializer.Deserialize<IEnumerable<GetUomResponse>>(jsonCategory);
                return Result.Success(result, "Successful");
            }
            else
            {
                var uom = await _repository.GetAll();
                if (uom is null)
                    throw new ArgumentNullException();

                var result = _mapper.Map<IEnumerable<GetUomResponse>>(uom);

                var cacheEntryOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                string jsonCategory = JsonSerializer.Serialize(uom);
                await _cache.SetAsync(_cacheKey, Encoding.UTF8.GetBytes(jsonCategory), cacheEntryOptions, cancellationToken);

                return Result.Success(result, "Successful");
            }
        }
    }
}