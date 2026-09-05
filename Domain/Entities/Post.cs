namespace Domain.Entities
{
    public sealed class Post
    {
        public int Id { get; set; }
        public string? Description { get; set; }
      
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

        public List<PostImages> PostImages { get; set; } = new();
    }

    public sealed class PostImages
    {
        public int Id { get; set; }
        public required string ImageUrl { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }

}
