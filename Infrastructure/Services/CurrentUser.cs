using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public sealed class CurrentUser (IHttpContextAccessor _httpContextAccessor): ICurrentUser
    {

        public Guid? UserId { 
            get
            {
                var value= _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

              return  Guid.TryParse(value, out Guid result) ? result : null;
            }
            
        }
    }
}
