using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profile.DTOS
{
    public sealed class ProfileDto
    {
        public int ?ProfileId { get; set; }
        public string ? ImageUrl { get; set; }
        public required string ProfileName { get; set; }
        public string? Bio { get; set; }
        public string? BackgroundImageUrl { get; set; }

    }
}
