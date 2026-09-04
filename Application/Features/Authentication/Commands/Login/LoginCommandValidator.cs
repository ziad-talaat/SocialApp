using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Authentication.Commands.Login
{
    internal class LoginCommandValidator:AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.loggenDto.Email).NotEmpty().WithMessage("Email can't be blank")
                .EmailAddress().WithMessage("invalid email address format");
            RuleFor(x => x.loggenDto.Password).NotEmpty().WithMessage("password can't be blank");
        }
    }
}
