using FluentValidation;
using ShoppingList.Application.Features.ListFeatures.Commands.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Application.Validators.ListCommandValidator
{
    public class PatchListCommandValidator : AbstractValidator<PatchListCommand>
    {
        public PatchListCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Please specify a valid Id for List!")
                .NotNull().WithMessage("Id can not be null!")
                .NotEqual(0).WithMessage("ListId can not be Zero!");
            RuleFor(x => x.IsCompleted)
                .NotNull().WithMessage("IsCompleted can not be null!");
        }
    }
}
