using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Interfaces.Services.UserServices;
using ShoppingList.Application.ViewModels.Request.UserViewModels;


namespace ShoppingList.Server.Controllers
{
    [Route("/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserSignUpService _signUpService;
        private readonly IUserLogInService _logInService;

        public UserController(IUserSignUpService signUpService,IUserLogInService logInService)
        {
            _signUpService = signUpService;
            _logInService = logInService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpViewModel signup)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _signUpService.SignUp(signup);
            return Ok();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogInViewModel login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _logInService.LogIn(login);
            return Ok();
        }

    }
}