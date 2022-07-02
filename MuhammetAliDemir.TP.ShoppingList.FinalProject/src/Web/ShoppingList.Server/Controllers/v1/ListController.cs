using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Features.ListFeatures.Commands.Create;
using ShoppingList.Application.Features.ListFeatures.Commands.Delete;
using ShoppingList.Application.Features.ListFeatures.Commands.Update;
using ShoppingList.Application.Features.ListFeatures.Queries.GetAll;
using ShoppingList.Application.Features.ListFeatures.Queries.GetAllByFilter;
using ShoppingList.Application.Features.ListFeatures.Queries.GetById;
using ShoppingList.Application.ViewModels.Request.FilterViewModels;
using ShoppingList.Application.ViewModels.Request.ListViewModels;

namespace ShoppingList.Server.Controllers.v1
{
    [Route("api/lists")]
    [ApiController]
    [Authorize]
    public class ListController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllLists()
            => Ok(await Mediator.Send(new GetAllListsQuery() { UserId = UserId }));


        [HttpGet("{id}")]
        public async Task<IActionResult> GetListById(int id)
            => Ok(await Mediator.Send(new GetListByIdQuery() { Id = id, UserId = UserId }));


        [HttpGet("search")]
        public async Task<IActionResult> GetAllListsByFilter([FromQuery] FilterViewModel query)
        {
            var result = Mapper.Map<GetAllByFilterQuery>(query);
            result.UserId = UserId;
            return Ok(await Mediator.Send(result));
        }


        [HttpPost]
        public async Task<IActionResult> CreateList([FromBody] ListCreateViewModel command)
        {
            var result = Mapper.Map<CreateListCommand>(command);
            result.UserId = UserId;
            return Ok(await Mediator.Send(result));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateList([FromBody] ListUpdateViewModel command)
        {
            var result = Mapper.Map<UpdateListCommand>(command);
            result.UserId = UserId;
            return Ok(await Mediator.Send(result));
        }


        [HttpPatch]
        public async Task<IActionResult> PatchListToCompleted([FromBody] ListPatchViewModel command)
        {
            var result = Mapper.Map<PatchListCommand>(command);
            result.UserId = UserId;
            return Ok(await Mediator.Send(result));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
            => Ok(await Mediator.Send(new DeleteListCommand() { Id = id, UserId = UserId }));

    }
}