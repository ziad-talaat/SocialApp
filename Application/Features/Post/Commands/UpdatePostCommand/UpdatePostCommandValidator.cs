using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Post.SharedValidators;
using FluentValidation;

namespace Application.Features.Post.Commands.UpdateCommand
{
    internal sealed class UpdatePostCommandValidator:AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(x => x.postDto).SetValidator(new PostDtoValidator());
            RuleFor(x => x.postDto.PostId).NotEmpty().WithMessage("invlaild post ID");
        }
    }
}
