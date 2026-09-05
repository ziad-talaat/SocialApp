using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Application.Features.Authentication.DTOS;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
namespace Application.Features.Authentication.Commands.Register
{
    internal sealed class RegisterCommandHandler(IAuthanticationService _authService, UserManager<AppUser> _userManager,IUnitOfWork _unitOfWork) : IRequestHandler<RegisterCommand, Result<LoggedUserDto>>
    {
        public async  Task<Result<LoggedUserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.FindByEmailAsync(request.registerDto.Email);
            if (user is not null)
                return Result<LoggedUserDto>.Failure(new Error("Email is taken"));

          //use auto mapper instead
            user = new AppUser
            {
                Email = request.registerDto.Email,
                Country = request.registerDto.Country,
                Gender = request.registerDto.Gender,
                UserName = request.registerDto.ProfileName,
                DateOfBirth=request.registerDto.DateOfBirth,
            };

           


            var result=  await _userManager.CreateAsync(user,request.registerDto.Password);
            if (!result.Succeeded)
            {
                return Result<LoggedUserDto>.Failure(new Error(result.Errors.FirstOrDefault()?.Description ?? "User Creation failed try later"));
            }
            Domain.Entities.Profile profile = new Domain.Entities.Profile
            {
                ImageUrl = request.registerDto.ImageUrl,
                ProfileName = request.registerDto.ProfileName,
                Bio = request.registerDto.Description,
                UserId =user.Id
            };
           await  _unitOfWork.ProfileRepository.Insert(profile);
            await _unitOfWork.SaveAsync(cancellationToken);

            var tokenModel= await _authService.CreateJWTToken(user);
            return Result<LoggedUserDto>.Success(tokenModel);

        }
    }
}
