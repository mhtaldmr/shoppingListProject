using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.ViewModels.Response.BaseResponses;

namespace ShoppingList.Server.Controllers.v1
{
    [Route("api/listarch")]
    [ApiController]
    public class ListArchController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllLists()
            => Ok(Result.Success(await MongoDbService.GetAsync(), "Successful"));
    }
}