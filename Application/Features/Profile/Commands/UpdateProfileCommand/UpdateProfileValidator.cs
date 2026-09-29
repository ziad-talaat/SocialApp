using Application.Features.Profile.SharedValidator;
using FluentValidation;
namespace Application.Features.Profile.Commands.UpdateProfileCommand
{
    internal class UpdateProfileValidator: AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileValidator()
        {
            RuleFor(x => x.profileDto).SetValidator(new ProfileDtoValidator());
            RuleFor(x=>x.profileDto.ProfileId).NotEmpty().WithMessage("invalid Profile ID");
        }
    }
}