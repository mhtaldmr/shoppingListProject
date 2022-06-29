using AutoMapper;
using MediatR;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.CategoryResponses;

namespace ShoppingList.Application.Features.CategoryFeatures.Queries.GetAll
{
    public class GetAllCategoriesQuery : IRequest<Result<IEnumerable<GetCategoryResponse>>>
    {
    }

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<IEnumerable<GetCategoryResponse>>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(ICategoryRepository repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

        public async Task<Result<IEnumerable<GetCategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetAll();
            if (category is null)
                throw new ArgumentNullException();

            var result = _mapper.Map<IEnumerable<GetCategoryResponse>>(category);
            return Result.Success(result, "Successful");
        }
    }
}