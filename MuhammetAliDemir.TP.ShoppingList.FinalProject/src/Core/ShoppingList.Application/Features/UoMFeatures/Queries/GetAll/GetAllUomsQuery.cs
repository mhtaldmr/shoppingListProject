using MediatR;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.UomServices;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.UomResponses;

namespace ShoppingList.Application.Features.UoMFeatures.Queries.GetAll
{
    public record GetAllUomsQuery : IRequest<Result<IEnumerable<GetUomResponse>>>;

    public class GetAllUomsQueryHandler : IRequestHandler<GetAllUomsQuery, Result<IEnumerable<GetUomResponse>>>
    {
        private readonly IUomGetService _uomGetService;
        public GetAllUomsQueryHandler(IUomGetService uomGetService)
            => _uomGetService = uomGetService;

        public async Task<Result<IEnumerable<GetUomResponse>>> Handle(GetAllUomsQuery request, CancellationToken cancellationToken)
            => Result.Success(await _uomGetService.GetAllUom(request), "Successful");
    }
}