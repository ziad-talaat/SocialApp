using System.ComponentModel.DataAnnotations;
using Domain.CautomeAttributes;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
namespace Domain.Entities
{
    public sealed  class AppUser : IdentityUser<Guid>
    {
        //[ProfileName]
        //public override string? UserName { get ; set; }

        [AgeValidator]
        public DateOnly DateOfBirth { get; set; }
        public Genders Gender { get; set; }
        [StringLength(50,MinimumLength =2,ErrorMessage ="country name should be greater than 2 chars and less than 50")]
        public required string Country { get; set; }
        public  string? RefreshToken { get; set; }
        public  DateTime? RefreshTokenExpiration { get; set; }


        public Profile Profile { get; set; }

        public ICollection<PostVote> PostVotes { get; set; } = new List<PostVote>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();

    }
}