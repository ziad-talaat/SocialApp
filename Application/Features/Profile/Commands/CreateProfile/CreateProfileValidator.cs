using Application.Features.Profile.Commands.SharedValidator;
using FluentValidation;
namespace Application.Features.Profile.Commands.CreateProfile
{
    internal  class CreateProfileValidator:AbstractValidator<CreateProfileCommand>
    {
        public CreateProfileValidator()
        {
            RuleFor(x => x.profileDto).SetValidator(new ProfileDtoValidator());
        }
    }
}