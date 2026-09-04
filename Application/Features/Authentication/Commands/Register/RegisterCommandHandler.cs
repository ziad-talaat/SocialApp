
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Application.Features.Authentication.Commands.Login;
using Application.Features.Authentication.DTOS;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Authentication.Commands.Register
{
    internal sealed class RegisterCommandHandler(IAuthanticationService _authService, UserManager<AppUser> _userManager) : IRequestHandler<RegisterCommand, Result<LoggedUserDto>>
    {
        public async  Task<Result<LoggedUserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.FindByEmailAsync(request.registerDto.Email);
            if (user is not null)
                return Result<LoggedUserDto>.Failure(new Error("Email is taken"));

          
            user = new AppUser
            {
                Email = request.registerDto.Email,
                Country = request.registerDto.Country,
                Gender = request.registerDto.Gender,
                Description = request.registerDto.Description,
                UserName = request.registerDto.ProfileName,
            };

            var result=  await _userManager.CreateAsync(user,request.registerDto.Password);
            if (!result.Succeeded)
            {
                return Result<LoggedUserDto>.Failure(new Error(result.Errors.FirstOrDefault()?.Description ?? "User Creation failed try later"));
            }

           var tokenModel= await _authService.CreateJWTToken(user);
            return Result<LoggedUserDto>.Success(tokenModel);

        }
    }
}
