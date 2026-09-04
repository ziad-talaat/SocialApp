using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authentication.DTOS
{
    public sealed  class RefreshTokenDto
    {
        public string  RefreshToken { get; set; }
        public DateTime  RefreshTokenExpiration { get; set; }
    }
}
