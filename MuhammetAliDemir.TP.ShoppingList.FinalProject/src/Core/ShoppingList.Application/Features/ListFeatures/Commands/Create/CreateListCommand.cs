﻿using AutoMapper;
using MediatR;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Interfaces.Services.RepositoryServices;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.ListResponses;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Features.ListFeatures.Commands.Create
{
    public class CreateListCommand : IRequest<Result<GetListResponse>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; } = 1;
        public string UserId { get; set; }
        public ICollection<ListItemCreateViewModel> Items { get; set; }
    }

    public class CreateListCommandHandler : IRequestHandler<CreateListCommand, Result<GetListResponse>>
    {
        private readonly IListCreateService _listCreateService;
        public CreateListCommandHandler(IListCreateService listCreateService)
            => _listCreateService = listCreateService;

        public async Task<Result<GetListResponse>> Handle(CreateListCommand request, CancellationToken cancellationToken)
            => Result.Success(await _listCreateService.CreateList(request), "Successful");
    }
}