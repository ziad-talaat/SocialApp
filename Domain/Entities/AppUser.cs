using System.ComponentModel.DataAnnotations;
using Domain.CautomeAttributes;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
namespace Domain.Entities
{
    public sealed  class AppUser : IdentityUser<Guid>
    {
        [ProfileName]
        public override string? UserName { get ; set; }
        [AgeValidator]
        public DateOnly DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public Genders Gender { get; set; }

        [MaxLength(1000,ErrorMessage ="Too long message")]
        public string? Description { get; set; }
        [StringLength(50,MinimumLength =2,ErrorMessage ="country name should be greater than 2 chars and less than 50")]
        public required string Country { get; set; }
        public  string? RefreshToken { get; set; }
        public  DateTime? RefreshTokenExpiration { get; set; }
    }
}