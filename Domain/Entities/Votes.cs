using Domain.Enums;
namespace Domain.Entities
{
    public sealed class Vote
    {
        public int Id { get; set; }
        public VoteTypes VoteName { get; set; }
    }
}
