
using Application.Common.ResultPattern;
using Application.Features.Authentication.DTOS;
using MediatR;

namespace Application.Features.Authentication.Commands.Register
{
    public sealed record RegisterCommand(RegisterDto registerDto):IRequest<Result<LoggedUserDto>>;
   
}
