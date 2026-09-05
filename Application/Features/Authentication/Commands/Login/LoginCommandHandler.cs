using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Application.Features.Authentication.DTOS;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
namespace Application.Features.Authentication.Commands.Login
{
    internal class LoginCommandHandler(IAuthanticationService _authService,UserManager<AppUser>_userManager,IAppDbContext _context) : IRequestHandler<LoginCommand, Result<LoggedUserDto>>
    {
        public async Task<Result<LoggedUserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.FindByEmailAsync(request.loggenDto.Email);
            if (user is null)
                return  Result<LoggedUserDto>.Failure(new Error("Invalid Credintiels"));
            if (await _userManager.IsLockedOutAsync(user))
                return Result<LoggedUserDto>.Failure(new Error("Account is locked out try later"));

            if (!await _userManager.CheckPasswordAsync(user, request.loggenDto.Password))
            {
                await _userManager.AccessFailedAsync(user);
                return Result<LoggedUserDto>.Failure(new Error("Invalid Credintiels"));
            }
            await _userManager.ResetAccessFailedCountAsync(user);


            int profileCount = _context.Profiles.Where(x => x.UserId == user.Id).Count();
            if (profileCount <= 0)
                return Result<LoggedUserDto>.Failure(new Error("no such Profile with this email"));



            var token = await _authService.CreateJWTToken(user);
            var claimedRefreshToken = _authService.GetRefrehToken();
            user.RefreshToken = claimedRefreshToken.RefreshToken;
            user.RefreshTokenExpiration = claimedRefreshToken.RefreshTokenExpiration;
            //Response.Cookies.Append(
            //  "refreshToken",
            //  refreshTokens.RefreshToken,
            //  new CookieOptions
            //  {
            //      HttpOnly = true,
            //      Secure = false,
            //      SameSite = SameSiteMode.Lax,
            //      Expires = refreshTokens.RefreshTokenExpiration
            //  });

            await _userManager.UpdateAsync(user);

            return Result<LoggedUserDto>.Success(token);

        }
    }
}
