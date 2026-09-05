using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Post.SharedValidators;
using FluentValidation;

namespace Application.Features.Post.Commands.CreatePost
{
    internal class CreatePostValidator:AbstractValidator<CreatePostCommand>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.postDto).SetValidator(new PostDtoValidator());
        }
    }
}
