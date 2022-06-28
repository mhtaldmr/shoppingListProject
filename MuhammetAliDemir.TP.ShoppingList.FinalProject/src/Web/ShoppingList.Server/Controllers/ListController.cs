using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Extensions;
using ShoppingList.Application.Features.ListFeatures.Commands.Create;
using ShoppingList.Application.Features.ListFeatures.Commands.Delete;
using ShoppingList.Application.Features.ListFeatures.Commands.Update;
using ShoppingList.Application.Features.ListFeatures.Queries.GetAll;
using ShoppingList.Application.Features.ListFeatures.Queries.GetById;
using ShoppingList.Application.ViewModels.Request.ListViewModels;

namespace ShoppingList.Server.Controllers
{
    [Route("/lists")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ListController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ListController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllLists()
            => Ok(await _mediator.Send(new GetAllListsQuery()));


        [HttpGet("{id}")]
        public async Task<IActionResult> GetListById(int id)
            => Ok(await _mediator.Send(new GetListByIdQuery() { Id = id }));


        [HttpPut]
        public async Task<IActionResult> UpdateList([FromBody] UpdateListCommand command)
            => Ok(await _mediator.Send(command));


        [HttpPatch]
        public async Task<IActionResult> PatchListToCompleted([FromBody] PatchListCommand command)
            => Ok(await _mediator.Send(command));


        [HttpPost]
        public async Task<IActionResult> CreateList([FromBody] ListViewModel command)
        {
            var result = _mapper.Map<ListViewModel, CreateListCommand>(command);
            result.UserId = HttpContext.GetUserId();
            return Ok(await _mediator.Send(result));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            await _mediator.Send(new DeleteListCommand() { Id = id });
            return NoContent();
        }
    }
}