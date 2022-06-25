using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Extensions;
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
        private readonly RoleManager<IdentityRole> _roleManager;

        public ListController(IListRepository repository, UserManager<User> userManager,IMapper mapper,RoleManager<IdentityRole> roleManager)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateList([FromBody] ListCreateViewModel list)
        {
            var newList = new List
            {
                Description = list.Description, Title= list.Title, UserId= HttpContext.GetUserId()
            };

            _repository.Create(newList);
            return Ok(newList);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllLists()
        {
            return Ok(await _repository.GetAll());
        }

    }
}