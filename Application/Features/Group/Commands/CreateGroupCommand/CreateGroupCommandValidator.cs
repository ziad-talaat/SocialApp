using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Group.SharedValidators;
using Application.Resources.GroupResources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.Group.Commands.CreateGroupCommand
{
    internal sealed class CreateGroupCommandValidator:AbstractValidator<CreateGroupCommand>
    {
        private readonly IStringLocalizer<GroupValidationRes> _groupLocalizer;

        public CreateGroupCommandValidator(IStringLocalizer<GroupValidationRes> groupLocalizer)
        {
            _groupLocalizer = groupLocalizer;
           RuleFor(x=>x.createGroupDto).SetValidator(new CreateGroupDtoValidator(_groupLocalizer));
        }
    }
}
