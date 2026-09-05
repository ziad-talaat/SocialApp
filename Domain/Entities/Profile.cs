using Domain.CautomeAttributes;
namespace Domain.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public string? Bio { get; set; }
        public string? BackgroundImage { get; set; }
        [ProfileName]
        public required string ProfileName { get; set; }

        public string? ImageUrl { get; set; }
        public DateOnly Created { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public DateTime LastActive { get; set; } = DateTime.UtcNow;



        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!;

        public ICollection<Post> Posts { get; set; } = new List<Post>();



    }
}