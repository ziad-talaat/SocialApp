using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Group.Commands.CreateGroupCommand;
using Application.Features.Group.SharedValidators;
using Application.Resources.GroupResources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.Group.Commands.UpdateGroupCommand
{
    internal sealed  class UpdateGroupCommandValidator:AbstractValidator<UpdateGroupCommand>
    {
        private readonly IStringLocalizer<GroupValidationRes> _groupLocalizer;
        public UpdateGroupCommandValidator(IStringLocalizer<GroupValidationRes> groupLocalizer)
        {
            _groupLocalizer = groupLocalizer;
            RuleFor(x => x.updateGroupDto).SetValidator(new CreateGroupDtoValidator(_groupLocalizer));
            RuleFor(x=>x.updateGroupDto.GroupId).NotEmpty().WithMessage(_ => _groupLocalizer[nameof(GroupValidationRes.InvalidGroupId)]);
        }
    }
}
