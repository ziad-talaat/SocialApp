
using FluentValidation;

namespace Application.Features.Authentication.Commands.Register
{
    internal sealed  class RegisterCommandValidator:AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.registerDto.Description)
            .MaximumLength(1000).WithMessage("too long message");

            RuleFor(x => x.registerDto.DateOfBirth)
               .LessThan(DateOnly.FromDateTime(DateTime.UtcNow))
               .WithMessage("date of birth should be in the past")
               .Must(BeAtLeast15).WithMessage("the minimum allowed age is 15");


            RuleFor(x => x.registerDto.ConfirmPassword)
                .Equal(x => x.registerDto.Password).WithMessage("confirm password doesn't match password");

        RuleFor(x => x.registerDto.Email)
                .NotEmpty().WithMessage("Email cant be empty")
                .EmailAddress().WithMessage("not valid email Address")
                .MaximumLength(50).WithMessage("Too long email");

            RuleFor(x => x.registerDto.ProfileName)
                .NotEmpty().WithMessage("profile name cant be blank")
                .MaximumLength(50).WithMessage("profile name is too long")
                 .MinimumLength(2).WithMessage("profile name is too short")
                 .Matches(@"^[A-Za-z0-9]+(?: [A-Za-z0-9]+)*$")
                 .WithMessage("Profile name can contain letters, numbers, and spaces.");




            RuleFor(x => x.registerDto.Country)
              .NotEmpty().WithMessage("Country can't be blank")
              .MaximumLength(50).WithMessage("country is too long")
               .MinimumLength(2).WithMessage("country name is too short");

            RuleFor(x => x.registerDto.Gender)
                .IsInEnum().WithMessage("Invalid Gender");

        }

        private static bool BeAtLeast15(DateOnly date)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var diffYears = today.Year - date.Year;

            if (date > today.AddYears(-diffYears))
                diffYears--;

            return diffYears >= 15;
        }
    }
}
