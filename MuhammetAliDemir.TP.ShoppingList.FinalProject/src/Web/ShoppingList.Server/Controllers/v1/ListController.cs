using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Features.ListFeatures.Commands.Create;
using ShoppingList.Application.Features.ListFeatures.Commands.Delete;
using ShoppingList.Application.Features.ListFeatures.Commands.Update;
using ShoppingList.Application.Features.ListFeatures.Queries.GetAll;
using ShoppingList.Application.Features.ListFeatures.Queries.GetAllByFilter;
using ShoppingList.Application.Features.ListFeatures.Queries.GetById;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using System.Security.Claims;

namespace ShoppingList.Server.Controllers.v1
{
    [Route("api/lists")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class ListController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllLists()
            => Ok(await Mediator.Send(new GetAllListsQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetListById(int id)
            => Ok(await Mediator.Send(new GetListByIdQuery() { Id = id }));


        [HttpGet("search")]
        public async Task<IActionResult> GetAllListsByFilter([FromQuery] GetAllByFilterQuery query)
            => Ok(await Mediator.Send(query));


        [HttpPut]
        public async Task<IActionResult> UpdateList([FromBody] UpdateListCommand command)
            => Ok(await Mediator.Send(command));


        [HttpPatch]
        public async Task<IActionResult> PatchListToCompleted([FromBody] PatchListCommand command)
            => Ok(await Mediator.Send(command));


        [HttpPost]
        public async Task<IActionResult> CreateList([FromBody] ListCreateViewModel command)
        {
            var result = Mapper.Map<ListCreateViewModel, CreateListCommand>(command);
            result.UserId = User.FindFirstValue(ClaimTypes.Name);
            return Ok(await Mediator.Send(result));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
            => Ok(await Mediator.Send(new DeleteListCommand() { Id = id }));
    }
}