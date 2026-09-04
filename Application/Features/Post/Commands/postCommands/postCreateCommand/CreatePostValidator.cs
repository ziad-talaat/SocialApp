using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Post.Commands.postCommands.postCreateCommand
{
    internal sealed class CreatePostValidator:AbstractValidator<CreatePostCommand>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.title).NotEmpty().WithMessage("Title should not be empty");        
        }
    }
}
