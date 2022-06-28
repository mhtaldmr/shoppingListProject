using AutoMapper;
using MediatR;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Response.ListResponse;
using ShoppingList.Application.ViewModels.Response.MainResponse;

namespace ShoppingList.Application.Features.ListFeatures.Commands.Update
{
    public class PatchListCommand : IRequest<Result<GetListResponse>>
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class PatchListCommandHandler : IRequestHandler<PatchListCommand, Result<GetListResponse>>
    {
        private readonly IListRepository _repository;
        private readonly IMapper _mapper;

        public PatchListCommandHandler(IListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result<GetListResponse>> Handle(PatchListCommand request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetById(request.Id);
            if (list is null)
                return Result.Fail(new GetListResponse(), new KeyNotFoundException().Message);

            list.IsCompleted = request.IsCompleted;
            list.CompletedAt = DateTime.Now;
            list.UpdatedAt = DateTime.Now;

            //Update the completed field
            await _repository.Update(list);
            //return the result
            var result = _mapper.Map<GetListResponse>(list);
            return Result.Success(result, "Successful");
        }
    }
}
