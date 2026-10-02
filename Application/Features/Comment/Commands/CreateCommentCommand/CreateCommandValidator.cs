using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Application.Features.Comment.Commands.CreateCommentCommand
{
    internal sealed class CreateCommandValidator:AbstractValidator<CreateCommentCommand>
    {
        public CreateCommandValidator(IStringLocalizer<CommentValidation> localizer,ILogger<CreateCommentCommand> _logger)
        {
            var s = localizer[nameof(CommentValidation.missingDescription)];
          _logger.LogInformation($"Found: {!s.ResourceNotFound}");
            _logger.LogInformation($"Searched: {s.SearchedLocation}");

            RuleFor(x => x.createCommentDto.Content).NotEmpty()
                //.WithMessage("you can't type an empty comment");
                .WithMessage(_ => localizer[nameof(CommentValidation.missingDescription)]);

            RuleFor(x => x.createCommentDto.PostId).NotEmpty().WithMessage("Post is UnExist");
        }
    }
}
