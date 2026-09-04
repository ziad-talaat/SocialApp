using Application.Common.ResultPattern;
using Application.Features.Authentication.DTOS;
using MediatR;

namespace Application.Features.Authentication.Commands.Login
{
    public sealed record LoginCommand(LoginDto loggenDto):IRequest<Result<LoggedUserDto>>;
}
