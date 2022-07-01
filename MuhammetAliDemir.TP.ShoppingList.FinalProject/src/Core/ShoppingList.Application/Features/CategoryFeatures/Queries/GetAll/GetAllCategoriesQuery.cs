using MediatR;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.CategoryServices;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.CategoryResponses;

namespace ShoppingList.Application.Features.CategoryFeatures.Queries.GetAll
{
    public record GetAllCategoriesQuery : IRequest<Result<IEnumerable<GetCategoryResponse>>>;

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<IEnumerable<GetCategoryResponse>>>
    {
        private readonly ICategoryGetService _categoryGetService;
        public GetAllCategoriesQueryHandler(ICategoryGetService categoryGetService)
            => _categoryGetService = categoryGetService;

        public async Task<Result<IEnumerable<GetCategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
            => Result.Success(await _categoryGetService.GetAllCategory(request), "Successful");
    }
}