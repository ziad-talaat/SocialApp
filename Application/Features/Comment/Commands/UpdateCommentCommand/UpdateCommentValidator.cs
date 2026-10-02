using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Comment.Commands.UpdateCommentCommand
{
    internal sealed class UpdateCommentValidator:AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentValidator()
        {
            RuleFor(x=>x.Content).NotEmpty().WithMessage("Cant add an empty Comment");
        }
    }
}
