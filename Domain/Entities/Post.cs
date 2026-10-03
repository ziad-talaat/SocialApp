namespace Domain.Entities
{
    public sealed class Post
    {
        public int Id { get; set; }
        public string? Description { get; set; }
      
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        //public AppUser AppUser { get; set; }
        //public Guid UserId { get; set; }

        public List<PostImages> PostImages { get; set; } = new();
        public List<PostVote> PostVotes { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();

        public int? GroupId { get; set; }
        public Group? Group { get; set; }
        public bool IsApproved { get; set; } = true;
    }

}
