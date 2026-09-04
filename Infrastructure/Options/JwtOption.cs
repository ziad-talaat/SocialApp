using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Infrastructure.Options
{
    public   class JwtOption
    {
        public const string SectionName = "JWT";
        public string key { get; set; } = string.Empty;
        public string issuer { get; set; }=string.Empty;
        public string audience { get; set; } = string.Empty;
        public string expiration_Minutes { get; set; } = string.Empty;
    }
}
