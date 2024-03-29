﻿using FluentValidation;
using ShoppingList.Application.Features.ListFeatures.Queries.GetById;

namespace ShoppingList.Application.Validators.ListQueryValidator
{
    public class GetListByIdQueryValidator : AbstractValidator<GetListByIdQuery>
    {
        public GetListByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Please specify a valid Id for List!")
                .NotNull().WithMessage("ListId can not be null!")
                .GreaterThanOrEqualTo(1).WithMessage("ListId must be valid!");
        }
    }
}