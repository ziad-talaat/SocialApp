using FluentValidation;
namespace Application.Features.Profile.Commands.CreateProfile
{
    internal  class CreateProfileValidator:AbstractValidator<CreateProfileCommand>
    {
        public CreateProfileValidator()
        {
            RuleFor(x => x.profileDto.ProfileName).NotEmpty().WithMessage("profileName can't be blank")
                .MinimumLength(2).WithMessage("profileName is too short")
                .MaximumLength(50).WithMessage("profile Name i too long");

            RuleFor(x => x.profileDto.Description)
                .MaximumLength(1000)
                .WithMessage("Description is too long");
        }
    }
}