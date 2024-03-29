﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Interfaces.DbContext;
using ShoppingList.Application.Interfaces.Services.UserServices;
using System.Security.Claims;

namespace ShoppingList.Server.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Creating this base controller and adding packages that we will use here
        /// to ensure that our other controllers inherit this class
        /// and decrease the size of the constructors
        /// beside this will provide a clean project.
        /// </summary>

        //List Controllers
        private IMediator _mediator;
        private IMapper _mapper;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();

        //Identity
        private IUserSignUpService _signUpService;
        private IUserLogInService _logInService;

        protected IUserSignUpService SignUpService => _signUpService ??= HttpContext.RequestServices.GetRequiredService<IUserSignUpService>();
        protected IUserLogInService LogInService => _logInService ??= HttpContext.RequestServices.GetRequiredService<IUserLogInService>();

        //MongoDB service
        private IMongoDbService _mongoDb;
        protected IMongoDbService MongoDbService => _mongoDb??= HttpContext.RequestServices.GetRequiredService<IMongoDbService>();

        //Current User ID
        private string _userId;
        protected string UserId => _userId ?? User.FindFirstValue(ClaimTypes.Name);

    }
}