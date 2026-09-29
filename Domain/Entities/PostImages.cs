namespace Domain.Entities
{
    public sealed class PostImages
    {
        public int Id { get; set; }
        public required string ImageUrl { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }

}
