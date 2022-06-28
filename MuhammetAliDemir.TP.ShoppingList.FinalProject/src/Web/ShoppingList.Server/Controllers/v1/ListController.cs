using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Extensions;
using ShoppingList.Application.Features.ListFeatures.Commands.Create;
using ShoppingList.Application.Features.ListFeatures.Commands.Delete;
using ShoppingList.Application.Features.ListFeatures.Commands.Update;
using ShoppingList.Application.Features.ListFeatures.Queries.GetAll;
using ShoppingList.Application.Features.ListFeatures.Queries.GetById;
using ShoppingList.Application.ViewModels.Request.ListViewModels;

namespace ShoppingList.Server.Controllers.v1
{
    [Route("/lists")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ListController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllLists()
            => Ok(await Mediator.Send(new GetAllListsQuery()));


        [HttpGet("{id}")]
        public async Task<IActionResult> GetListById(int id)
            => Ok(await Mediator.Send(new GetListByIdQuery() { Id = id }));


        [HttpPut]
        public async Task<IActionResult> UpdateList([FromBody] UpdateListCommand command)
            => Ok(await Mediator.Send(command));


        [HttpPatch]
        public async Task<IActionResult> PatchListToCompleted([FromBody] PatchListCommand command)
            => Ok(await Mediator.Send(command));


        [HttpPost]
        public async Task<IActionResult> CreateList([FromBody] ListViewModel command)
        {
            var result = Mapper.Map<ListViewModel, CreateListCommand>(command);
            result.UserId = HttpContext.GetUserId();
            return Ok(await Mediator.Send(result));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            await Mediator.Send(new DeleteListCommand() { Id = id });
            return NoContent();
        }
    }
}