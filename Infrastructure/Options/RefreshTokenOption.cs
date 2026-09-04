using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Options
{
    public sealed  class RefreshTokenOption
    {
        public const string SectionName = "refreshToken";
        public string expiration_Date { get; set; } = string.Empty;
    }
}
