using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Authentication.DTOS;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IAuthanticationService
    {
        Task<LoggedUserDto> CreateJWTToken(AppUser appUserDTO);
        RefreshTokenDto GetRefrehToken();
        //Task<AppUser?> GetUserByValidRefreshToken(string refreshToken);
        ClaimsPrincipal? GetClaimsPrincipal(string token);
        //Task LogOut();
    }
}
