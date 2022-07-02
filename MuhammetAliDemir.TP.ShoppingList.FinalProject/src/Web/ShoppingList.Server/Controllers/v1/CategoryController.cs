using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Features.CategoryFeatures.Queries.GetAll;

namespace ShoppingList.Server.Controllers.v1
{
    [Route("api/categories")]
    [ApiController]
    [Authorize]
    public class CategoryController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
            => Ok(await Mediator.Send(new GetAllCategoriesQuery()));
    }
}
