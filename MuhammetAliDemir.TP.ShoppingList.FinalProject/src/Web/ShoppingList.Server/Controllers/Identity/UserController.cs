using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.ViewModels.Request.UserViewModels;

namespace ShoppingList.Server.Controllers.Identity
{
    [Route("/users")]
    [ApiController]
    public class UserController : BaseApiController
    {
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpViewModel signup)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await SignUpService.SignUp(signup));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogInViewModel login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await LogInService.LogIn(login));
        }
    }
}