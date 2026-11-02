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
        public bool IsLocked { get; set; } 

        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!;
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<PostVote> PostVotes { get; set; } = new List<PostVote>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Group> OwnedGroups { get; set; } = new List<Group>();
        public ICollection<GroupMember> GroupMemberships { get; set; } = new List<GroupMember>();



        public ICollection<ProfileFollowers> Followers { get; set; } = new List<ProfileFollowers>();
        public ICollection<ProfileFollowers> Following { get; set; } = new List<ProfileFollowers>();


        public ICollection<FriendShip> SentFRiendShipRequests { get; set; } = new List<FriendShip>();
        public ICollection<FriendShip> ReceivedFRiendShipRequests { get; set; } = new List<FriendShip>();




    }
}