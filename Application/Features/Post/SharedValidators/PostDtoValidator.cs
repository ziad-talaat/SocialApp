using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Post.Dtos;
using FluentValidation;

namespace Application.Features.Post.SharedValidators
{
    internal sealed class PostDtoValidator:AbstractValidator<PostDto>
    {
        public PostDtoValidator()
        {

            RuleFor(x => x).Must(x => !string.IsNullOrEmpty(x.Description) || x.ImagesUrl.Any() == true)
                .WithMessage("A Post must contain a description, at least one image,or both");

        }
    }
}
