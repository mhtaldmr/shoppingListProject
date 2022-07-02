using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.ViewModels.Response.BaseResponses;

namespace ShoppingList.Server.Controllers.v1
{
    [Route("api/listarchs")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class ListArchController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllLists()
            => Ok(Result.Success(await MongoDbService.GetAsync(), "Successful"));
    }
}