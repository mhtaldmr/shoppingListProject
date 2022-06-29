using AutoMapper;
using MediatR;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.UomResponses;

namespace ShoppingList.Application.Features.UoMFeatures.Queries.GetAll
{
    public class GetAllUomsQuery : IRequest<Result<IEnumerable<GetUomResponse>>>
    {
    }

    public class GetAllUomsQueryHandler : IRequestHandler<GetAllUomsQuery, Result<IEnumerable<GetUomResponse>>>
    {
        private readonly IUomRepository _repository;
        private readonly IMapper _mapper;

        public GetAllUomsQueryHandler(IUomRepository repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

        public async Task<Result<IEnumerable<GetUomResponse>>> Handle(GetAllUomsQuery request, CancellationToken cancellationToken)
        {
            var uom = await _repository.GetAll();
            if (uom is null)
                throw new ArgumentNullException();

            var result = _mapper.Map<IEnumerable<GetUomResponse>>(uom);
            return Result.Success(result, "Successful");
        }
    }
}