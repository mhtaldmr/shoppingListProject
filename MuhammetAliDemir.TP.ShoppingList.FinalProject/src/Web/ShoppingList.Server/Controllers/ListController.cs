using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Server.Controllers
{
    [Route("/lists")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IListRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ListController(IListRepository repository, UserManager<User> userManager,IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
        }


        [HttpPost("create")]
        public IActionResult CreateList([FromBody] ListCreateViewModel list)
        {

            Console.WriteLine(User.Identity.IsAuthenticated);
            Console.WriteLine(User.Claims);
            Console.WriteLine(User.Identities.Where(x => 1 ==1));
            Console.WriteLine(User.Identity.GetType());

            Console.WriteLine(User.Identity.GetType());

            var newList = new List
            {
                Description = list.Description, Title= list.Title, UserId= "0b3c3cc3-9ef2-4e75-a75b-684248ddc72c"
            };

            _repository.Create(newList);
            return Ok(newList);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllLists()
        {
            return Ok(await _repository.GetAll());
        }

    }
}