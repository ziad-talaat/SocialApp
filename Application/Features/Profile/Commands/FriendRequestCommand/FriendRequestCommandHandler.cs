using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Application.Resources.GlobalResources;
using Application.Resources.ProfileResources;
using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Application.Features.Profile.Commands.FriendRequestCommand
{
    internal sealed class FriendRequestCommandHandler(IAppDbContext _context,
        IUnitOfWork _unitOfWork, ICurrentUser _currentUser, IStringLocalizer<GlobalValidationMessagesRes> _globalRes, IStringLocalizer<ProfileValidationResource> _profileRes) : IRequestHandler<FriendRequestCommand, Result>
    {
        public async  Task<Result> Handle(FriendRequestCommand request, CancellationToken cancellationToken)
        {
            var sourceId = await _currentUser.GetProfileId();
            if (!sourceId.HasValue)
                return Result.Failure(new Error(_globalRes[GlobalValidationMessagesRes.UnAuthenticatedUser]));

            if (sourceId.Value == request.targetUserId)
                return Result.Failure(new Error(_profileRes[ProfileValidationResource.CycleFriendRequest]));


            var targetExist = await _context.Profiles.AnyAsync(x => x.Id == request.targetUserId, cancellationToken);
            if (!targetExist)
                return Result.Failure(new Error(_profileRes[ProfileValidationResource.unExistProfile]));

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {

                var existedFriendShip = await _context.FriendShips.SingleOrDefaultAsync(x => x.SenderId == sourceId.Value && x.ReceiverId == request.targetUserId, cancellationToken);
                if (existedFriendShip != null)
                {
                    if (existedFriendShip.FriendShipStatus != FriendshipStatus.Rejected)
                    {
                        await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                        return Result.Failure(new Error(_profileRes[ProfileValidationResource.requestFriendShipSent]));
                    }
                   
                        _unitOfWork.FriendShipRepository.Delete(existedFriendShip);
                    
                }

                var friendShip = new FriendShip
                {
                    SenderId = sourceId.Value,
                    ReceiverId = request.targetUserId
                };

                await _unitOfWork.FriendShipRepository.Insert(friendShip);
                await _unitOfWork.SaveAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return Result.Success();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
