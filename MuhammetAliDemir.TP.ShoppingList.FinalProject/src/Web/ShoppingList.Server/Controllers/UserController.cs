using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Server.Controllers
{
    [Route("/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepository;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpViewModel signup)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(signup.Email);
            if (user is not null)
                throw new Exception("There is a user with this email!");

            var newUser = new User()
            {
                Email = signup.Email,
                PasswordHash = signup.Password,
                UserName = signup.UserName,
                FirstName = signup.FirstName,
                LastName = signup.LastName,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            var IsCreated = await _userManager.CreateAsync(newUser, signup.Password);

            if (IsCreated.Succeeded)
                return Ok();

            return BadRequest(IsCreated.Errors);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _userManager.FindByEmailAsync(login.Email);

            if (existingUser == null)
                return BadRequest("User does not exist!");
            if (await _userManager.IsLockedOutAsync(existingUser))
                return BadRequest("User is Locked");


            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, login.Password);
            var singInResult = await _signInManager.CheckPasswordSignInAsync(existingUser, login.Password, false);

            if (!isCorrect)
            {
                await _userManager.AccessFailedAsync(existingUser);
                return BadRequest("Access Failed");
            }

            await _signInManager.SignInAsync(existingUser, false);

            return Ok();
        }

    }
}
