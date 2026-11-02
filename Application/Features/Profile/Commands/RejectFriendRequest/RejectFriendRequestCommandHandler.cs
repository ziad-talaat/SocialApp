
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Application.Features.Profile.Commands.AcceptFriendRequest;
using Application.Resources.GlobalResources;
using Domain.Enums;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.Profile.Commands.RejectFriendRequest
{
    internal sealed class RejectFriendRequestCommandHandler(IAppDbContext _appDbContext, IUnitOfWork _unitOfWork,ICurrentUser _currentUser,IStringLocalizer<GlobalValidationMessagesRes>_globalRes):IRequestHandler<RejectFriendRequestCommand,Result>
    {
        public async  Task<Result> Handle(RejectFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var targetUserId = await _currentUser.GetProfileId();
            if (!targetUserId.HasValue)
                return Result.Failure(new Error(_globalRes[GlobalValidationMessagesRes.UnAuthenticatedUser]));


            var friedShip = await _appDbContext.FriendShips.FindAsync(new object[]{  request.sourceId, targetUserId.Value  },cancellationToken);
            if (friedShip == null || friedShip.FriendShipStatus != FriendshipStatus.Pending)
                return Result.Failure(new Error(_globalRes[GlobalValidationMessagesRes.InvalidOperation]));

            friedShip.FriendShipStatus = FriendshipStatus.Rejected;  
            friedShip.CreationDate= DateTime.UtcNow;
            await _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();
        }
    }
}
