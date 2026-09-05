using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Features.Authentication.DTOS;
using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace Application.Service
{
    internal sealed class AuthenticationService:IAuthanticationService
    {

            private readonly JwtOption _jwtOption;
            private readonly RefreshTokenOption _refreshTokenOption;
            private readonly UserManager<AppUser> _userManager;
            private readonly IUnitOfWork _unitOfWork;
            public AuthenticationService(IOptions<JwtOption>options, IOptions<RefreshTokenOption> refreshOptions, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
            {
                _jwtOption = options.Value;
                 _refreshTokenOption = refreshOptions.Value;
                _userManager = userManager;
                _unitOfWork = unitOfWork;
            }
            public async Task<LoggedUserDto> CreateJWTToken(AppUser appUser)
            {
                DateTime expirationDate = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtOption.expiration_Minutes));

                string issuer = _jwtOption.issuer;
                string adiuence = _jwtOption.audience;


                var claims = new List<Claim>
           {
                new Claim(JwtRegisteredClaimNames.Sub,appUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,  new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.NameIdentifier,appUser.Email!.ToString()),
                new Claim(ClaimTypes.Name,appUser.UserName!.ToString()),

           };


                var roles = await _userManager.GetRolesAsync(appUser);

                List<Claim> claimsRoles = new List<Claim>();
                foreach (var role in roles)
                {
                    claimsRoles.Add(new Claim("role", role));
                }

                claims.AddRange(claimsRoles);





                SymmetricSecurityKey symmetricKey = new(Encoding.UTF8.GetBytes(_jwtOption.key));
                SigningCredentials credntials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                    issuer: issuer,
                    audience: adiuence,
                    claims: claims,
                    expires: expirationDate,
                    signingCredentials: credntials
                    );

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.WriteToken(tokenGenerator);



                return new LoggedUserDto
                {
                    Token = token,
                    Email = appUser.Email,
                    UserName = appUser.UserName,
                    //photoUrl = appUser?.ImageUrl,
                    Id = appUser.Id,
                };

            }

            public ClaimsPrincipal? GetClaimsPrincipal(string token)
            {
                var tokenValidation = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _jwtOption.issuer,
                    ValidAudience = _jwtOption.audience,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.key))
                };

                JwtSecurityTokenHandler jwtHandler = new();
                ClaimsPrincipal principles = jwtHandler.ValidateToken(token, tokenValidation, out SecurityToken validatedToken);

                if (validatedToken is not JwtSecurityToken jwtSecurityToken
                    || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid Token");
                }
                return principles;
            }

        public RefreshTokenDto GetRefrehToken()
        {
            return new RefreshTokenDto
            {
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiration = DateTime.UtcNow.Date.AddDays(Convert.ToDouble(_refreshTokenOption.expiration_Date))
            };
        }

       
        private string GenerateRefreshToken()
            {
                byte[] bytes = new byte[64];
                RandomNumberGenerator generator = RandomNumberGenerator.Create();

                generator.GetBytes(bytes);
                return Convert.ToBase64String(bytes);


            }


            //public async Task<AppUser?> GetUserByValidRefreshToken(string refreshToken)
            //{
            //    AppUser? user = await _unitOfWork.UserRepository.GetQuery
            //        .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken && x.RefreshTokenExpiration > DateTime.UtcNow);
            //    if (user == null)
            //        return null;
            //    return user;
            //}

            //public async Task LogOut()
            //{
            //    await _unitOfWork.UserRepository.GetQuery.ExecuteUpdateAsync(setter => setter.SetProperty(x => x.RefreshToken, _ => null)
            //    .SetProperty(x => x.RefreshTokenExpiration, _ => null));
            //}


    }
}

