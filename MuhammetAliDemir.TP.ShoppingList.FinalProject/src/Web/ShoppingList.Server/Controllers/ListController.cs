using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Extensions;
using ShoppingList.Application.Features.ListFeatures.Commands.Create;
using ShoppingList.Application.Features.ListFeatures.Commands.Delete;
using ShoppingList.Application.Features.ListFeatures.Commands.Update;
using ShoppingList.Application.Features.ListFeatures.Queries.GetAll;
using ShoppingList.Application.Features.ListFeatures.Queries.GetById;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Server.Controllers
{
    [Route("/lists")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ListController : ControllerBase
    {
        private readonly IListRepository _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ListController(IListRepository repository, IMediator mediator, IMapper mapper)
        {
            _repository = repository;
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllLists()
            => Ok(await _mediator.Send(new GetAllListsQuery()));


        [HttpGet("{id}")]
        public async Task<IActionResult> GetListById(int id)
            => Ok(await _mediator.Send(new GetListByIdQuery() { Id = id }));


        [HttpPost]
        public async Task<IActionResult> CreateList([FromBody] ListViewModel command)
        {
            var result = _mapper.Map<ListViewModel, CreateListCommand>(command);
            result.UserId = HttpContext.GetUserId();
            return Ok(await _mediator.Send(result));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateList([FromBody] UpdateListCommand command)
            => Ok(await _mediator.Send(command));


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            await _mediator.Send(new DeleteListCommand() { Id = id });
            return NoContent();
        }







        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateListByJsonPatch(int id, [FromBody] JsonPatchDocument<List> list)
        {
            var listToUpdate = await _repository.GetById(id);

            if (listToUpdate is null)
                return NotFound($"This List with id = {id} doesnt exist!");

            //To apply the changes
            list.ApplyTo(listToUpdate);

            listToUpdate.UpdatedAt = DateTime.Now;
            listToUpdate.CompletedAt = DateTime.Now;

            _repository.Update(listToUpdate);
            return Ok(listToUpdate); //Http 200
        }


    }
}