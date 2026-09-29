using Domain.Enums;

namespace Domain.Entities
{
    public sealed class PostVote
    {
        public int Id { get; set; }
        public VoteTypes VoteType { get; set; }
        public AppUser User { get; set; }
        public Guid UserId { get; set; }

        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}
