using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Features.UoMFeatures.Queries.GetAll;

namespace ShoppingList.Server.Controllers.v1
{
    [Route("api/unitofmaterials")]
    [ApiController]
    public class UoMController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllUoms()
            => Ok(await Mediator.Send(new GetAllUomsQuery()));
    }
}