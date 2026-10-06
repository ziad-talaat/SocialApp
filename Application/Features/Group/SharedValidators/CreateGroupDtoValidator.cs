using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Group.DTOS;
using Application.Resources.GroupResources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.Group.SharedValidators
{
    internal sealed class CreateGroupDtoValidator:AbstractValidator<CreateGroupDto>
    {
             private readonly IStringLocalizer<GroupValidationRes> _groupLocalizer;
        public CreateGroupDtoValidator(IStringLocalizer<GroupValidationRes> groupLocalizer)
        {
            _groupLocalizer = groupLocalizer;
            RuleFor(x => x.GroupName).NotEmpty()
                .WithMessage(_ => _groupLocalizer[nameof(GroupValidationRes.GroupNameEmpty)])
                .MaximumLength(200).WithMessage(_ => _groupLocalizer[nameof(GroupValidationRes.MaxGroupName)])
                .MinimumLength(2).WithMessage(_ => _groupLocalizer[nameof(GroupValidationRes.MinGroupName)]);
            RuleFor(x => x.Privacy).IsInEnum()
                .WithMessage(_ => _groupLocalizer[nameof(GroupValidationRes.InvalidPrivacy)]);
        }
    }
}
