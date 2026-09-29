using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Profile.DTOS;
using FluentValidation;

namespace Application.Features.Profile.SharedValidator
{
    internal sealed  class ProfileDtoValidator:AbstractValidator<ProfileDto>
    {
        public ProfileDtoValidator()
        {
            RuleFor(x => x.ProfileName).NotEmpty().WithMessage("profileName can't be blank")
                .MinimumLength(2).WithMessage("profileName is too short")
                .MaximumLength(50).WithMessage("profile Name i too long");

            RuleFor(x => x.Bio)
                .MaximumLength(1000)
                .WithMessage("Description is too long");
        }
    }
}
