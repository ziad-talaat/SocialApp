using Application.Common.ResultPattern;
using MediatR;
namespace Application.Features.Profile.Commands.FriendRequestCommand
{
    public sealed record FriendRequestCommand(int targetUserId):IRequest<Result>;

}
