using Microsoft.AspNetCore.Mvc;

namespace ShoppingList.Server.Controllers.v1
{
    [Route("api/listarch")]
    [ApiController]
    public class ListArchController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllLists()
            => Ok(await MongoDbService.GetAsync());
    }
}