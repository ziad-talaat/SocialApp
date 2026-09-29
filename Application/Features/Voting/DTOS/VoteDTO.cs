using Domain.Enums;
namespace Application.Features.Voting.DTOS
{
    public sealed record VoteDTO( int PostId, VoteTypes VoteType);
}
