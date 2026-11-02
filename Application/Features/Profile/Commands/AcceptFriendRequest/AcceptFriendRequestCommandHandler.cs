using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Application.Resources.GlobalResources;
using Domain.Enums;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Application.Features.Profile.Commands.AcceptFriendRequest
{
    internal sealed class AcceptFriendRequestCommandHandler(IAppDbContext _appDbContext,IUnitOfWork _unitOfWork,ICurrentUser _currentUser,IStringLocalizer<GlobalValidationMessagesRes>_globalRes) : IRequestHandler<AcceptFriendRequestCommand, Result>
    {
        public async Task<Result> Handle(AcceptFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var targetUserId= await _currentUser.GetProfileId();
            if(!targetUserId.HasValue)
                return Result.Failure(new Error(_globalRes[GlobalValidationMessagesRes.UnAuthenticatedUser]));




            var friedShip=await _appDbContext.FriendShips.FindAsync(new object []{request.sourceId,targetUserId.Value},cancellationToken);
            if (friedShip == null || friedShip.FriendShipStatus!=FriendshipStatus.Pending)
                return Result.Failure(new Error(_globalRes[GlobalValidationMessagesRes.InvalidOperation]));

            friedShip.FriendShipStatus = FriendshipStatus.Accepted;
            friedShip.CreationDate= DateTime.UtcNow;
           await  _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();
        }
    }
}
