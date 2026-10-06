using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public sealed class CurrentUser (IHttpContextAccessor _httpContextAccessor,IAppDbContext _context): ICurrentUser
    {

        public Guid? UserId { 
            get
            {
                var value= _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

              return  Guid.TryParse(value, out Guid result) ? result : null;
            }
            
        }


        public async Task<int?> GetProfileId()
        {
            if(!UserId.HasValue)
                return null;
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == UserId.Value);

            return profile?.Id;


        }

    }
}
